using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using BE;
using BE.BEComposite;
using Servicios.Utilidades;

namespace Mapper
{
    public class MPPUsuario
    {
        private readonly string rutaXML = XmlPaths.BaseDatosLocal;

        // Lista todos los usuarios activos
        public List<Usuario> ListarTodo()
        {
            if (!File.Exists(rutaXML))
                return new List<Usuario>();

            var doc = XDocument.Load(rutaXML);
            var usuariosNode = doc.Root.Element("Usuarios");
            if (usuariosNode == null)
                return new List<Usuario>();

            return usuariosNode
                .Elements("Usuario")
                .Where(x => (string)x.Attribute("Active") == "true")
                .Select(x => new Usuario
                {
                    Id = (int)x.Attribute("Id"),
                    Username = (string)x.Element("Username"),
                    Password = (string)x.Element("Password"),
                    Rol = new List<BEComponente>() // se completará luego
                })
                .ToList();
        }

        // Verifica que exista un usuario con ese username y password
        public bool VerificarUsuario(string username, string passwordEncriptado)
        {
            var u = BuscarPorUsername(username);
            return u != null && u.Password == passwordEncriptado;
        }

        // Busca uno por username
        public Usuario BuscarPorUsername(string username)
        {
            if (!File.Exists(rutaXML))
                return null;

            var doc = XDocument.Load(rutaXML);
            var element = doc.Root
                .Element("Usuarios")?
                .Elements("Usuario")
                .FirstOrDefault(x =>
                    (string)x.Attribute("Active") == "true" &&
                    string.Equals((string)x.Element("Username"), username, StringComparison.OrdinalIgnoreCase));

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

        // Registro de nuevo usuario
        public void RegistrarUsuario(Usuario user)
        {
            Agregar(user);
        }

        // Comprueba existencia de username
        public bool ExisteUsuario(string username)
            => BuscarPorUsername(username) != null;

        // Modifica usuario existente
        public void ModificarUsuario(string usernameOriginal, Usuario modificado)
        {
            var existing = BuscarPorUsername(usernameOriginal);
            if (existing == null)
                throw new InvalidOperationException("Usuario original no encontrado.");

            modificado.Id = existing.Id;
            Actualizar(modificado);
        }

        // Elimina (marca inactive) por username
        public void EliminarUsuario(string username)
        {
            var existing = BuscarPorUsername(username);
            if (existing != null)
                Eliminar(existing.Id);
        }

        // --- Métodos privados de CRUD en XML ---

        private void Agregar(Usuario user)
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

        private void Actualizar(Usuario user)
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

        private void Eliminar(int userId)
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
    }
}
