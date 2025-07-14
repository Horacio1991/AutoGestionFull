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
    public class MPPVehiculo
    {
        private readonly string _rutaXml = XmlPaths.BaseDatosLocal;
        private const string NodoRoot = "Vehiculos";
        private const string NodoElemento = "Vehiculo";

        public List<Vehiculo> ListarTodo()
        {
            if (!File.Exists(_rutaXml))
                return new List<Vehiculo>();

            var doc = XDocument.Load(_rutaXml);
            var elems = doc.Root.Element(NodoRoot)?.Elements(NodoElemento) ?? Enumerable.Empty<XElement>();

            return elems
                .Where(x => (string)x.Attribute("Active") == "true")
                .Select(Parse)
                .ToList();
        }

        public Vehiculo BuscarPorId(int id)
        {
            if (!File.Exists(_rutaXml)) return null;
            var doc = XDocument.Load(_rutaXml);
            var x = doc.Root.Element(NodoRoot)?
                .Elements(NodoElemento)
                .FirstOrDefault(e => (int)e.Attribute("Id") == id && (string)e.Attribute("Active") == "true");
            return x == null ? null : Parse(x);
        }

        public Vehiculo BuscarPorDominio(string dominio)
        {
            if (!File.Exists(_rutaXml)) return null;
            var doc = XDocument.Load(_rutaXml);
            var x = doc.Root.Element(NodoRoot)?
                .Elements(NodoElemento)
                .FirstOrDefault(e =>
                    (string)e.Attribute("Active") == "true" &&
                    string.Equals((string)e.Element("Dominio"), dominio, StringComparison.OrdinalIgnoreCase));
            return x == null ? null : Parse(x);
        }

        public void Alta(Vehiculo v)
        {
            XDocument doc;
            if (File.Exists(_rutaXml))
                doc = XDocument.Load(_rutaXml);
            else
                doc = new XDocument(new XElement("BaseDeDatosLocal", new XElement(NodoRoot)));

            var root = doc.Root.Element(NodoRoot) ?? new XElement(NodoRoot);
            if (doc.Root.Element(NodoRoot) == null)
                doc.Root.Add(root);

            int nuevoId = root.Elements(NodoElemento).Any()
                ? root.Elements(NodoElemento).Max(e => (int)e.Attribute("Id")) + 1
                : 1;

            v.ID = nuevoId;
            var elem = new XElement(NodoElemento,
                new XAttribute("Id", v.ID),
                new XAttribute("Active", "true"),
                new XElement("Marca", v.Marca),
                new XElement("Modelo", v.Modelo),
                new XElement("Año", v.Año),
                new XElement("Color", v.Color),
                new XElement("Km", v.Km),
                new XElement("Dominio", v.Dominio),
                new XElement("Estado", v.Estado)
            );

            root.Add(elem);
            doc.Save(_rutaXml);
        }

        public void Modificar(Vehiculo v)
        {
            if (!File.Exists(_rutaXml)) throw new FileNotFoundException(_rutaXml);
            var doc = XDocument.Load(_rutaXml);
            var elem = doc.Root.Element(NodoRoot)?
                .Elements(NodoElemento)
                .FirstOrDefault(e => (int)e.Attribute("Id") == v.ID);

            if (elem == null) throw new ApplicationException("Vehículo no encontrado");

            elem.SetElementValue("Marca", v.Marca);
            elem.SetElementValue("Modelo", v.Modelo);
            elem.SetElementValue("Año", v.Año);
            elem.SetElementValue("Color", v.Color);
            elem.SetElementValue("Km", v.Km);
            elem.SetElementValue("Dominio", v.Dominio);
            elem.SetElementValue("Estado", v.Estado);

            doc.Save(_rutaXml);
        }

        public void Baja(int id)
        {
            if (!File.Exists(_rutaXml)) return;
            var doc = XDocument.Load(_rutaXml);
            var elem = doc.Root.Element(NodoRoot)?
                .Elements(NodoElemento)
                .FirstOrDefault(e => (int)e.Attribute("Id") == id);

            if (elem != null)
            {
                elem.SetAttributeValue("Active", "false");
                doc.Save(_rutaXml);
            }
        }

        private Vehiculo Parse(XElement x) => new Vehiculo
        {
            ID = (int)x.Attribute("Id"),
            Marca = (string)x.Element("Marca"),
            Modelo = (string)x.Element("Modelo"),
            Año = (int)x.Element("Año"),
            Color = (string)x.Element("Color"),
            Km = (int)x.Element("Km"),
            Dominio = (string)x.Element("Dominio"),
            Estado = (string)x.Element("Estado")
        };
    }
}
