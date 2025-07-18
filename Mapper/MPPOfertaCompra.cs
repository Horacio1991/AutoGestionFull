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
            try
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
            catch (Exception)
            {
            }
        }

        private XElement Root(XDocument doc)
        {
            var root = doc.Root.Element("OfertaCompras");
            if (root == null)
            {
                root = new XElement("OfertaCompras");
                doc.Root.Add(root);
            }
            return root;
        }

        public List<OfertaCompra> ListarTodo()
        {
            try
            {
                var doc = XDocument.Load(rutaXML);
                var root = doc.Root.Element("OfertaCompras");
                if (root == null) return new List<OfertaCompra>();

                return root
                    .Elements("OfertaCompra")
                    .Where(x => (string)x.Attribute("Active") == "true")
                    .Select(x => new OfertaCompra
                    {
                        ID = (int)x.Attribute("Id"),
                        FechaInspeccion = DateTime.Parse(x.Element("FechaInspeccion")?.Value ?? DateTime.Now.ToString("s")),
                        Estado = (string)x.Element("Estado"),
                        Vehiculo = new Vehiculo { ID = (int)x.Element("VehiculoId") }
                    })
                    .ToList();
            }
            catch (Exception)
            {
                return new List<OfertaCompra>();
            }
        }

        public OfertaCompra BuscarPorId(int id)
        {
            try
            {
                return ListarTodo().FirstOrDefault(o => o.ID == id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<OfertaCompra> BuscarPorDominio(string dominio)
        {
            try
            {
                return ListarTodo()
                    .Where(o => o.Vehiculo != null && string.Equals(
                        new MPPVehiculo().BuscarPorId(o.Vehiculo.ID)?.Dominio,
                        dominio, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }
            catch (Exception)
            {
                return new List<OfertaCompra>();
            }
        }

        public void Alta(OfertaCompra oferta)
        {
            try
            {
                var doc = XDocument.Load(rutaXML);
                var root = Root(doc);

                int nextId = root.Elements("OfertaCompra")
                                 .Select(x => (int)x.Attribute("Id"))
                                 .DefaultIfEmpty(0)
                                 .Max() + 1;
                oferta.ID = nextId;

                var elem = new XElement("OfertaCompra",
                    new XAttribute("Id", oferta.ID),
                    new XAttribute("Active", "true"),
                    new XElement("OferenteId", oferta.Oferente.ID),
                    new XElement("VehiculoId", oferta.Vehiculo.ID),
                    new XElement("FechaInspeccion", oferta.FechaInspeccion.ToString("s")),
                    new XElement("Estado", oferta.Estado)
                );

                root.Add(elem);
                doc.Save(rutaXML);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("No se pudo dar de alta la oferta de compra. " + ex.Message, ex);
            }
        }

    }
}
