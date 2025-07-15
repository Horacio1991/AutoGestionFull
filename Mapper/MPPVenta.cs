using System.Xml.Linq;
using BE;
using Servicios.Utilidades;

namespace Mapper
{
    public class MPPVenta
    {
        private readonly string rutaXML = XmlPaths.BaseDatosLocal;

        public MPPVenta()
        {
            EnsureRoot();
        }

        private void EnsureRoot()
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

        public List<Venta> ListarTodo()
        {
            var doc = XDocument.Load(rutaXML);
            var root = doc.Root.Element("Ventas");
            if (root == null) return new();

            return root.Elements("Venta")
                       .Where(x => (string)x.Attribute("Active") == "true")
                       .Select(x => ParseVenta(x))
                       .ToList();
        }

        public Venta BuscarPorId(int id)
        {
            return ListarTodo().FirstOrDefault(v => v.ID == id);
        }

        public int NextId()
        {
            var doc = XDocument.Load(rutaXML);
            var root = doc.Root.Element("Ventas");
            return root.Elements("Venta")
                       .Select(x => (int)x.Attribute("Id"))
                       .DefaultIfEmpty(0)
                       .Max() + 1;
        }

        public void Alta(Venta venta)
        {
            var doc = XDocument.Load(rutaXML);
            var root = doc.Root.Element("Ventas");

            venta.ID = NextId();
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
                new XElement("MotivoRechazo", venta.MotivoRechazo ?? String.Empty)
            );

            root.Add(elem);
            doc.Save(rutaXML);
        }

        public void ActualizarEstado(int id, string nuevoEstado, string motivo = null)
        {
            var doc = XDocument.Load(rutaXML);
            var root = doc.Root.Element("Ventas");
            var x = root?.Elements("Venta")
                            .FirstOrDefault(v => (int)v.Attribute("Id") == id);
            if (x == null) throw new ApplicationException("Venta no encontrada.");

            x.SetElementValue("Estado", nuevoEstado);
            if (motivo != null)
                x.SetElementValue("MotivoRechazo", motivo);

            doc.Save(rutaXML);
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
