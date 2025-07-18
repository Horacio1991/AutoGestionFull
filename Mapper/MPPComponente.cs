using System.Xml.Linq;
using DTOs;
using Servicios.Utilidades;

namespace Mapper
{
    public class MPPComponente
    {
        private readonly string _rutaXml = XmlPaths.BaseDatosLocal;

        public MPPComponente()
        {
            AsegurarSeccionComponentes();
        }

        private void AsegurarSeccionComponentes()
        {
            try
            {
                var dir = Path.GetDirectoryName(_rutaXml);
                if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);

                if (!File.Exists(_rutaXml))
                {
                    new XDocument(
                        new XElement("BaseDeDatosLocal",
                            new XElement("Componentes"),
                            new XElement("Usuario_Permisos")
                        )
                    ).Save(_rutaXml);
                }
                else
                {
                    var doc = XDocument.Load(_rutaXml);
                    if (doc.Root.Element("Componentes") == null)
                        doc.Root.Add(new XElement("Componentes"));
                    if (doc.Root.Element("Usuario_Permisos") == null)
                        doc.Root.Add(new XElement("Usuario_Permisos"));
                    doc.Save(_rutaXml);
                }
            }
            catch (Exception)
            {
            }
        }

        public List<RolDto> ListarRolesDto()
        {
            try
            {
                var doc = XDocument.Load(_rutaXml);
                var comps = doc.Root.Element("Componentes")?.Elements("Rol")
                           .Where(r => (string)r.Attribute("Active") != "false")
                           .Select(ParseRolDto)
                           .ToList();
                return comps ?? new List<RolDto>();
            }
            catch (Exception)
            {
                return new List<RolDto>();
            }
        }

        public List<PermisoDto> ListarPermisosDto()
        {
            try
            {
                var doc = XDocument.Load(_rutaXml);
                var comps = doc.Root.Element("Componentes")?.Elements("Permiso")
                           .Where(p => (string)p.Attribute("Active") != "false")
                           .Select(ParsePermisoDto)
                           .ToList();
                return comps ?? new List<PermisoDto>();
            }
            catch (Exception)
            {
                return new List<PermisoDto>();
            }
        }

        public List<PermisoDto> ListarPermisosUsuarioDto(int usuarioId)
        {
            try
            {
                var doc = XDocument.Load(_rutaXml);
                var ups = doc.Root.Element("Usuario_Permisos");
                var componentesRoot = doc.Root.Element("Componentes");
                if (ups == null || componentesRoot == null)
                    return new List<PermisoDto>();

                var resultado = new List<PermisoDto>();

                foreach (var up in ups.Elements("Usuario_Permiso"))
                {
                    if ((int)up.Element("IdUsuario") != usuarioId) continue;

                    int compId = (int)up.Element("IdComponente");

                    // 1) Permisos sueltos
                    var permisoXml = componentesRoot
                        .Elements("Permiso")
                        .FirstOrDefault(x => (int)x.Attribute("Id") == compId && (string)x.Attribute("Active") != "false");
                    if (permisoXml != null)
                    {
                        resultado.Add(new PermisoDto
                        {
                            Id = compId,
                            Nombre = (string)permisoXml.Element("Nombre")
                        });
                        continue;
                    }

                    // 2) Si es un Rol: extraigo sus hijos Permiso
                    var rolXml = componentesRoot
                        .Elements("Rol")
                        .FirstOrDefault(r => (int)r.Attribute("Id") == compId && (string)r.Attribute("Active") != "false");
                    if (rolXml != null)
                    {
                        foreach (var px in rolXml.Elements("Permiso")
                                                 .Where(p => (string)p.Attribute("Active") != "false"))
                        {
                            resultado.Add(new PermisoDto
                            {
                                Id = (int)px.Attribute("Id"),
                                Nombre = (string)px.Element("Nombre")
                            });
                        }
                        continue;
                    }
                }

                // Sin duplicados
                return resultado
                    .GroupBy(p => p.Id)
                    .Select(g => g.First())
                    .ToList();
            }
            catch (Exception)
            {
                return new List<PermisoDto>();
            }
        }

