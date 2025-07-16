using System.Xml.Linq;
using BE;
using Servicios.Utilidades;

namespace Mapper
{
    public class MPPCliente
    {
        private readonly string rutaXML = XmlPaths.BaseDatosLocal;

        public MPPCliente()
        {
            AsegurarArchivo();
        }

        private void AsegurarArchivo()
        {
            var dir = Path.GetDirectoryName(rutaXML);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            if (!File.Exists(rutaXML))
            {
                new XDocument(new XElement("BaseDeDatosLocal",
                    new XElement("Clientes"))).Save(rutaXML);
            }
        }

        public List<Cliente> ListarTodo()
        {
            var doc = XDocument.Load(rutaXML);
            var root = doc.Root.Element("Clientes");
            if (root == null) return new List<Cliente>();

            return root.Elements("Cliente")
                       .Where(x => (string)x.Attribute("Active") == "true")
                       .Select(x => new Cliente
                       {
                           ID = (int)x.Attribute("Id"),
                           Dni = (string)x.Element("Dni"),
                           Nombre = (string)x.Element("Nombre"),
                           Apellido = (string)x.Element("Apellido"),
                           Contacto = (string)x.Element("Contacto"),
                           FechaRegistro = DateTime.Parse((string)x.Element("FechaRegistro"))
                       })
                       .ToList();
        }

        public Cliente BuscarPorId(int id)
        {
            return ListarTodo().FirstOrDefault(c => c.ID == id);
        }

        public Cliente BuscarPorDni(string dni)
        {
            return ListarTodo()
                   .FirstOrDefault(c => c.Dni.Equals(dni, StringComparison.OrdinalIgnoreCase));
        }

        public void Alta(Cliente cliente)
        {
            var doc = XDocument.Load(rutaXML);
            var root = doc.Root.Element("Clientes");

            int nextId = root.Elements("Cliente")
                             .Select(x => (int)x.Attribute("Id"))
                             .DefaultIfEmpty(0)
                             .Max() + 1;

            cliente.ID = nextId;
            cliente.FechaRegistro = DateTime.Now;

            var elem = new XElement("Cliente",
                new XAttribute("Id", nextId),
                new XAttribute("Active", "true"),
                new XElement("Dni", cliente.Dni),
                new XElement("Nombre", cliente.Nombre),
                new XElement("Apellido", cliente.Apellido),
                new XElement("Contacto", cliente.Contacto),
                new XElement("FechaRegistro", cliente.FechaRegistro.ToString("s"))
            );

            root.Add(elem);
            doc.Save(rutaXML);
        }

        public void Modificar(Cliente cliente)
        {
            var doc = XDocument.Load(rutaXML);
            var elem = doc.Root.Element("Clientes")
                           .Elements("Cliente")
                           .FirstOrDefault(x => (int)x.Attribute("Id") == cliente.ID);
            if (elem == null) throw new ApplicationException("Cliente no encontrado.");

            elem.Element("Dni").SetValue(cliente.Dni);
            elem.Element("Nombre").SetValue(cliente.Nombre);
            elem.Element("Apellido").SetValue(cliente.Apellido);
            elem.Element("Contacto").SetValue(cliente.Contacto);
            // FechaRegistro se conserva

            doc.Save(rutaXML);
        }

        public void Baja(int id)
        {
            var doc = XDocument.Load(rutaXML);
            var elem = doc.Root.Element("Clientes")
                           .Elements("Cliente")
                           .FirstOrDefault(x => (int)x.Attribute("Id") == id);
            if (elem == null) throw new ApplicationException("Cliente no encontrado.");

            elem.SetAttributeValue("Active", "false");
            doc.Save(rutaXML);
        }
    }
}
