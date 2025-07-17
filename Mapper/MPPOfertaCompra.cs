using System.Xml.Linq;
using BE;
using Servicios.Utilidades;

namespace Mapper
{
    public class MPPOfertaCompra
    {
        private readonly string rutaXML = XmlPaths.BaseDatosLocal;

        public MPPOfertaCompra()
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
                new XDocument(
                    new XElement("BaseDeDatosLocal",
                        new XElement("OfertaCompras")
                    )
                ).Save(rutaXML);
            }
        }

        public List<OfertaCompra> ListarTodo()
        {
            if (!File.Exists(rutaXML))
                return new List<OfertaCompra>();

            var doc = XDocument.Load(rutaXML);
            var root = doc.Root.Element("OfertasCompra");
            if (root == null) return new List<OfertaCompra>();

            return root
                .Elements("OfertaCompra")
                .Where(x => (string)x.Attribute("Active") == "true")
                .Select(x => new OfertaCompra
                {
                    ID = (int)x.Attribute("Id"),
                    FechaInspeccion = DateTime.Parse(x.Element("FechaInspeccion")?.Value ?? DateTime.Now.ToString("s")),
                    Estado = (string)x.Element("Estado"),
                    // Cargamos Vehiculo solo con ID; el BLL completará marca/modelo
                    Vehiculo = new Vehiculo { ID = (int)x.Element("Vehiculo").Attribute("Id") }
                })
                .ToList();
        }
        public OfertaCompra BuscarPorId(int id)
        {
            return ListarTodo().FirstOrDefault(o => o.ID == id);
        }

        public List<OfertaCompra> BuscarPorDominio(string dominio)
        {
            return ListarTodo()
                   .Where(o => o.Vehiculo != null &&
                               !string.IsNullOrEmpty(o.Vehiculo.Dominio) &&
                               o.Vehiculo.Dominio.Equals(dominio, StringComparison.OrdinalIgnoreCase))
                   .ToList();
        }

        public void Alta(OfertaCompra oferta)
        {
            var doc = XDocument.Load(rutaXML);

            // Asegurarnos de que exista el contenedor <OfertaCompras>
            var root = doc.Root.Element("OfertaCompras");
            if (root == null)
            {
                root = new XElement("OfertaCompras");
                doc.Root.Add(root);
            }

            // Calcular el próximo Id
            int nextId = root.Elements("OfertaCompra")
                             .Select(x => (int)x.Attribute("Id"))
                             .DefaultIfEmpty(0)
                             .Max() + 1;
            oferta.ID = nextId;

            // Construir el elemento XML
            var elem = new XElement("OfertaCompra",
                new XAttribute("Id", oferta.ID),
                new XAttribute("Active", "true"),
                new XElement("OferenteId", oferta.Oferente.ID),
                new XElement("VehiculoId", oferta.Vehiculo.ID),
                new XElement("FechaInspeccion", oferta.FechaInspeccion.ToString("s")),
                new XElement("Estado", oferta.Estado)
            );

            // Agregar y guardar
            root.Add(elem);
            doc.Save(rutaXML);
        }

        public void MarcarTasada(int ofertaId)
        {
            var doc = XDocument.Load(rutaXML);
            var root = doc.Root.Element("OfertaCompras");
            var elem = root?.Elements("OfertaCompra")
                            .FirstOrDefault(x => (int)x.Attribute("Id") == ofertaId);
            if (elem == null)
                throw new ApplicationException("Oferta no encontrada.");
            elem.SetAttributeValue("Estado", "Tasada");
            doc.Save(rutaXML);
        }

    }
}
