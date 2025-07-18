using System.Xml.Linq;
using BE;
using Servicios.Utilidades;

namespace Mapper
{
    public class MPPComprobanteEntrega
    {
        private readonly string rutaXML = XmlPaths.BaseDatosLocal;

        public MPPComprobanteEntrega()
        {
            EnsureRoot();
        }

        private void EnsureRoot()
        {
            try
            {
                var dir = Path.GetDirectoryName(rutaXML);
                if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);

                if (!File.Exists(rutaXML))
                {
                    new XDocument(
                        new XElement("BaseDeDatosLocal",
                            new XElement("ComprobantesEntrega")
                        )
                    ).Save(rutaXML);
                }
                else
                {
                    var doc = XDocument.Load(rutaXML);
                    if (doc.Root.Element("ComprobantesEntrega") == null)
                    {
                        doc.Root.Add(new XElement("ComprobantesEntrega"));
                        doc.Save(rutaXML);
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        public int NextId()
        {
            try
            {
                var doc = XDocument.Load(rutaXML);
                var root = doc.Root.Element("ComprobantesEntrega");
                return root.Elements("ComprobanteEntrega")
                           .Select(x => (int)x.Attribute("Id"))
                           .DefaultIfEmpty(0)
                           .Max() + 1;
            }
            catch (Exception)
            {
                return 1;
            }
        }

        public void Alta(ComprobanteEntrega comprobante)
        {
            try
            {
                var doc = XDocument.Load(rutaXML);
                var root = doc.Root.Element("ComprobantesEntrega");

                comprobante.ID = NextId();
                comprobante.FechaEntrega = DateTime.Now;

                var elem = new XElement("ComprobanteEntrega",
                    new XAttribute("Id", comprobante.ID),
                    new XAttribute("Active", "true"),
                    new XElement("VentaId", comprobante.Venta.ID),
                    new XElement("FechaEntrega", comprobante.FechaEntrega.ToString("s"))
                );

                root.Add(elem);
                doc.Save(rutaXML);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("No se pudo dar de alta el comprobante de entrega. " + ex.Message, ex);
            }
        }

    }
}
