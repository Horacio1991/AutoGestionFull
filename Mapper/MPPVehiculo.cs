using System.Xml.Linq;
using BE;
using Servicios.Utilidades;

namespace Mapper
{
    public class MPPVehiculo
    {
        private readonly string rutaXML = XmlPaths.BaseDatosLocal;

        private XDocument LoadOrEmpty()
        {
            return File.Exists(rutaXML)
                ? XDocument.Load(rutaXML)
                : new XDocument(new XElement("BaseDeDatosLocal"));
        }

        private IEnumerable<XElement> RootVehiculos(XDocument doc)
        {
            var root = doc.Root.Element("Vehiculos");
            if (root == null) yield break;
            foreach (var x in root.Elements("Vehiculo")) yield return x;
        }

        public List<Vehiculo> ListarTodo()
        {
            var doc = LoadOrEmpty();
            return RootVehiculos(doc)
                .Where(x => (string)x.Attribute("Active") == "true")
                .Select(ParseVehiculo)
                .ToList();
        }

        public List<Vehiculo> ListarDisponibles()
        {
            return ListarTodo()
                .Where(v => string.Equals(v.Estado, "Disponible", StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public Vehiculo BuscarPorId(int id)
        {
            return ListarTodo().FirstOrDefault(v => v.ID == id);
        }

        public List<Vehiculo> BuscarPorModelo(string modelo)
        {
            return ListarDisponibles()
                .Where(v => v.Modelo.Equals(modelo, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public List<Vehiculo> BuscarPorMarca(string marca)
        {
            return ListarDisponibles()
                .Where(v => v.Marca.Equals(marca, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public void Actualizar(Vehiculo vehiculo)
        {
            // 1) Cargo el XML
            var doc = XDocument.Load(rutaXML);
            var root = doc.Root.Element("Vehiculos");
            if (root == null)
                throw new ApplicationException("Sección 'Vehiculos' no encontrada en el XML.");

            // 2) Busco el elemento por ID
            var elem = root
                .Elements("Vehiculo")
                .FirstOrDefault(x => (int)x.Attribute("Id") == vehiculo.ID);

            if (elem == null)
                throw new ApplicationException($"Vehículo con Id={vehiculo.ID} no encontrado.");

            // 3) Actualizo cada campo
            elem.SetElementValue("Marca", vehiculo.Marca);
            elem.SetElementValue("Modelo", vehiculo.Modelo);
            elem.SetElementValue("Año", vehiculo.Año);
            elem.SetElementValue("Color", vehiculo.Color);
            elem.SetElementValue("Km", vehiculo.Km);
            elem.SetElementValue("Dominio", vehiculo.Dominio);
            elem.SetElementValue("Estado", vehiculo.Estado);

            // 4) Guardo los cambios
            doc.Save(rutaXML);
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

        public Vehiculo BuscarPorDominio(string dominio)
        {
            return ListarTodo()
                .FirstOrDefault(v =>
                    v.Dominio.Equals(dominio, StringComparison.OrdinalIgnoreCase));
        }

        public void Alta(Vehiculo vehiculo)
        {
            var doc = LoadOrEmpty();
            var root = doc.Root.Element("Vehiculos");
            if (root == null)
            {
                root = new XElement("Vehiculos");
                doc.Root.Add(root);
            }
            int nextId = root.Elements("Vehiculo")
                             .Select(x => (int)x.Attribute("Id"))
                             .DefaultIfEmpty(0)
                             .Max() + 1;
            vehiculo.ID = nextId;

            var elem = new XElement("Vehiculo",
                new XAttribute("Id", vehiculo.ID),
                new XAttribute("Active", "true"),
                new XElement("Marca", vehiculo.Marca),
                new XElement("Modelo", vehiculo.Modelo),
                new XElement("Año", vehiculo.Año),
                new XElement("Color", vehiculo.Color),
                new XElement("Km", vehiculo.Km),
                new XElement("Dominio", vehiculo.Dominio),
                new XElement("Estado", vehiculo.Estado)
            );
            root.Add(elem);
            doc.Save(rutaXML);
        }

    }
}
