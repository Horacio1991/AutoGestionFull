using BE;
using System.Xml.Linq;
using Servicios.Utilidades;

namespace Mapper
{
    public class MPPFactura
    {
        private readonly string rutaXML = XmlPaths.BaseDatosLocal;

        private XDocument LoadOrCreate()
        {
            try
            {
                if (!File.Exists(rutaXML))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(rutaXML));
                    new XDocument(new XElement("BaseDeDatosLocal")).Save(rutaXML);
                }
                return XDocument.Load(rutaXML);
            }
            catch (Exception)
            {
                // Devuelve un doc vacío si falla la carga/creación
                return new XDocument(new XElement("BaseDeDatosLocal"));
            }
        }

        private XElement EnsureFacturasRoot(XDocument doc)
        {
            var root = doc.Root.Element("Facturas");
            if (root == null)
            {
                root = new XElement("Facturas");
                doc.Root.Add(root);
            }
            return root;
        }

        public void AltaFactura(Factura factura)
        {
            try
            {
                var doc = LoadOrCreate();
                var root = EnsureFacturasRoot(doc);

                factura.ID = root.Elements("Factura")
                                 .Select(e => (int)e.Attribute("Id"))
                                 .DefaultIfEmpty(0)
                                 .Max() + 1;
                factura.Fecha = DateTime.Now;

                var elem = new XElement("Factura",
                    new XAttribute("Id", factura.ID),
                    new XAttribute("Active", "true"),
                    new XElement("ClienteId", factura.Cliente.ID),
                    new XElement("VehiculoId", factura.Vehiculo.ID),
                    new XElement("FormaPago", factura.FormaPago),
                    new XElement("Precio", factura.Precio),
                    new XElement("Fecha", factura.Fecha.ToString("s"))
                );

                root.Add(elem);
                doc.Save(rutaXML);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("No se pudo dar de alta la factura. " + ex.Message, ex);
            }
        }

        public List<Factura> ListarTodo()
        {
            try
            {
                var doc = LoadOrCreate();
                var elems = doc.Root.Element("Facturas")?
                            .Elements("Factura")
                            .Where(x => (string)x.Attribute("Active") == "true")
                          ?? Enumerable.Empty<XElement>();

                return elems.Select(x => new Factura
                {
                    ID = (int)x.Attribute("Id"),
                    Cliente = new Cliente { ID = (int)x.Element("ClienteId") },
                    Vehiculo = new Vehiculo { ID = (int)x.Element("VehiculoId") },
                    FormaPago = (string)x.Element("FormaPago"),
                    Precio = decimal.Parse(x.Element("Precio")?.Value ?? "0"),
                    Fecha = DateTime.Parse(x.Element("Fecha")?.Value ?? DateTime.Now.ToString())
                }).ToList();
            }
            catch (Exception)
            {
                return new List<Factura>();
            }
        }

        public void MarcarFacturada(int ventaId)
        {
            try
            {
                var doc = LoadOrCreate();
                var ventaElem = doc.Root.Element("Ventas")
                                      ?.Elements("Venta")
                                      .FirstOrDefault(x => (int)x.Attribute("Id") == ventaId);
                if (ventaElem == null) throw new ApplicationException("Venta no encontrada.");

                ventaElem.SetElementValue("Estado", "Facturada");
                doc.Save(rutaXML);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("No se pudo marcar la venta como facturada. " + ex.Message, ex);
            }
        }
    }
}