        public bool AltaPermiso(string nombrePermiso)
        {
            try
            {
                var doc = XDocument.Load(_rutaXml);
                var root = doc.Root.Element("Componentes");
                if (root.Elements("Permiso")
                        .Any(x => (string)x.Element("Nombre") == nombrePermiso
                               && (string)x.Attribute("Active") != "false"))
                    return false;

                int id = SiguienteId(isPermiso: true, doc);
                root.Add(new XElement("Permiso",
                    new XAttribute("Id", id),
                    new XAttribute("Active", "true"),
                    new XElement("Nombre", nombrePermiso)
                ));
                doc.Save(_rutaXml);
                return true;
            }
            catch { return false; }
        }

        public bool BajaPermiso(int permisoId)
        {
            try
            {
                var doc = XDocument.Load(_rutaXml);
                var comp = doc.Root.Element("Componentes");
                var nodo = comp.Elements("Permiso")
                               .FirstOrDefault(x => (int)x.Attribute("Id") == permisoId);
                if (nodo == null) return false;
                nodo.SetAttributeValue("Active", "false");
                // quito de cualquier rol también
                foreach (var rol in comp.Elements("Rol"))
                    rol.Elements("Permiso")
                        .Where(p => (int)p.Attribute("Id") == permisoId)
                        .Remove();
                doc.Save(_rutaXml);
                return true;
            }
            catch { return false; }
        }

        public bool AltaRol(string nombreRol, List<int> permisoIds)
        {
            try
            {
                var doc = XDocument.Load(_rutaXml);
                var comp = doc.Root.Element("Componentes");
                int id = SiguienteId(isPermiso: false, doc);
                var rolXml = new XElement("Rol",
                    new XAttribute("Id", id),
                    new XAttribute("Active", "true"),
                    new XElement("Nombre", nombreRol)
                );
                // para cada permiso, guardo el nombre actual
                var allPerms = ListarPermisosDto();
                foreach (var pid in permisoIds)
                {
                    var p = allPerms.FirstOrDefault(x => x.Id == pid);
                    rolXml.Add(new XElement("Permiso",
                        new XAttribute("Id", pid),
                        new XAttribute("Active", "true"),
                        new XElement("Nombre", p?.Nombre ?? "")
                    ));
                }
                comp.Add(rolXml);
                doc.Save(_rutaXml);
                return true;
            }
            catch { return false; }
        }

        public bool ModificarRol(int rolId, string nuevoNombre, List<int> nuevosPermisos)
        {
            try
            {
                var doc = XDocument.Load(_rutaXml);
                var comp = doc.Root.Element("Componentes");
                var rolXml = comp.Elements("Rol")
                                 .FirstOrDefault(r => (int)r.Attribute("Id") == rolId);
                if (rolXml == null) return false;
                rolXml.Element("Nombre")?.SetValue(nuevoNombre);
                rolXml.Elements("Permiso").Remove();

                var allPerms = ListarPermisosDto();
                foreach (var pid in nuevosPermisos)
                {
                    var p = allPerms.FirstOrDefault(x => x.Id == pid);
                    rolXml.Add(new XElement("Permiso",
                        new XAttribute("Id", pid),
                        new XAttribute("Active", "true"),
                        new XElement("Nombre", p?.Nombre ?? "")
                    ));
                }
                doc.Save(_rutaXml);
                return true;
            }
            catch { return false; }
        }

        public bool BajaRol(int rolId)
        {
            try
            {
                var doc = XDocument.Load(_rutaXml);
                var comp = doc.Root.Element("Componentes");
                var rolXml = comp.Elements("Rol")
                                 .FirstOrDefault(r => (int)r.Attribute("Id") == rolId);
                if (rolXml == null) return false;
                rolXml.SetAttributeValue("Active", "false");
                // remover asignaciones de usuario
                doc.Root.Element("Usuario_Permisos")?
                   .Elements("Usuario_Permiso")
                   .Where(up => (int)up.Element("IdComponente") == rolId)
                   .Remove();
                doc.Save(_rutaXml);
                return true;
            }
            catch { return false; }
        }

