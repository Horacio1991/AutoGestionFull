using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        public List<ComprobanteEntrega> ListarTodo()
        {
            var doc = XDocument.Load(rutaXML);
            var root = doc.Root.Element("ComprobantesEntrega");
            if (root == null) return new();

            return root.Elements("ComprobanteEntrega")
                       .Where(x => (string)x.Attribute("Active") == "true")
                       .Select(Parse)
                       .ToList();
        }

        public ComprobanteEntrega BuscarPorId(int id)
            => ListarTodo().FirstOrDefault(c => c.ID == id);

        public List<ComprobanteEntrega> ListarPorVenta(int ventaId)
            => ListarTodo().Where(c => c.Venta.ID == ventaId).ToList();

        public int NextId()
        {
            var doc = XDocument.Load(rutaXML);
            var root = doc.Root.Element("ComprobantesEntrega");
            return root.Elements("ComprobanteEntrega")
                       .Select(x => (int)x.Attribute("Id"))
                       .DefaultIfEmpty(0)
                       .Max() + 1;
        }

        public void Alta(ComprobanteEntrega comprobante)
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

        private ComprobanteEntrega Parse(XElement x) => new ComprobanteEntrega
        {
            ID = (int)x.Attribute("Id"),
            Venta = new Venta { ID = (int)x.Element("VentaId") },
            FechaEntrega = DateTime.Parse(x.Element("FechaEntrega")?.Value ?? DateTime.Now.ToString("s"))
        };
    }
}
