// Mapper/MPPFactura.cs
using BE;
using Servicios.Utilidades;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Mapper
{
    public class MPPFactura
    {
        private readonly string rutaXML = XmlPaths.BaseDatosLocal;

        public List<Factura> ListarTodo()
        {
            if (!File.Exists(rutaXML))
                return new List<Factura>();

            var doc = XDocument.Load(rutaXML);
            var elems = doc.Root.Element("Facturas")?
                        .Elements("Factura")
                        .Where(x => x.Attribute("Active")?.Value == "true")
                      ?? Enumerable.Empty<XElement>();

            return elems.Select(x => new Factura
            {
                ID = (int)x.Attribute("Id"),
                Cliente = new Cliente { ID = (int)x.Element("ClienteId") },
                Vehiculo = new Vehiculo { ID = (int)x.Element("VehiculoId") },
                FormaPago = x.Element("FormaPago")?.Value,
                Precio = decimal.Parse(x.Element("Precio")?.Value ?? "0"),
                Fecha = DateTime.Parse(x.Element("Fecha")?.Value ?? DateTime.Now.ToString())
            }).ToList();
        }

        public Factura BuscarPorId(int id) =>
            ListarTodo().FirstOrDefault(f => f.ID == id);

        public void AltaFactura(Factura factura)
        {
            var doc = File.Exists(rutaXML)
                ? XDocument.Load(rutaXML)
                : new XDocument(new XElement("BaseDeDatosLocal", new XElement("Facturas")));
            var root = doc.Root.Element("Facturas") ?? new XElement("Facturas");
            if (doc.Root.Element("Facturas") == null)
                doc.Root.Add(root);

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
    }
}
