using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using BE;
using Servicios.Utilidades;

namespace Mapper
{
    public class MPPPago
    {
        private readonly string rutaXML = XmlPaths.BaseDatosLocal;

        public List<Pago> ListarTodo()
        {
            if (!File.Exists(rutaXML))
                return new List<Pago>();

            var doc = XDocument.Load(rutaXML);
            var pagosRoot = doc.Root.Element("Pagos");
            if (pagosRoot == null)
                return new List<Pago>();

            return pagosRoot
                .Elements("Pago")
                .Where(x => (string)x.Attribute("Active") == "true")
                .Select(p => new Pago
                {
                    ID = (int)p.Attribute("Id"),
                    TipoPago = p.Element("TipoPago").Value.Trim(),
                    Monto = decimal.Parse(p.Element("Monto").Value.Trim()),
                    Cuotas = int.Parse(p.Element("Cuotas").Value.Trim()),
                    Detalles = p.Element("Detalles").Value.Trim(),
                    FechaPago = DateTime.Parse(p.Element("FechaPago").Value.Trim())
                })
                .ToList();
        }

        public Pago BuscarPorId(int id)
        {
            if (!File.Exists(rutaXML))
                return null;

            var doc = XDocument.Load(rutaXML);
            var elem = doc.Root
                .Element("Pagos")?
                .Elements("Pago")
                .FirstOrDefault(x => (int)x.Attribute("Id") == id && (string)x.Attribute("Active") == "true");
            if (elem == null) return null;

            return new Pago
            {
                ID = id,
                TipoPago = elem.Element("TipoPago").Value.Trim(),
                Monto = decimal.Parse(elem.Element("Monto").Value.Trim()),
                Cuotas = int.Parse(elem.Element("Cuotas").Value.Trim()),
                Detalles = elem.Element("Detalles").Value.Trim(),
                FechaPago = DateTime.Parse(elem.Element("FechaPago").Value.Trim())
            };
        }

        public void AltaPago(Pago pago)
        {
            XDocument doc;
            if (File.Exists(rutaXML))
                doc = XDocument.Load(rutaXML);
            else
            {
                Directory.CreateDirectory(Path.GetDirectoryName(rutaXML));
                doc = new XDocument(new XElement("BaseDeDatosLocal", new XElement("Pagos")));
            }

            var root = doc.Root.Element("Pagos");
            int nuevoId = 1;
            if (root.Elements("Pago").Any())
                nuevoId = root.Elements("Pago").Max(x => (int)x.Attribute("Id")) + 1;

            pago.ID = nuevoId;
            pago.FechaPago = DateTime.Now;

            var nuevo = new XElement("Pago",
                new XAttribute("Id", pago.ID),
                new XAttribute("Active", "true"),
                new XElement("TipoPago", pago.TipoPago),
                new XElement("Monto", pago.Monto),
                new XElement("Cuotas", pago.Cuotas),
                new XElement("Detalles", pago.Detalles),
                new XElement("FechaPago", pago.FechaPago.ToString("o"))
            );

            root.Add(nuevo);
            doc.Save(rutaXML);
        }
    }
}
