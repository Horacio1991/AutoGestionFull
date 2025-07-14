// Mapper/MPPComprobanteEntrega.cs
using BE;
using Servicios.Utilidades;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Mapper
{
    public class MPPComprobanteEntrega
    {
        private readonly string rutaXML = XmlPaths.BaseDatosLocal;

        public List<ComprobanteEntrega> ListarTodo()
        {
            if (!File.Exists(rutaXML))
                return new List<ComprobanteEntrega>();

            var doc = XDocument.Load(rutaXML);
            var elems = doc.Root.Element("ComprobantesEntrega")?
                        .Elements("ComprobanteEntrega")
                        .Where(x => x.Attribute("Active")?.Value == "true")
                      ?? Enumerable.Empty<XElement>();

            var lista = new List<ComprobanteEntrega>();
            foreach (var x in elems)
            {
                var c = new ComprobanteEntrega
                {
                    ID = (int)x.Attribute("Id"),
                    FechaEntrega = DateTime.Parse(x.Element("FechaEntrega")?.Value ?? DateTime.Now.ToString())
                };

                var ventaIdAttr = x.Element("Venta")?.Attribute("Id");
                if (ventaIdAttr != null && int.TryParse(ventaIdAttr.Value, out var vid))
                    c.Venta = new MPPVenta().BuscarPorId(vid);

                lista.Add(c);
            }

            return lista;
        }

        public ComprobanteEntrega BuscarPorId(int id) =>
            ListarTodo().FirstOrDefault(c => c.ID == id);

        public void AltaComprobante(ComprobanteEntrega comp)
        {
            XDocument doc;
            if (File.Exists(rutaXML))
                doc = XDocument.Load(rutaXML);
            else
                doc = new XDocument(new XElement("BaseDeDatosLocal", new XElement("ComprobantesEntrega")));

            var root = doc.Root.Element("ComprobantesEntrega") ?? new XElement("ComprobantesEntrega");
            if (doc.Root.Element("ComprobantesEntrega") == null)
                doc.Root.Add(root);

            var elem = new XElement("ComprobanteEntrega",
                new XAttribute("Id", comp.ID),
                new XAttribute("Active", "true"),
                new XElement("FechaEntrega", comp.FechaEntrega),
                new XElement("Venta", new XAttribute("Id", comp.Venta.ID))
            );

            root.Add(elem);
            doc.Save(rutaXML);
        }

        public void GuardarLista(List<ComprobanteEntrega> lista)
        {
            var doc = XDocument.Load(rutaXML);
            var root = doc.Root.Element("ComprobantesEntrega");
            root.RemoveAll();

            foreach (var comp in lista)
            {
                var elem = new XElement("ComprobanteEntrega",
                    new XAttribute("Id", comp.ID),
                    new XAttribute("Active", "true"),
                    new XElement("FechaEntrega", comp.FechaEntrega),
                    new XElement("Venta", new XAttribute("Id", comp.Venta.ID))
                );
                root.Add(elem);
            }

            doc.Save(rutaXML);
        }
    }
}
