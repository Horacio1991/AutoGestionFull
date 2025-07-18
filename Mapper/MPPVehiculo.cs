using System.Xml.Linq;
using BE;
using Servicios.Utilidades;

namespace Mapper
{
    public class MPPVehiculo
    {
        private readonly string rutaXML = XmlPaths.BaseDatosLocal;

        // Asegura que el archivo y la sección Vehiculos existan.
        private XDocument LoadOrEmpty()
        {
            try
            {
                if (!File.Exists(rutaXML))
                    return new XDocument(new XElement("BaseDeDatosLocal", new XElement("Vehiculos")));

                var doc = XDocument.Load(rutaXML);
                if (doc.Root.Element("Vehiculos") == null)
                    doc.Root.Add(new XElement("Vehiculos"));
                return doc;
            }
            catch (Exception)
            {
                // Error al cargar archivo: retorna estructura vacía
                return new XDocument(new XElement("BaseDeDatosLocal", new XElement("Vehiculos")));
            }
        }

        private IEnumerable<XElement> RootVehiculos(XDocument doc)
        {
            var root = doc.Root.Element("Vehiculos");
            if (root == null) yield break;
            foreach (var x in root.Elements("Vehiculo"))
                yield return x;
        }

        // Devuelve todos los vehículos activos.
        public List<Vehiculo> ListarTodo()
        {
            try
            {
                var doc = LoadOrEmpty();
                return RootVehiculos(doc)
                    .Where(x => (string)x.Attribute("Active") == "true")
                    .Select(ParseVehiculo)
                    .ToList();
            }
            catch (Exception)
            {
                return new List<Vehiculo>();
            }
        }

        // Devuelve solo vehículos con estado "Disponible"
        public List<Vehiculo> ListarDisponibles()
        {
            try
            {
                return ListarTodo()
                       .Where(v => string.Equals(v.Estado, "Disponible", StringComparison.OrdinalIgnoreCase))
                       .ToList();
            }
            catch (Exception)
            {
                return new List<Vehiculo>();
            }
        }

        public Vehiculo BuscarPorId(int id)
        {
            try
            {
                return ListarTodo().FirstOrDefault(v => v.ID == id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        // Da de alta un nuevo vehículo
        public void Alta(Vehiculo v)
        {
            try
            {
                var doc = LoadOrEmpty();
                var root = doc.Root.Element("Vehiculos");

                // Calcular nuevo ID
                int nextId = root.Elements("Vehiculo")
                                 .Select(x => (int)x.Attribute("Id"))
                                 .DefaultIfEmpty(0)
                                 .Max() + 1;
                v.ID = nextId;

                // Crear elemento
                var elem = new XElement("Vehiculo",
                    new XAttribute("Id", v.ID),
                    new XAttribute("Active", "true"),
                    new XElement("Marca", v.Marca),
                    new XElement("Modelo", v.Modelo),
                    new XElement("Año", v.Año),
                    new XElement("Color", v.Color),
                    new XElement("Km", v.Km),
                    new XElement("Dominio", v.Dominio),
                    new XElement("Estado", v.Estado)
                );
                root.Add(elem);

                doc.Save(rutaXML);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("No se pudo dar de alta el vehículo. " + ex.Message, ex);
            }
        }

        // Actualiza todos los datos de un vehículo
        public void Actualizar(Vehiculo vehiculo)
        {
            try
            {
                var doc = XDocument.Load(rutaXML);
                var root = doc.Root.Element("Vehiculos")
                           ?? throw new ApplicationException("Sección 'Vehiculos' no encontrada en el XML.");

                var elem = root
                    .Elements("Vehiculo")
                    .FirstOrDefault(x => (int)x.Attribute("Id") == vehiculo.ID);

                if (elem == null)
                    throw new ApplicationException($"Vehículo con Id={vehiculo.ID} no encontrado.");

                elem.SetElementValue("Marca", vehiculo.Marca);
                elem.SetElementValue("Modelo", vehiculo.Modelo);
                elem.SetElementValue("Año", vehiculo.Año);
                elem.SetElementValue("Color", vehiculo.Color);
                elem.SetElementValue("Km", vehiculo.Km);
                elem.SetElementValue("Dominio", vehiculo.Dominio);
                elem.SetElementValue("Estado", vehiculo.Estado);

                doc.Save(rutaXML);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("No se pudo actualizar el vehículo. " + ex.Message, ex);
            }
        }
        private Vehiculo ParseVehiculo(XElement x) => new Vehiculo
        {
            ID = (int)x.Attribute("Id"),
            Marca = x.Element("Marca")?.Value,
            Modelo = x.Element("Modelo")?.Value,
            Año = (int?)x.Element("Año") ?? 0,
            Color = x.Element("Color")?.Value,
            Km = (int?)x.Element("Km") ?? 0,
            Dominio = x.Element("Dominio")?.Value,
            Estado = x.Element("Estado")?.Value
        };
    }
}
