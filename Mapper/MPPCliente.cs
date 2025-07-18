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

        // Asegura que el archivo y la sección Clientes existan.
        private void AsegurarArchivo()
        {
            try
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
            catch (Exception)
            {
                // Si falla la creación del archivo, no hago nada (puede fallar por permisos o ruta)
            }
        }

        // Devuelve todos los clientes activos.
        public List<Cliente> ListarTodo()
        {
            var clientes = new List<Cliente>();
            try
            {
                var doc = XDocument.Load(rutaXML);
                var root = doc.Root.Element("Clientes");
                if (root == null) return clientes;

                clientes = root.Elements("Cliente")
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
            catch (Exception)
            {
            }
            return clientes;
        }

        // Busca un cliente por ID.
        public Cliente BuscarPorId(int id)
        {
            try
            {
                return ListarTodo().FirstOrDefault(c => c.ID == id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        // Busca un cliente por DNI.
        public Cliente BuscarPorDni(string dni)
        {
            try
            {
                return ListarTodo()
                       .FirstOrDefault(c => c.Dni.Equals(dni, StringComparison.OrdinalIgnoreCase));
            }
            catch (Exception)
            {
                return null;
            }
        }

        // Da de alta un nuevo cliente.
        public void Alta(Cliente cliente)
        {
            try
            {
                var doc = XDocument.Load(rutaXML);

                var root = doc.Root.Element("Clientes");
                if (root == null)
                {
                    root = new XElement("Clientes");
                    doc.Root.Add(root);
                }

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
            catch (Exception ex)
            {
                throw new ApplicationException("No se pudo dar de alta el cliente. " + ex.Message, ex);
            }
        }
    }
}
