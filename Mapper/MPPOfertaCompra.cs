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

        public List<OfertaCompra> ListarTodo()
        {
            var doc = XDocument.Load(rutaXML);
            var root = doc.Root.Element("OfertaCompras");
            if (root == null) return new List<OfertaCompra>();

            return root.Elements("OfertaCompra")
                       .Where(x => (string)x.Attribute("Active") == "true")
                       .Select(x => new OfertaCompra
                       {
                           ID = (int)x.Attribute("Id"),
                           FechaInspeccion = DateTime.Parse(x.Element("FechaInspeccion")?.Value ?? DateTime.Now.ToString("s")),
                           Estado = (string)x.Element("Estado"),
                           Oferente = new Oferente { ID = (int)x.Element("Oferente").Attribute("Id") },
                           Vehiculo = new Vehiculo { ID = (int)x.Element("Vehiculo").Attribute("Id") }
                       })
                       .ToList();
        }

        public OfertaCompra BuscarPorId(int id)
        {
            return ListarTodo().FirstOrDefault(o => o.ID == id);
        }

        public List<OfertaCompra> BuscarPorDominio(string dominio)
        {
            return ListarTodo()
                   .Where(o => o.Vehiculo != null &&
                               !string.IsNullOrEmpty(o.Vehiculo.Dominio) &&
                               o.Vehiculo.Dominio.Equals(dominio, StringComparison.OrdinalIgnoreCase))
                   .ToList();
        }

        public void Alta(OfertaCompra oferta)
        {
            var doc = XDocument.Load(rutaXML);
            var root = doc.Root.Element("OfertaCompras");

            int nextId = root.Elements("OfertaCompra")
                             .Select(x => (int)x.Attribute("Id"))
                             .DefaultIfEmpty(0)
                             .Max() + 1;

            oferta.ID = nextId;

            var elem = new XElement("OfertaCompra",
                new XAttribute("Id", nextId),
                new XAttribute("Active", "true"),
                new XElement("FechaInspeccion", oferta.FechaInspeccion.ToString("s")),
                new XElement("Estado", oferta.Estado),
                new XElement("Oferente", new XAttribute("Id", oferta.Oferente.ID)),
                new XElement("Vehiculo", new XAttribute("Id", oferta.Vehiculo.ID))
            );

            root.Add(elem);
            doc.Save(rutaXML);
        }

        public void MarcarTasada(int ofertaId)
        {
            var doc = XDocument.Load(rutaXML);
            var root = doc.Root.Element("OfertaCompras");
            var elem = root?.Elements("OfertaCompra")
                            .FirstOrDefault(x => (int)x.Attribute("Id") == ofertaId);
            if (elem == null)
                throw new ApplicationException("Oferta no encontrada.");
            elem.SetAttributeValue("Estado", "Tasada");
            doc.Save(rutaXML);
        }

    }
}
