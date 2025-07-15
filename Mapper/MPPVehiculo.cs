using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using BE;
using Servicios.Utilidades;

namespace Mapper
{
    public class MPPVehiculo
    {
        private readonly string rutaXML = XmlPaths.BaseDatosLocal;

        private XDocument LoadOrEmpty()
        {
            return File.Exists(rutaXML)
                ? XDocument.Load(rutaXML)
                : new XDocument(new XElement("BaseDeDatosLocal"));
        }

        private IEnumerable<XElement> RootVehiculos(XDocument doc)
        {
            var root = doc.Root.Element("Vehiculos");
            if (root == null) yield break;
            foreach (var x in root.Elements("Vehiculo")) yield return x;
        }

        public List<Vehiculo> ListarTodo()
        {
            var doc = LoadOrEmpty();
            return RootVehiculos(doc)
                .Where(x => (string)x.Attribute("Active") == "true")
                .Select(ParseVehiculo)
                .ToList();
        }

        public List<Vehiculo> ListarDisponibles()
        {
            return ListarTodo()
                .Where(v => string.Equals(v.Estado, "Disponible", StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public Vehiculo BuscarPorId(int id)
        {
            return ListarTodo().FirstOrDefault(v => v.ID == id);
        }

        public List<Vehiculo> BuscarPorModelo(string modelo)
        {
            return ListarDisponibles()
                .Where(v => v.Modelo.Equals(modelo, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public List<Vehiculo> BuscarPorMarca(string marca)
        {
            return ListarDisponibles()
                .Where(v => v.Marca.Equals(marca, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        private Vehiculo ParseVehiculo(XElement x) => new Vehiculo
        {
            ID = (int)x.Attribute("Id"),
            Marca = x.Element("Marca")?.Value,
            Modelo = x.Element("Modelo")?.Value,
            Año = (int?)x.Element("Año") ?? 0,
            Color = x.Element("Color")?.Value,
            Km = (int?)x.Element("Km") ?? 0,
            Dominio = x.Element("Dominio")?.Value,
            Estado = x.Element("Estado")?.Value
        };
    }
}
