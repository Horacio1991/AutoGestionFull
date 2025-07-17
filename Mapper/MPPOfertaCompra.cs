using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using BE;
using Servicios.Utilidades;

namespace Mapper
{
    public class MPPOfertaCompra
    {
        private readonly string rutaXML = XmlPaths.BaseDatosLocal;

        public MPPOfertaCompra()
        {
            AsegurarArchivo();
        }

        private void AsegurarArchivo()
        {
            var dir = Path.GetDirectoryName(rutaXML);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            if (!File.Exists(rutaXML))
            {
                new XDocument(
                    new XElement("BaseDeDatosLocal",
                        new XElement("OfertaCompras")
                    )
                ).Save(rutaXML);
            }
        }

        private XElement Root(XDocument doc)
        {
            var root = doc.Root.Element("OfertaCompras");
            if (root == null)
            {
                root = new XElement("OfertaCompras");
                doc.Root.Add(root);
            }
            return root;
        }

        public List<OfertaCompra> ListarTodo()
        {
            var doc = XDocument.Load(rutaXML);
            var root = doc.Root.Element("OfertaCompras");
            if (root == null) return new List<OfertaCompra>();

            return root
                .Elements("OfertaCompra")
                .Where(x => (string)x.Attribute("Active") == "true")
                .Select(x => new OfertaCompra
                {
                    ID = (int)x.Attribute("Id"),
                    FechaInspeccion = DateTime.Parse(x.Element("FechaInspeccion")?.Value ?? DateTime.Now.ToString("s")),
                    Estado = (string)x.Element("Estado"),
                    // sólo asignamos el ID de la oferta; el Vehiculo completo se llena en BLL
                    Vehiculo = new Vehiculo { ID = (int)x.Element("VehiculoId") }
                })
                .ToList();
        }

        public OfertaCompra BuscarPorId(int id)
        {
            return ListarTodo().FirstOrDefault(o => o.ID == id);
        }

        public List<OfertaCompra> BuscarPorDominio(string dominio)
        {
            // Busca ofertas cuyo vehículo tenga ese dominio
            return ListarTodo()
                .Where(o => o.Vehiculo != null && string.Equals(
                    // vamos a cargar el vehículo para comparar
                    new MPPVehiculo().BuscarPorId(o.Vehiculo.ID)?.Dominio,
                    dominio, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public void Alta(OfertaCompra oferta)
        {
            var doc = XDocument.Load(rutaXML);
            var root = Root(doc);

            int nextId = root.Elements("OfertaCompra")
                             .Select(x => (int)x.Attribute("Id"))
                             .DefaultIfEmpty(0)
                             .Max() + 1;
            oferta.ID = nextId;

            var elem = new XElement("OfertaCompra",
                new XAttribute("Id", oferta.ID),
                new XAttribute("Active", "true"),
                new XElement("OferenteId", oferta.Oferente.ID),
                new XElement("VehiculoId", oferta.Vehiculo.ID),
                new XElement("FechaInspeccion", oferta.FechaInspeccion.ToString("s")),
                new XElement("Estado", oferta.Estado)
            );

            root.Add(elem);
            doc.Save(rutaXML);
        }

        public void MarcarTasada(int ofertaId)
        {
            var doc = XDocument.Load(rutaXML);
            var root = doc.Root.Element("OfertaCompras");
            var elem = root?
                .Elements("OfertaCompra")
                .FirstOrDefault(x => (int)x.Attribute("Id") == ofertaId);
            if (elem == null)
                throw new ApplicationException("Oferta no encontrada.");
            elem.SetElementValue("Estado", "Tasada");
            doc.Save(rutaXML);
        }
    }
}
