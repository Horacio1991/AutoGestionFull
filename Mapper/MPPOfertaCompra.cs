using BE;
using Servicios;
using Servicios.Utilidades;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Mapper
{
    public class MPPOfertaCompra
    {
        private readonly string _rutaXml = XmlPaths.BaseDatosLocal;
        private const string NodoRoot = "Ofertas";
        private const string NodoElemento = "Oferta";

        public List<OfertaCompra> ListarTodo()
        {
            if (!File.Exists(_rutaXml))
                return new List<OfertaCompra>();

            var doc = XDocument.Load(_rutaXml);
            var ofertasXml = doc.Root.Element(NodoRoot)?.Elements(NodoElemento) ?? Enumerable.Empty<XElement>();

            return ofertasXml
                .Where(x => (string)x.Attribute("Active") == "true")
                .Select(ParseOferta)
                .ToList();
        }

        public OfertaCompra BuscarPorId(int id)
        {
            if (!File.Exists(_rutaXml)) return null;
            var doc = XDocument.Load(_rutaXml);
            var elem = doc.Root.Element(NodoRoot)?
                .Elements(NodoElemento)
                .FirstOrDefault(x => (int)x.Attribute("Id") == id && (string)x.Attribute("Active") == "true");
            return elem == null ? null : ParseOferta(elem);
        }

        public void Alta(OfertaCompra oferta)
        {
            XDocument doc;
            if (File.Exists(_rutaXml))
                doc = XDocument.Load(_rutaXml);
            else
                doc = new XDocument(new XElement("BaseDeDatosLocal", new XElement(NodoRoot)));

            var root = doc.Root.Element(NodoRoot);
            if (root == null)
            {
                root = new XElement(NodoRoot);
                doc.Root.Add(root);
            }

            int nuevoId = 1;
            if (root.Elements(NodoElemento).Any())
                nuevoId = root.Elements(NodoElemento).Max(x => (int)x.Attribute("Id")) + 1;

            oferta.ID = nuevoId;
            var elem = new XElement(NodoElemento,
                new XAttribute("Id", oferta.ID),
                new XAttribute("Active", "true"),
                new XElement("OferenteId", oferta.Oferente.ID),
                new XElement("VehiculoId", oferta.Vehiculo.ID),
                new XElement("FechaInspeccion", oferta.FechaInspeccion),
                new XElement("Estado", oferta.Estado)
            );

            root.Add(elem);
            doc.Save(_rutaXml);
        }

        public void Modificar(OfertaCompra oferta)
        {
            if (!File.Exists(_rutaXml)) throw new FileNotFoundException(_rutaXml);
            var doc = XDocument.Load(_rutaXml);
            var root = doc.Root.Element(NodoRoot);
            var elem = root?
                .Elements(NodoElemento)
                .FirstOrDefault(x => (int)x.Attribute("Id") == oferta.ID);

            if (elem == null) throw new ApplicationException("Oferta no encontrada");

            elem.SetElementValue("OferenteId", oferta.Oferente.ID);
            elem.SetElementValue("VehiculoId", oferta.Vehiculo.ID);
            elem.SetElementValue("FechaInspeccion", oferta.FechaInspeccion);
            elem.SetElementValue("Estado", oferta.Estado);

            doc.Save(_rutaXml);
        }

        public void Baja(int id)
        {
            if (!File.Exists(_rutaXml)) return;
            var doc = XDocument.Load(_rutaXml);
            var elem = doc.Root.Element(NodoRoot)?
                .Elements(NodoElemento)
                .FirstOrDefault(x => (int)x.Attribute("Id") == id);

            if (elem != null)
            {
                elem.SetAttributeValue("Active", "false");
                doc.Save(_rutaXml);
            }
        }

        private OfertaCompra ParseOferta(XElement x)
        {
            return new OfertaCompra
            {
                ID = (int)x.Attribute("Id"),
                Oferente = new Oferente { ID = (int)x.Element("OferenteId") },
                Vehiculo = new Vehiculo { ID = (int)x.Element("VehiculoId") },
                FechaInspeccion = DateTime.Parse(x.Element("FechaInspeccion").Value),
                Estado = x.Element("Estado").Value
            };
        }
    }
}
