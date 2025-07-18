using System.Xml.Linq;
using BE;
using Servicios.Utilidades;

namespace Mapper
{
    public class MPPPago
    {
        private readonly string rutaXML = XmlPaths.BaseDatosLocal;

        public MPPPago()
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
                            new XElement("Pagos")
                        )
                    ).Save(rutaXML);
                }
            }
            catch (Exception)
            {
            }
        }

        public List<Pago> ListarTodo()
        {
            try
            {
                var doc = XDocument.Load(rutaXML);
                var root = doc.Root.Element("Pagos");
                if (root == null) return new List<Pago>();

                return root.Elements("Pago")
                           .Where(x => (string)x.Attribute("Active") == "true")
                           .Select(x => new Pago
                           {
                               ID = (int)x.Attribute("Id"),
                               TipoPago = (string)x.Element("TipoPago"),
                               Monto = decimal.Parse(x.Element("Monto")?.Value ?? "0"),
                               Cuotas = int.Parse(x.Element("Cuotas")?.Value ?? "0"),
                               Detalles = (string)x.Element("Detalles") ?? "",
                               FechaPago = DateTime.Parse(x.Element("FechaPago")?.Value ?? DateTime.Now.ToString("s"))
                           })
                           .ToList();
            }
            catch (Exception)
            {
                return new List<Pago>();
            }
        }

        public Pago BuscarPorId(int id)
        {
            try
            {
                return ListarTodo().FirstOrDefault(p => p.ID == id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void Alta(Pago pago)
        {
            try
            {
                var doc = XDocument.Load(rutaXML);
                var root = doc.Root.Element("Pagos");

                if (root == null)
                {
                    root = new XElement("Pagos");
                    doc.Root.Add(root);
                }

                int nextId = root.Elements("Pago")
                                 .Select(x => (int)x.Attribute("Id"))
                                 .DefaultIfEmpty(0)
                                 .Max() + 1;

                pago.ID = nextId;
                pago.FechaPago = DateTime.Now;

                var elem = new XElement("Pago",
                    new XAttribute("Id", nextId),
                    new XAttribute("Active", "true"),
                    new XElement("TipoPago", pago.TipoPago),
                    new XElement("Monto", pago.Monto),
                    new XElement("Cuotas", pago.Cuotas),
                    new XElement("Detalles", pago.Detalles ?? ""),
                    new XElement("FechaPago", pago.FechaPago.ToString("s"))
                );

                root.Add(elem);
                doc.Save(rutaXML);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("No se pudo dar de alta el pago. " + ex.Message, ex);
            }
        }
    }
}
