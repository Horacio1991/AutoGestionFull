using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using BE;
using Servicios.Utilidades;

namespace Mapper
{
    public class MPPFactura
    {
        private readonly string rutaXML = XmlPaths.BaseDatosLocal;

        private XDocument LoadOrEmpty()
        {
            return File.Exists(rutaXML)
                ? XDocument.Load(rutaXML)
                : new XDocument(new XElement("BaseDeDatosLocal"));
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

        public List<Factura> ListarTodo()
        {
            var doc = LoadOrEmpty();
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

        public Factura BuscarPorId(int id) =>
            ListarTodo().FirstOrDefault(f => f.ID == id);

        public void AltaFactura(Factura factura)
        {
            var doc = LoadOrEmpty();
            var root = EnsureFacturasRoot(doc);

            int nuevoId = root.Elements("Factura")
                              .Select(e => (int)e.Attribute("Id"))
                              .DefaultIfEmpty(0)
                              .Max() + 1;
            factura.ID = nuevoId;
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

        /// <summary>
        /// Marca la venta asociada como facturada (cambia su estado en Ventas XML).
        /// </summary>
        public void MarcarFacturada(int ventaId)
        {
            var doc = LoadOrEmpty();
            var ventasRoot = doc.Root.Element("Ventas");
            var ventaElem = ventasRoot?
                .Elements("Venta")
                .FirstOrDefault(x => (int)x.Attribute("Id") == ventaId);
            if (ventaElem == null) throw new ApplicationException("Venta no encontrada.");

            ventaElem.SetElementValue("Estado", "Facturada");
            doc.Save(rutaXML);
        }
    }
}
