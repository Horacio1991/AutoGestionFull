using System.Xml.Linq;
using BE;
using Servicios.Utilidades;

namespace Mapper
{
    public class MPPOferente
    {
        private readonly string rutaXML = XmlPaths.BaseDatosLocal;

        public MPPOferente()
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
                    new XElement("Oferentes"))).Save(rutaXML);
            }
        }

        public List<Oferente> ListarTodo()
        {
            var doc = XDocument.Load(rutaXML);
            var root = doc.Root.Element("Oferentes");
            if (root == null) return new List<Oferente>();

            return root.Elements("Oferente")
                       .Where(x => (string)x.Attribute("Active") == "true")
                       .Select(x => new Oferente
                       {
                           ID = (int)x.Attribute("Id"),
                           Dni = (string)x.Element("Dni"),
                           Nombre = (string)x.Element("Nombre"),
                           Apellido = (string)x.Element("Apellido"),
                           Contacto = (string)x.Element("Contacto")
                       })
                       .ToList();
        }

        public Oferente BuscarPorId(int id)
        {
            return ListarTodo().FirstOrDefault(o => o.ID == id);
        }

        public Oferente BuscarPorDni(string dni)
        {
            return ListarTodo()
                   .FirstOrDefault(o => string.Equals(o.Dni, dni, StringComparison.OrdinalIgnoreCase));
        }

        public bool Existe(string dni)
        {
            return BuscarPorDni(dni) != null;
        }

        public void Alta(Oferente oferente)
        {
            var doc = XDocument.Load(rutaXML);

            // Asegurar nodo 'Oferentes'
            var root = doc.Root.Element("Oferentes");
            if (root == null)
            {
                root = new XElement("Oferentes");
                doc.Root.Add(root);
            }

            int nextId = root.Elements("Oferente")
                             .Select(x => (int)x.Attribute("Id"))
                             .DefaultIfEmpty(0)
                             .Max() + 1;

            oferente.ID = nextId;

            var elem = new XElement("Oferente",
                new XAttribute("Id", nextId),
                new XAttribute("Active", "true"),
                new XElement("Dni", oferente.Dni),
                new XElement("Nombre", oferente.Nombre),
                new XElement("Apellido", oferente.Apellido),
                new XElement("Contacto", oferente.Contacto)
            );

            root.Add(elem);
            doc.Save(rutaXML);
        }


        public void Modificar(Oferente oferente)
        {
            var doc = XDocument.Load(rutaXML);
            var elem = doc.Root.Element("Oferentes")
                          .Elements("Oferente")
                          .FirstOrDefault(x => (int)x.Attribute("Id") == oferente.ID);
            if (elem == null) throw new ApplicationException("Oferente no encontrado.");

            elem.Element("Dni").SetValue(oferente.Dni);
            elem.Element("Nombre").SetValue(oferente.Nombre);
            elem.Element("Apellido").SetValue(oferente.Apellido);
            elem.Element("Contacto").SetValue(oferente.Contacto);

            doc.Save(rutaXML);
        }

        public void Baja(int id)
        {
            var doc = XDocument.Load(rutaXML);
            var elem = doc.Root.Element("Oferentes")
                          .Elements("Oferente")
                          .FirstOrDefault(x => (int)x.Attribute("Id") == id);
            if (elem == null) throw new ApplicationException("Oferente no encontrado.");

            elem.SetAttributeValue("Active", "false");
            doc.Save(rutaXML);
        }
    }
}
