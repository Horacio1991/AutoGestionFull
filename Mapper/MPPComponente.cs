using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using BE.BEComposite;
using Servicios.Utilidades;

namespace Mapper
{
    public class MPPComponente
    {
        private readonly string rutaXml = XmlPaths.BaseDatosLocal;

        public MPPComponente()
        {
            AsegurarSeccionComponentes();
        }

        private void AsegurarSeccionComponentes()
        {
            var dir = Path.GetDirectoryName(rutaXml);
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);

            if (!File.Exists(rutaXml))
            {
                new XDocument(
                    new XElement("BaseDeDatosLocal",
                        new XElement("Componentes"),
                        new XElement("Usuario_Permisos")
                    )
                ).Save(rutaXml);
            }
            else
            {
                var doc = XDocument.Load(rutaXml);
                if (doc.Root.Element("Componentes") == null)
                    doc.Root.Add(new XElement("Componentes"));
                if (doc.Root.Element("Usuario_Permisos") == null)
                    doc.Root.Add(new XElement("Usuario_Permisos"));
                doc.Save(rutaXml);
            }
        }

        public List<BEComponente> ListarTodo()
        {
            try
            {
                var doc = XDocument.Load(rutaXml);
                var root = doc.Root.Element("Componentes");
                if (root == null) return new List<BEComponente>();

                return root.Elements()
                           .Where(n => (string)n.Attribute("Active") != "false")
                           .Select(ParsearComponente)
                           .Where(c => c != null)
                           .ToList();
            }
            catch
            {
                return new List<BEComponente>();
            }
        }

        private BEComponente ParsearComponente(XElement nodo)
        {
            if (nodo.Name == "Rol")
            {
                var rol = new BERol
                {
                    Id = (int)nodo.Attribute("Id"),
                    Nombre = (string)(nodo.Element("Nombre")?.Value ?? nodo.Attribute("Nombre")?.Value)
                };
                foreach (var permisoXml in nodo.Elements("Permiso"))
                    rol.AgregarHijo(ParsearComponente(permisoXml));
                return rol;
            }
            else if (nodo.Name == "Permiso")
            {
                return new BEPermiso
                {
                    Id = (int)nodo.Attribute("Id"),
                    Nombre = (string)(nodo.Element("Nombre")?.Value ?? nodo.Attribute("Nombre")?.Value)
                };
            }
            return null;
        }

        public List<BEComponente> ListarPermisosUsuario(int idUsuario)
        {
            try
            {
                var todos = ListarTodo();
                var doc = XDocument.Load(rutaXml);
                var ups = doc.Root.Element("Usuario_Permisos");
                if (ups == null) return new List<BEComponente>();

                var resultado = new List<BEComponente>();
                foreach (var up in ups.Elements("Usuario_Permiso"))
                {
                    if ((int)up.Element("IdUsuario") == idUsuario)
                    {
                        int idComp = (int)up.Element("IdComponente");
                        var c = BuscarPorId(todos, idComp);
                        if (c != null) resultado.Add(c);
                    }
                }
                return resultado;
            }
            catch
            {
                return new List<BEComponente>();
            }
        }

        private BEComponente BuscarPorId(IEnumerable<BEComponente> lista, int id)
        {
            foreach (var c in lista)
            {
                if (c.Id == id) return c;
                if (c is BERol rol)
                {
                    var hijo = BuscarPorId(rol.Hijos, id);
                    if (hijo != null) return hijo;
                }
            }
            return null;
        }

        public bool AltaPermiso(string nombrePermiso)
        {
            try
            {
                var doc = XDocument.Load(rutaXml);
                var root = doc.Root.Element("Componentes");
                if (root.Elements("Permiso")
                        .Any(p => (string)p.Element("Nombre") == nombrePermiso && (string)p.Attribute("Active") != "false"))
                    return false;

                int nuevoId = SiguienteId(esPermiso: true, doc);
                root.Add(new XElement("Permiso",
                    new XAttribute("Id", nuevoId),
                    new XAttribute("Active", "true"),
                    new XElement("Nombre", nombrePermiso)
                ));
                doc.Save(rutaXml);
                return true;
            }
            catch { return false; }
        }

