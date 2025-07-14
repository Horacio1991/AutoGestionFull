using BE.BEComposite;
using Servicios.Utilidades;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace MAPPER
{
    public class MPPComponente
    {
        private readonly string rutaXml = XmlPaths.BaseDatosLocal;
        private int SiguienteId(bool esPermiso, XDocument doc)
        {
            try
            {
                int minimo = esPermiso ? 1000 : 1;

                int maximo = doc.Descendants()
                                .Attributes("Id")
                                .Select(a => int.Parse(a.Value))
                                .Where(id => esPermiso ? id >= 1000 : id < 1000)
                                .DefaultIfEmpty(minimo - 1)
                                .Max();

                return maximo + 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al calcular el siguiente ID: " + ex.Message);
                return esPermiso ? 1000 : 1;
            }
        }

        public List<BEComponente> ListarTodo()
        {
            var componentes = new List<BEComponente>();
            try
            {
                XDocument xmlDoc = XDocument.Load(rutaXml);
                XElement componentesNode = xmlDoc.Root.Element("Componentes");
                if (componentesNode == null)
                {
                    componentesNode = new XElement("Componentes");
                    xmlDoc.Root.Add(componentesNode);
                    xmlDoc.Save(rutaXml);
                    return componentes;
                }

                foreach (var nodo in componentesNode.Elements()
                                                    .Where(n => n.Attribute("Active")?.Value != "false"))
                {
                    componentes.Add(ParsearComponente(nodo));
                }
                return componentes;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al listar componentes: " + ex.Message);
                return new List<BEComponente>();
            }
        }

        private BEComponente ParsearComponente(XElement nodo)
        {
            try
            {
                if (nodo.Name == "Rol")
                {
                    BERol rol = new BERol
                    {
                        Id = int.Parse(nodo.Attribute("Id").Value),
                        Nombre = nodo.Element("Nombre") != null
                                 ? nodo.Element("Nombre").Value
                                 : nodo.Attribute("Nombre")?.Value ?? "[s/n]"
                    };

                    foreach (var hijo in nodo.Elements("Permiso"))
                        rol.AgregarHijo(ParsearComponente(hijo));

                    return rol;
                }
                else if (nodo.Name == "Permiso")
                {
                    return new BEPermiso
                    {
                        Id = int.Parse(nodo.Attribute("Id").Value),
                        Nombre = nodo.Element("Nombre") != null
                                 ? nodo.Element("Nombre").Value
                                 : nodo.Attribute("Nombre")?.Value ?? "[s/n]"
                    };
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al parsear componente: " + ex.Message);
                return null;
            }
        }

        public List<BEComponente> ListarPermisosUsuario(int idUsuario)
        {
            var resultado = new List<BEComponente>();
            try
            {
                List<BEComponente> componentes = ListarTodo();
                XDocument xmlDoc = XDocument.Load(rutaXml);
                var permisosNode = xmlDoc.Root.Element("Usuario_Permisos");

                foreach (var nodo in permisosNode.Elements("Usuario_Permiso"))
                {
                    if (int.Parse(nodo.Element("IdUsuario").Value) == idUsuario)
                    {
                        int idComp = int.Parse(nodo.Element("IdComponente").Value);
                        BEComponente comp = BuscarComponentePorId(componentes, idComp);
                        if (comp != null)
                            resultado.Add(comp);
                    }
                }
                return resultado;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al listar permisos de usuario: " + ex.Message);
                return new List<BEComponente>();
            }
        }

        private BEComponente BuscarComponentePorId(List<BEComponente> comps, int id)
        {
            foreach (var c in comps)
            {
                if (c.Id == id) return c;
                if (c is BERol r)
                {
                    var hijo = BuscarComponentePorId(r.Hijos, id);
                    if (hijo != null) return hijo;
                }
            }
            return null;
        }

        public bool AltaPermiso(string nombrePermiso)
        {
            try
            {
                XDocument xmlDoc = XDocument.Load(rutaXml);

                XElement root = xmlDoc.Root.Element("Componentes");
                if (root == null)
                {
                    root = new XElement("Componentes");
                    xmlDoc.Root.Add(root);
                }

                bool duplicado = root.Elements("Permiso")
                                     .Any(p => (p.Element("Nombre")?.Value ?? p.Attribute("Nombre")?.Value)
                                                == nombrePermiso &&
                                                p.Attribute("Active")?.Value != "false");
                if (duplicado) return false;

                int id = SiguienteId(true, xmlDoc);

                XElement nuevoPermiso = new XElement("Permiso",
                    new XAttribute("Id", id),
                    new XAttribute("Active", "true"),
                    new XElement("Nombre", nombrePermiso));

                root.Add(nuevoPermiso);
                xmlDoc.Save(rutaXml);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al dar de alta permiso: " + ex.Message);
                return false;
            }
        }

        public bool BajaPermiso(int idPermiso)
        {
            try
            {
                XDocument xmlDoc = XDocument.Load(rutaXml);

                XElement nodo = xmlDoc.Root.Element("Componentes")
                                           .Elements("Permiso")
                                           .FirstOrDefault(p => (int)p.Attribute("Id") == idPermiso);

                if (nodo == null) return false;

                nodo.SetAttributeValue("Active", "false");

                foreach (var rol in xmlDoc.Root.Element("Componentes").Elements("Rol"))
                {
                    XElement refPerm = rol.Elements("Permiso")
                                          .FirstOrDefault(p => (int)p.Attribute("Id") == idPermiso);
                    if (refPerm != null)
                        refPerm.Remove();
                }

                xmlDoc.Save(rutaXml);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al dar de baja permiso: " + ex.Message);
                return false;
            }
        }

        public bool AltaRol(string nombreRol, List<BEPermiso> permisos)
        {
            try
            {
                XDocument xmlDoc = XDocument.Load(rutaXml);

                XElement root = xmlDoc.Root.Element("Componentes");
                if (root == null)
                {
                    root = new XElement("Componentes");
                    xmlDoc.Root.Add(root);
                }

                int idRol = SiguienteId(false, xmlDoc);

                XElement rolXml = new XElement("Rol",
                    new XAttribute("Id", idRol),
                    new XAttribute("Active", "true"),
                    new XElement("Nombre", nombreRol));

                foreach (var p in permisos)
                {
                    rolXml.Add(new XElement("Permiso",
                        new XAttribute("Id", p.Id),
                        new XAttribute("Active", "true"),
                        new XElement("Nombre", p.Nombre)));
                }

                root.Add(rolXml);
                xmlDoc.Save(rutaXml);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al dar de alta rol: " + ex.Message);
                return false;
            }
        }

        public bool ModificarRol(BERol rol, List<BEPermiso> nuevosPermisos)
        {
            try
            {
                XDocument xmlDoc = XDocument.Load(rutaXml);
                XElement rolXml = xmlDoc.Root.Element("Componentes")
                                             .Elements("Rol")
                                             .FirstOrDefault(r => (int)r.Attribute("Id") == rol.Id);
                if (rolXml == null) return false;

                rolXml.Element("Nombre")?.Remove();
                rolXml.AddFirst(new XElement("Nombre", rol.Nombre));

                rolXml.Elements("Permiso").Remove();
                foreach (var p in nuevosPermisos)
                {
                    rolXml.Add(new XElement("Permiso",
                        new XAttribute("Id", p.Id)));
                }

                xmlDoc.Save(rutaXml);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al modificar rol: " + ex.Message);
                return false;
            }
        }


        public bool BajaRol(int idRol)
        {
            try
            {
                XDocument xmlDoc = XDocument.Load(rutaXml);

                XElement root = xmlDoc.Root.Element("Componentes");
                if (root == null) return false;

                XElement rolXml = root.Elements("Rol")
                                      .FirstOrDefault(r => (int)r.Attribute("Id") == idRol);
                if (rolXml == null) return false;

                rolXml.SetAttributeValue("Active", "false");

                XElement upRoot = xmlDoc.Root.Element("Usuario_Permisos");
                if (upRoot != null)
                {
                    foreach (var up in upRoot.Elements("Usuario_Permiso")
                                             .Where(up => (int)up.Element("IdComponente") == idRol)
                                             .ToList())
                    {
                        up.Remove();
                    }
                }

                xmlDoc.Save(rutaXml);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al dar de baja rol: " + ex.Message);
                return false;
            }
        }

        public bool AsignarRolAUsuario(int idUsuario, int idRol)
        {
            try
            {
                return AsignarComponenteAUsuario(idUsuario, idRol);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al asignar rol a usuario: " + ex.Message);
                return false;
            }
        }

        public bool AsignarPermisoARol(int idRol, int idPermiso)
        {
            try
            {
                XDocument xmlDoc = XDocument.Load(rutaXml);

                XElement rolXml = xmlDoc.Root.Element("Componentes")
                                             .Elements("Rol")
                                             .FirstOrDefault(r => (int)r.Attribute("Id") == idRol &&
                                                                  r.Attribute("Active")?.Value != "false");
                if (rolXml == null) return false;

                bool ya = rolXml.Elements("Permiso")
                                .Any(p => (int)p.Attribute("Id") == idPermiso);
                if (ya) return true;

                XElement permisoRoot = xmlDoc.Root.Element("Componentes")
                                                  .Elements("Permiso")
                                                  .FirstOrDefault(p => (int)p.Attribute("Id") == idPermiso &&
                                                                       p.Attribute("Active")?.Value != "false");
                if (permisoRoot == null) return false;

                string nombrePermiso = permisoRoot.Element("Nombre") != null
                                       ? permisoRoot.Element("Nombre").Value
                                       : permisoRoot.Attribute("Nombre")?.Value ?? "[s/n]";

                rolXml.Add(new XElement("Permiso",
                    new XAttribute("Id", idPermiso),
                    new XElement("Nombre", nombrePermiso)));

                xmlDoc.Save(rutaXml);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al asignar permiso a rol: " + ex.Message);
                return false;
            }
        }
        public bool QuitarPermisoDeRol(int idRol, int idPermiso)
        {
            try
            {
                XDocument xmlDoc = XDocument.Load(rutaXml);

                XElement rolXml = xmlDoc.Root.Element("Componentes")
                                             .Elements("Rol")
                                             .FirstOrDefault(r => (int)r.Attribute("Id") == idRol &&
                                                                  r.Attribute("Active")?.Value != "false");
                if (rolXml == null) return false;

                XElement permXml = rolXml.Elements("Permiso")
                                         .FirstOrDefault(p => (int)p.Attribute("Id") == idPermiso);

                if (permXml == null) return false;

                permXml.Remove();
                xmlDoc.Save(rutaXml);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al quitar permiso de rol: " + ex.Message);
                return false;
            }
        }

        public bool AsignarComponenteAUsuario(int idUsuario, int idComp)
        {
            try
            {
                XDocument xmlDoc = XDocument.Load(rutaXml);

                XElement root = xmlDoc.Root.Element("Usuario_Permisos");
                if (root == null)
                {
                    root = new XElement("Usuario_Permisos");
                    xmlDoc.Root.Add(root);
                }

                bool existe = root.Elements("Usuario_Permiso")
                                  .Any(x => int.Parse(x.Element("IdUsuario").Value) == idUsuario &&
                                            int.Parse(x.Element("IdComponente").Value) == idComp);
                if (!existe)
                {
                    root.Add(new XElement("Usuario_Permiso",
                        new XElement("IdUsuario", idUsuario),
                        new XElement("IdComponente", idComp)));
                    xmlDoc.Save(rutaXml);
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al asignar componente a usuario: " + ex.Message);
                return false;
            }
        }

        public bool EliminarComponenteDeUsuario(int idUsuario, int idComp)
        {
            try
            {
                XDocument xmlDoc = XDocument.Load(rutaXml);
                XElement root = xmlDoc.Root.Element("Usuario_Permisos");
                if (root == null)
                {
                    return true;
                }

                var nodo = root.Elements("Usuario_Permiso")
                               .FirstOrDefault(x => int.Parse(x.Element("IdUsuario").Value) == idUsuario &&
                                                    int.Parse(x.Element("IdComponente").Value) == idComp);
                if (nodo != null)
                {
                    nodo.Remove();
                    xmlDoc.Save(rutaXml);
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al eliminar componente de usuario: " + ex.Message);
                return false;
            }
        }

    }
}
