using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using BE;
using Servicios.Utilidades;

namespace Mapper
{
    public class MPPCliente
    {
        private readonly string rutaXML = XmlPaths.BaseDatosLocal;

        public List<Cliente> ListarTodo()
        {
            if (!File.Exists(rutaXML))
                return new List<Cliente>();

            var doc = XDocument.Load(rutaXML);
            var clientesRoot = doc.Root.Element("Clientes");
            if (clientesRoot == null)
                return new List<Cliente>();

            return clientesRoot
                .Elements("Cliente")
                .Where(x => (string)x.Attribute("Active") == "true")
                .Select(c => new Cliente
                {
                    ID = (int)c.Attribute("Id"),
                    Dni = c.Element("Dni").Value.Trim(),
                    Nombre = c.Element("Nombre").Value.Trim(),
                    Apellido = c.Element("Apellido").Value.Trim(),
                    Contacto = c.Element("Contacto").Value.Trim(),
                    FechaRegistro = DateTime.Parse(c.Element("FechaRegistro").Value.Trim())
                })
                .ToList();
        }

        public Cliente BuscarPorId(int id)
        {
            if (!File.Exists(rutaXML))
                return null;

            var doc = XDocument.Load(rutaXML);
            var elem = doc.Root
                .Element("Clientes")?
                .Elements("Cliente")
                .FirstOrDefault(x => (int)x.Attribute("Id") == id && (string)x.Attribute("Active") == "true");
            if (elem == null) return null;

            return new Cliente
            {
                ID = id,
                Dni = elem.Element("Dni").Value.Trim(),
                Nombre = elem.Element("Nombre").Value.Trim(),
                Apellido = elem.Element("Apellido").Value.Trim(),
                Contacto = elem.Element("Contacto").Value.Trim(),
                FechaRegistro = DateTime.Parse(elem.Element("FechaRegistro").Value.Trim())
            };
        }

        public Cliente BuscarPorDni(string dni)
        {
            if (!File.Exists(rutaXML))
                return null;

            var doc = XDocument.Load(rutaXML);
            var elem = doc.Root
                .Element("Clientes")?
                .Elements("Cliente")
                .FirstOrDefault(x =>
                    (string)x.Attribute("Active") == "true"
                    && string.Equals(x.Element("Dni").Value.Trim(), dni, StringComparison.OrdinalIgnoreCase));
            if (elem == null) return null;

            return new Cliente
            {
                ID = (int)elem.Attribute("Id"),
                Dni = elem.Element("Dni").Value.Trim(),
                Nombre = elem.Element("Nombre").Value.Trim(),
                Apellido = elem.Element("Apellido").Value.Trim(),
                Contacto = elem.Element("Contacto").Value.Trim(),
                FechaRegistro = DateTime.Parse(elem.Element("FechaRegistro").Value.Trim())
            };
        }

        public bool ExisteCliente(string dni)
        {
            return BuscarPorDni(dni) != null;
        }

        public void AltaCliente(Cliente cliente)
        {
            XDocument doc;
            if (File.Exists(rutaXML))
            {
                doc = XDocument.Load(rutaXML);
            }
            else
            {
                Directory.CreateDirectory(Path.GetDirectoryName(rutaXML));
                doc = new XDocument(new XElement("BaseDeDatosLocal", new XElement("Clientes")));
            }

            var root = doc.Root.Element("Clientes");
            int nuevoId = 1;
            if (root.Elements("Cliente").Any())
                nuevoId = root.Elements("Cliente").Max(x => (int)x.Attribute("Id")) + 1;

            cliente.ID = nuevoId;
            cliente.FechaRegistro = DateTime.Now;

            var nuevo = new XElement("Cliente",
                new XAttribute("Id", cliente.ID),
                new XAttribute("Active", "true"),
                new XElement("Dni", cliente.Dni),
                new XElement("Nombre", cliente.Nombre),
                new XElement("Apellido", cliente.Apellido),
                new XElement("Contacto", cliente.Contacto),
                new XElement("FechaRegistro", cliente.FechaRegistro.ToString("o"))
            );

            root.Add(nuevo);
            doc.Save(rutaXML);
        }

        public void ModificarCliente(Cliente cliente)
        {
            if (!File.Exists(rutaXML))
                throw new ApplicationException("Archivo no existe.");

            var doc = XDocument.Load(rutaXML);
            var elem = doc.Root
                .Element("Clientes")?
                .Elements("Cliente")
                .FirstOrDefault(x => (int)x.Attribute("Id") == cliente.ID);
            if (elem == null)
                throw new ApplicationException("Cliente no encontrado.");

            elem.Element("Dni").Value = cliente.Dni;
            elem.Element("Nombre").Value = cliente.Nombre;
            elem.Element("Apellido").Value = cliente.Apellido;
            elem.Element("Contacto").Value = cliente.Contacto;
            // FechaRegistro no se modifica

            doc.Save(rutaXML);
        }

        public void BajaCliente(int id)
        {
            if (!File.Exists(rutaXML))
                throw new ApplicationException("Archivo no existe.");

            var doc = XDocument.Load(rutaXML);
            var elem = doc.Root
                .Element("Clientes")?
                .Elements("Cliente")
                .FirstOrDefault(x => (int)x.Attribute("Id") == id);
            if (elem == null)
                throw new ApplicationException("Cliente no encontrado.");

            elem.SetAttributeValue("Active", "false");
            doc.Save(rutaXML);
        }
    }
}