        public bool AsignarPermisoARol(int rolId, int permisoId)
        {
            try
            {
                var doc = XDocument.Load(_rutaXml);
                var rolXml = doc.Root.Element("Componentes")
                              .Elements("Rol")
                              .FirstOrDefault(r => (int)r.Attribute("Id") == rolId);
                if (rolXml == null) return false;
                // no duplicar
                if (rolXml.Elements("Permiso").Any(p => (int)p.Attribute("Id") == permisoId))
                    return false;
                var allPerms = ListarPermisosDto();
                var p = allPerms.FirstOrDefault(x => x.Id == permisoId);
                rolXml.Add(new XElement("Permiso",
                    new XAttribute("Id", permisoId),
                    new XAttribute("Active", "true"),
                    new XElement("Nombre", p?.Nombre ?? "")
                ));
                doc.Save(_rutaXml);
                return true;
            }
            catch { return false; }
        }

        public bool QuitarPermisoDeRol(int rolId, int permisoId)
        {
            try
            {
                var doc = XDocument.Load(_rutaXml);
                var rolXml = doc.Root.Element("Componentes")
                              .Elements("Rol")
                              .FirstOrDefault(r => (int)r.Attribute("Id") == rolId);
                if (rolXml == null) return false;
                rolXml.Elements("Permiso")
                      .Where(p => (int)p.Attribute("Id") == permisoId)
                      .Remove();
                doc.Save(_rutaXml);
                return true;
            }
            catch { return false; }
        }

        // Asignación de componentes a usuarios
        public bool AsignarAUsuario(int usuarioId, int componenteId)
        {
            try
            {
                var doc = XDocument.Load(_rutaXml);
                var ups = doc.Root.Element("Usuario_Permisos");
                bool existe = ups.Elements("Usuario_Permiso")
                                 .Any(up => (int)up.Element("IdUsuario") == usuarioId
                                            && (int)up.Element("IdComponente") == componenteId);
                if (!existe)
                    ups.Add(new XElement("Usuario_Permiso",
                        new XElement("IdUsuario", usuarioId),
                        new XElement("IdComponente", componenteId)
                    ));
                doc.Save(_rutaXml);
                return true;
            }
            catch { return false; }
        }

        public bool QuitarDeUsuario(int usuarioId, int componenteId)
        {
            try
            {
                var doc = XDocument.Load(_rutaXml);
                var ups = doc.Root.Element("Usuario_Permisos");
                var nodo = ups.Elements("Usuario_Permiso")
                              .FirstOrDefault(up => (int)up.Element("IdUsuario") == usuarioId
                                                   && (int)up.Element("IdComponente") == componenteId);
                if (nodo != null)
                {
                    nodo.Remove();
                    doc.Save(_rutaXml);
                }
                return true;
            }
            catch { return false; }
        }

        private RolDto ParseRolDto(XElement nodo)
        {
            var rol = new RolDto
            {
                Id = (int)nodo.Attribute("Id"),
                Nombre = (string)nodo.Element("Nombre") ?? ""
            };
            foreach (var px in nodo.Elements("Permiso")
                                   .Where(p => (string)p.Attribute("Active") != "false"))
            {
                var pd = new PermisoDto
                {
                    Id = (int)px.Attribute("Id"),
                    Nombre = (string)px.Element("Nombre") ?? ""
                };
                rol.Permisos.Add(pd);
            }
            return rol;
        }

        private PermisoDto ParsePermisoDto(XElement nodo)
        {
            return new PermisoDto
            {
                Id = (int)nodo.Attribute("Id"),
                Nombre = (string)nodo.Element("Nombre") ?? ""
            };
        }

        // Elije IDs distintos para permisos (>=1000) y roles (<1000)
        private int SiguienteId(bool isPermiso, XDocument doc)
        {
            int min = isPermiso ? 1000 : 1;
            var ids = doc.Descendants()
                         .Attributes("Id")
                         .Select(a => (int?)int.Parse(a.Value))
                         .Where(id => id.HasValue && (isPermiso ? id >= 1000 : id < 1000))
                         .Select(i => i.Value);
            int max = ids.DefaultIfEmpty(min - 1).Max();
            return max + 1;
        }
    }
}
