// Mapper/MPPVenta.cs
using BE;
using Servicios.Utilidades;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Mapper
{
    public class MPPVenta
    {
        private readonly string rutaXML = XmlPaths.BaseDatosLocal;

        public List<Venta> ListarTodo()
        {
            if (!File.Exists(rutaXML))
                return new List<Venta>();

            var doc = XDocument.Load(rutaXML);
            var ventasXml = doc.Root.Element("Ventas")?.Elements("Venta") ?? Enumerable.Empty<XElement>();

            var ventas = new List<Venta>();
            foreach (var x in ventasXml)
            {
                if (x.Attribute("Active")?.Value != "true") continue;

                var v = new Venta
                {
                    ID = (int)x.Attribute("Id"),
                    Estado = x.Element("Estado")?.Value,
                    Fecha = DateTime.Parse(x.Element("Fecha")?.Value ?? DateTime.Now.ToString()),
                    MotivoRechazo = x.Element("MotivoRechazo")?.Value
                };

                // Cargar Vendedor
                var vendId = (int?)x.Element("Vendedor")?.Attribute("Id");
                if (vendId.HasValue)
                    v.Vendedor = new MPPVendedor().BuscarPorId(vendId.Value);

                // Cargar Cliente
                var cliId = (int?)x.Element("Cliente")?.Attribute("Id");
                if (cliId.HasValue)
                    v.Cliente = new MPPCliente().BuscarPorId(cliId.Value);

                // Cargar Vehiculo
                var vehId = (int?)x.Element("Vehiculo")?.Attribute("Id");
                if (vehId.HasValue)
                    v.Vehiculo = new MPPVehiculo().BuscarPorId(vehId.Value);

                // Cargar Pago
                var pagoId = (int?)x.Element("Pago")?.Attribute("Id");
                if (pagoId.HasValue)
                    v.Pago = new MPPPago().BuscarPorId(pagoId.Value);

                ventas.Add(v);
            }

            return ventas;
        }

        public Venta BuscarPorId(int id) =>
            ListarTodo().FirstOrDefault(v => v.ID == id);

        public void AltaVenta(Venta venta)
        {
            XDocument doc;
            if (File.Exists(rutaXML))
                doc = XDocument.Load(rutaXML);
            else
                doc = new XDocument(new XElement("BaseDeDatosLocal", new XElement("Ventas")));

            var root = doc.Root.Element("Ventas");
            if (root == null)
            {
                root = new XElement("Ventas");
                doc.Root.Add(root);
            }

            var nuevo = new XElement("Venta",
                new XAttribute("Id", venta.ID),
                new XAttribute("Active", "true"),
                new XElement("Estado", venta.Estado),
                new XElement("Fecha", venta.Fecha),
                new XElement("MotivoRechazo", venta.MotivoRechazo ?? string.Empty),
                new XElement("Vendedor", new XAttribute("Id", venta.Vendedor.ID)),
                new XElement("Cliente", new XAttribute("Id", venta.Cliente.ID)),
                new XElement("Vehiculo", new XAttribute("Id", venta.Vehiculo.ID)),
                new XElement("Pago", new XAttribute("Id", venta.Pago.ID))
            );

            root.Add(nuevo);
            doc.Save(rutaXML);
        }

        public void GuardarLista(List<Venta> lista)
        {
            var doc = XDocument.Load(rutaXML);
            var root = doc.Root.Element("Ventas");
            root.RemoveAll();

            foreach (var venta in lista)
            {
                var elem = new XElement("Venta",
                    new XAttribute("Id", venta.ID),
                    new XAttribute("Active", "true"),
                    new XElement("Estado", venta.Estado),
                    new XElement("Fecha", venta.Fecha),
                    new XElement("MotivoRechazo", venta.MotivoRechazo ?? string.Empty),
                    new XElement("Vendedor", new XAttribute("Id", venta.Vendedor.ID)),
                    new XElement("Cliente", new XAttribute("Id", venta.Cliente.ID)),
                    new XElement("Vehiculo", new XAttribute("Id", venta.Vehiculo.ID)),
                    new XElement("Pago", new XAttribute("Id", venta.Pago.ID))
                );
                root.Add(elem);
            }

            doc.Save(rutaXML);
        }
    }
}
