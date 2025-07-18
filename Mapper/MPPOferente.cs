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
            try
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
            catch (Exception)
            {
            }
        }

        public List<Oferente> ListarTodo()
        {
            try
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
            catch (Exception)
            {
                return new List<Oferente>();
            }
        }

        public Oferente BuscarPorDni(string dni)
        {
            try
            {
                return ListarTodo()
                       .FirstOrDefault(o => string.Equals(o.Dni, dni, StringComparison.OrdinalIgnoreCase));
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool Existe(string dni)
        {
            try
            {
                return BuscarPorDni(dni) != null;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void Alta(Oferente oferente)
        {
            try
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
            catch (Exception ex)
            {
                throw new ApplicationException("No se pudo dar de alta el oferente. " + ex.Message, ex);
            }
        }

    }
}
