using BE;
using System.Xml.Linq;
using Servicios.Utilidades;

namespace Mapper
{
    public class MPPVenta
    {
        private readonly string rutaXML = XmlPaths.BaseDatosLocal;

        public MPPVenta()
        {
            EnsureStructure();
        }

        // Se asegura que la estructura básica exista en el XML.
        private void EnsureStructure()
        {
            try
            {
                var dir = Path.GetDirectoryName(rutaXML);
                if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);

                if (!File.Exists(rutaXML))
                {
                    new XDocument(
                        new XElement("BaseDeDatosLocal",
                            new XElement("Ventas")
                        )
                    ).Save(rutaXML);
                }
                else
                {
                    var doc = XDocument.Load(rutaXML);
                    if (doc.Root.Element("Ventas") == null)
                    {
                        doc.Root.Add(new XElement("Ventas"));
                        doc.Save(rutaXML);
                    }
                }
            }
            catch (Exception)
            {
                // Si falla la creacin, no hago nada
            }
        }

        // Devuelve todas las ventas activas.
        public List<Venta> ListarTodo()
        {
            try
            {
                var doc = XDocument.Load(rutaXML);
                var root = doc.Root.Element("Ventas");
                if (root == null) return new();

                return root.Elements("Venta")
                           .Where(x => (string)x.Attribute("Active") == "true")
                           .Select(ParseVenta)
                           .ToList();
            }
            catch (Exception)
            {
                return new List<Venta>();
            }
        }

        // Busca una venta por ID.
        public Venta BuscarPorId(int id)
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

        // Da de alta una nueva venta.
        public void Alta(Venta venta)
        {
            try
            {
                var doc = XDocument.Load(rutaXML);
                var root = doc.Root.Element("Ventas");

                venta.ID = root.Elements("Venta")
                               .Select(x => (int)x.Attribute("Id"))
                               .DefaultIfEmpty(0)
                               .Max() + 1;
                venta.Fecha = DateTime.Now;
                venta.Estado = "Pendiente";

                var elem = new XElement("Venta",
                    new XAttribute("Id", venta.ID),
                    new XAttribute("Active", "true"),
                    new XElement("VendedorId", venta.Vendedor.ID),
                    new XElement("ClienteId", venta.Cliente.ID),
                    new XElement("VehiculoId", venta.Vehiculo.ID),
                    new XElement("PagoId", venta.Pago.ID),
                    new XElement("Estado", venta.Estado),
                    new XElement("Fecha", venta.Fecha.ToString("s")),
                    new XElement("MotivoRechazo", venta.MotivoRechazo ?? string.Empty)
                );

                root.Add(elem);
                doc.Save(rutaXML);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("No se pudo dar de alta la venta. " + ex.Message, ex);
            }
        }

        // Cambia el estado de una venta.
        public void ActualizarEstado(int id, string nuevoEstado, string motivo = null)
        {
            try
            {
                var doc = XDocument.Load(rutaXML);
                var ventaElem = doc.Root.Element("Ventas")?
                                      .Elements("Venta")
                                      .FirstOrDefault(x => (int)x.Attribute("Id") == id);
                if (ventaElem == null) throw new ApplicationException("Venta no encontrada.");

                ventaElem.SetElementValue("Estado", nuevoEstado);
                if (motivo != null)
                    ventaElem.SetElementValue("MotivoRechazo", motivo);

                doc.Save(rutaXML);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("No se pudo actualizar el estado de la venta. " + ex.Message, ex);
            }
        }

        private Venta ParseVenta(XElement x)
        {
            return new Venta
            {
                ID = (int)x.Attribute("Id"),
                Vendedor = new Vendedor { ID = (int)x.Element("VendedorId") },
                Cliente = new Cliente { ID = (int)x.Element("ClienteId") },
                Vehiculo = new Vehiculo { ID = (int)x.Element("VehiculoId") },
                Pago = new Pago { ID = (int)x.Element("PagoId") },
                Estado = (string)x.Element("Estado"),
                Fecha = DateTime.Parse(x.Element("Fecha")?.Value ?? DateTime.Now.ToString("s")),
                MotivoRechazo = (string)x.Element("MotivoRechazo")
            };
        }
    }
}
