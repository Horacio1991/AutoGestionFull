// Mapper/MPPVehiculo.cs
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
            if (!File.Exists(rutaXML))
                return new XDocument(new XElement("BaseDeDatosLocal", new XElement("Vehiculos")));

            var doc = XDocument.Load(rutaXML);
            if (doc.Root.Element("Vehiculos") == null)
                doc.Root.Add(new XElement("Vehiculos"));
            return doc;
        }

        private IEnumerable<XElement> RootVehiculos(XDocument doc)
        {
            var root = doc.Root.Element("Vehiculos");
            if (root == null) yield break;
            foreach (var x in root.Elements("Vehiculo"))
                yield return x;
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
            => ListarTodo()
               .Where(v => string.Equals(v.Estado, "Disponible", StringComparison.OrdinalIgnoreCase))
               .ToList();

        public Vehiculo BuscarPorId(int id)
            => ListarTodo().FirstOrDefault(v => v.ID == id);

        public List<Vehiculo> BuscarPorModelo(string modelo)
            => ListarDisponibles()
               .Where(v => v.Modelo.Equals(modelo, StringComparison.OrdinalIgnoreCase))
               .ToList();

        public List<Vehiculo> BuscarPorMarca(string marca)
            => ListarDisponibles()
               .Where(v => v.Marca.Equals(marca, StringComparison.OrdinalIgnoreCase))
               .ToList();

        public Vehiculo BuscarPorDominio(string dominio)
            => ListarTodo()
               .FirstOrDefault(v =>
                   v.Dominio.Equals(dominio, StringComparison.OrdinalIgnoreCase));

        public void Alta(Vehiculo v)
        {
            var doc = LoadOrEmpty();
            var root = doc.Root.Element("Vehiculos");

            // Calcular nuevo ID
            int nextId = root.Elements("Vehiculo")
                             .Select(x => (int)x.Attribute("Id"))
                             .DefaultIfEmpty(0)
                             .Max() + 1;
            v.ID = nextId;

            // Crear elemento
            var elem = new XElement("Vehiculo",
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

            doc.Save(rutaXML);
        }

        public void Actualizar(Vehiculo vehiculo)
        {
            var doc = XDocument.Load(rutaXML);
            var root = doc.Root.Element("Vehiculos")
                       ?? throw new ApplicationException("Sección 'Vehiculos' no encontrada en el XML.");

            var elem = root
                .Elements("Vehiculo")
                .FirstOrDefault(x => (int)x.Attribute("Id") == vehiculo.ID);

            if (elem == null)
                throw new ApplicationException($"Vehículo con Id={vehiculo.ID} no encontrado.");

            elem.SetElementValue("Marca", vehiculo.Marca);
            elem.SetElementValue("Modelo", vehiculo.Modelo);
            elem.SetElementValue("Año", vehiculo.Año);
            elem.SetElementValue("Color", vehiculo.Color);
            elem.SetElementValue("Km", vehiculo.Km);
            elem.SetElementValue("Dominio", vehiculo.Dominio);
            elem.SetElementValue("Estado", vehiculo.Estado);

            doc.Save(rutaXML);
        }

        /// <summary>
        /// Cambia el estado de stock de un vehículo y lo persiste en el XML.
        /// </summary>
        public void ActualizarEstadoStock(int vehiculoId, string nuevoEstado)
        {
            // 1) Cargo el documento
            var doc = XDocument.Load(rutaXML);
            var root = doc.Root.Element("Vehiculos");
            if (root == null)
                throw new ApplicationException("Sección 'Vehiculos' no encontrada en el XML.");

            // 2) Busco el elemento
            var elem = root
                .Elements("Vehiculo")
                .FirstOrDefault(x => (int)x.Attribute("Id") == vehiculoId);
            if (elem == null)
                throw new ApplicationException($"Vehículo con Id={vehiculoId} no encontrado.");

            // 3) Actualizo el estado
            elem.SetElementValue("Estado", nuevoEstado);

            // 4) Guardo
            doc.Save(rutaXML);
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
