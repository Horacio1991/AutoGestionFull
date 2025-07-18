using System.Xml.Linq;
using BE;
using BE.BEComposite;
using Servicios.Utilidades;

namespace Mapper
{
    public class MPPUsuario
    {
        private readonly string rutaXML = XmlPaths.BaseDatosLocal;

        public List<Usuario> ListarTodo()
        {
            try
            {
                if (!File.Exists(rutaXML))
                    return new List<Usuario>();

                var doc = XDocument.Load(rutaXML);
                var nodoUsuarios = doc.Root.Element("Usuarios");
                if (nodoUsuarios == null)
                    return new List<Usuario>();

                return nodoUsuarios
                    .Elements("Usuario")
                    .Where(x => (string)x.Attribute("Active") == "true")
                    .Select(x => new Usuario
                    {
                        Id = (int)x.Attribute("Id"),
                        Username = (string)x.Element("Username"),
                        Password = (string)x.Element("Password"),
                        Rol = new List<BEComponente>() // Se llena desde BLLComponente si es necesario
                    })
                    .ToList();
            }
            catch (Exception)
            {
                return new List<Usuario>();
            }
        }

        public Usuario BuscarPorId(int id)
        {
            try
            {
                if (!File.Exists(rutaXML))
                    return null;
                var doc = XDocument.Load(rutaXML);
                var element = doc.Root.Element("Usuarios")?
                                 .Elements("Usuario")
                                 .FirstOrDefault(x => (int)x.Attribute("Id") == id && (string)x.Attribute("Active") == "true");
                if (element == null)
                    return null;
                return new Usuario
                {
                    Id = (int)element.Attribute("Id"),
                    Username = (string)element.Element("Username"),
                    Password = (string)element.Element("Password"),
                    Rol = new List<BEComponente>()
                };
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Usuario BuscarPorUsername(string username)
        {
            try
            {
                if (!File.Exists(rutaXML))
                    return null;

                var doc = XDocument.Load(rutaXML);
                var element = doc.Root
                                 .Element("Usuarios")?
                                 .Elements("Usuario")
                                 .FirstOrDefault(x =>
                                      (string)x.Attribute("Active") == "true" &&
                                      string.Equals((string)x.Element("Username"),
                                                    username,
                                                    StringComparison.OrdinalIgnoreCase));

                if (element == null)
                    return null;

                return new Usuario
                {
                    Id = (int)element.Attribute("Id"),
                    Username = (string)element.Element("Username"),
                    Password = (string)element.Element("Password"),
                    Rol = new List<BEComponente>()
                };
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void Agregar(Usuario user)
        {
            try
            {
                XDocument doc;
                if (File.Exists(rutaXML))
                {
                    doc = XDocument.Load(rutaXML);
                    if (doc.Root.Element("Usuarios") == null)
                        doc.Root.Add(new XElement("Usuarios"));
                }
                else
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(rutaXML));
                    doc = new XDocument(
                        new XElement("BaseDeDatosLocal",
                            new XElement("Usuarios")
                        )
                    );
                }

                var usuarios = doc.Root.Element("Usuarios");
                int nuevoId = usuarios.Elements("Usuario")
                                      .Select(x => (int)x.Attribute("Id"))
                                      .DefaultIfEmpty(0)
                                      .Max() + 1;

                var nuevo = new XElement("Usuario",
                    new XAttribute("Id", nuevoId),
                    new XAttribute("Active", "true"),
                    new XElement("Username", user.Username),
                    new XElement("Password", user.Password)
                );

                usuarios.Add(nuevo);
                doc.Save(rutaXML);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("No se pudo agregar el usuario. " + ex.Message, ex);
            }
        }

        public void Actualizar(Usuario user)
        {
            try
            {
                if (!File.Exists(rutaXML))
                    throw new FileNotFoundException("Archivo de usuarios no encontrado.");

                var doc = XDocument.Load(rutaXML);
                var element = doc.Root.Element("Usuarios")?
                                 .Elements("Usuario")
                                 .FirstOrDefault(x => (int)x.Attribute("Id") == user.Id);

                if (element == null)
                    throw new InvalidOperationException("Usuario no encontrado.");

                element.SetAttributeValue("Active", "true");
                element.Element("Username")?.SetValue(user.Username);
                element.Element("Password")?.SetValue(user.Password);
                doc.Save(rutaXML);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("No se pudo actualizar el usuario. " + ex.Message, ex);
            }
        }

        public void Eliminar(int userId)
        {
            try
            {
                if (!File.Exists(rutaXML))
                    return;

                var doc = XDocument.Load(rutaXML);
                var element = doc.Root.Element("Usuarios")?
                                 .Elements("Usuario")
                                 .FirstOrDefault(x => (int)x.Attribute("Id") == userId);

                if (element != null)
                {
                    element.SetAttributeValue("Active", "false");
                    doc.Save(rutaXML);
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("No se pudo eliminar el usuario. " + ex.Message, ex);
            }
        }
    }
}