        public bool BajaPermiso(int idPermiso)
        {
            try
            {
                var doc = XDocument.Load(rutaXml);
                var root = doc.Root.Element("Componentes");
                var nodo = root.Elements("Permiso")
                               .FirstOrDefault(p => (int)p.Attribute("Id") == idPermiso);
                if (nodo == null) return false;

                nodo.SetAttributeValue("Active", "false");
                // eliminar referencias en roles
                foreach (var rol in root.Elements("Rol"))
                    rol.Elements("Permiso")
                        .Where(p => (int)p.Attribute("Id") == idPermiso)
                        .Remove();
                doc.Save(rutaXml);
                return true;
            }
            catch { return false; }
        }

        public bool AltaRol(string nombreRol, List<BEPermiso> permisos)
        {
            try
            {
                var doc = XDocument.Load(rutaXml);
                var root = doc.Root.Element("Componentes");

                int nuevoId = SiguienteId(esPermiso: false, doc);
                var rolXml = new XElement("Rol",
                    new XAttribute("Id", nuevoId),
                    new XAttribute("Active", "true"),
                    new XElement("Nombre", nombreRol)
                );
                foreach (var p in permisos)
                    rolXml.Add(new XElement("Permiso",
                        new XAttribute("Id", p.Id),
                        new XAttribute("Active", "true"),
                        new XElement("Nombre", p.Nombre)
                    ));
                root.Add(rolXml);
                doc.Save(rutaXml);
                return true;
            }
            catch { return false; }
        }

        public bool ModificarRol(BERol rol, List<BEPermiso> nuevosPermisos)
        {
            try
            {
                var doc = XDocument.Load(rutaXml);
                var root = doc.Root.Element("Componentes");
                var rolXml = root.Elements("Rol")
                                 .FirstOrDefault(r => (int)r.Attribute("Id") == rol.Id);
                if (rolXml == null) return false;

                rolXml.Element("Nombre")?.SetValue(rol.Nombre);
                rolXml.Elements("Permiso").Remove();
                foreach (var p in nuevosPermisos)
                    rolXml.Add(new XElement("Permiso",
                        new XAttribute("Id", p.Id),
                        new XElement("Nombre", p.Nombre)
                    ));
                doc.Save(rutaXml);
                return true;
            }
            catch { return false; }
        }

        public bool BajaRol(int idRol)
        {
            try
            {
                var doc = XDocument.Load(rutaXml);
                var root = doc.Root.Element("Componentes");
                var rolXml = root.Elements("Rol")
                                 .FirstOrDefault(r => (int)r.Attribute("Id") == idRol);
                if (rolXml == null) return false;

                rolXml.SetAttributeValue("Active", "false");
                // eliminar asignaciones a usuarios
                doc.Root.Element("Usuario_Permisos")?
                   .Elements("Usuario_Permiso")
                   .Where(up => (int)up.Element("IdComponente") == idRol)
                   .Remove();
                doc.Save(rutaXml);
                return true;
            }
            catch { return false; }
        }

        public bool AsignarComponenteAUsuario(int idUsuario, int idComp)
        {
            try
            {
                var doc = XDocument.Load(rutaXml);
                var ups = doc.Root.Element("Usuario_Permisos");
                bool existe = ups.Elements("Usuario_Permiso")
                                 .Any(up => (int)up.Element("IdUsuario") == idUsuario
                                         && (int)up.Element("IdComponente") == idComp);
                if (!existe)
                    ups.Add(new XElement("Usuario_Permiso",
                        new XElement("IdUsuario", idUsuario),
                        new XElement("IdComponente", idComp)
                    ));
                doc.Save(rutaXml);
                return true;
            }
            catch { return false; }
        }

        public bool EliminarComponenteDeUsuario(int idUsuario, int idComp)
        {
            try
            {
                var doc = XDocument.Load(rutaXml);
                var ups = doc.Root.Element("Usuario_Permisos");
                var nodo = ups.Elements("Usuario_Permiso")
                              .FirstOrDefault(up => (int)up.Element("IdUsuario") == idUsuario
                                                && (int)up.Element("IdComponente") == idComp);
                if (nodo != null)
                {
                    nodo.Remove();
                    doc.Save(rutaXml);
                }
                return true;
            }
            catch { return false; }
        }

        private int SiguienteId(bool esPermiso, XDocument doc)
        {
            int min = esPermiso ? 1000 : 1;
            int max = doc.Descendants()
                         .Attributes("Id")
                         .Select(a => (int?)int.Parse(a.Value))
                         .Where(id => id.HasValue && (esPermiso ? id >= 1000 : id < 1000))
                         .DefaultIfEmpty(min - 1)
                         .Max().Value;
            return max + 1;
        }
    }
}
