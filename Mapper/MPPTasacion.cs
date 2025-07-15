using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using BE;
using Servicios.Utilidades;

namespace Mapper
{
    public class MPPTasacion
    {
        private readonly string rutaXML = XmlPaths.BaseDatosLocal;

        public MPPTasacion()
        {
            AsegurarNodo();
        }

        private void AsegurarNodo()
        {
            var dir = Path.GetDirectoryName(rutaXML);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            if (!File.Exists(rutaXML))
            {
                new XDocument(
                    new XElement("BaseDeDatosLocal",
                        new XElement("Tasaciones")
                    )
                ).Save(rutaXML);
            }
            else
            {
                var doc = XDocument.Load(rutaXML);
                if (doc.Root.Element("Tasaciones") == null)
                {
                    doc.Root.Add(new XElement("Tasaciones"));
                    doc.Save(rutaXML);
                }
            }
        }

        public List<Tasacion> ListarTodo()
        {
            var doc = XDocument.Load(rutaXML);
            var root = doc.Root.Element("Tasaciones");
            if (root == null) return new List<Tasacion>();

            return root.Elements("Tasacion")
                       .Where(x => (string)x.Attribute("Active") == "true")
                       .Select(x => new Tasacion
                       {
                           ID = (int)x.Attribute("Id"),
                           ValorFinal = decimal.Parse(x.Element("ValorFinal")?.Value ?? "0"),
                           Fecha = DateTime.Parse(x.Element("Fecha")?.Value ?? DateTime.Now.ToString("s")),
                           Oferta = new OfertaCompra
                           { ID = (int)x.Element("OfertaId") }
                       })
                       .ToList();
        }

        public Tasacion BuscarPorId(int id)
        {
            return ListarTodo().FirstOrDefault(t => t.ID == id);
        }

        public void Alta(Tasacion tasacion)
        {
            var doc = XDocument.Load(rutaXML);
            var root = doc.Root.Element("Tasaciones");

            int nextId = root.Elements("Tasacion")
                             .Select(x => (int)x.Attribute("Id"))
                             .DefaultIfEmpty(0)
                             .Max() + 1;

            tasacion.ID = nextId;
            tasacion.Fecha = DateTime.Now;

            var elem = new XElement("Tasacion",
                new XAttribute("Id", nextId),
                new XAttribute("Active", "true"),
                new XElement("OfertaId", tasacion.Oferta.ID),
                new XElement("ValorFinal", tasacion.ValorFinal),
                new XElement("Fecha", tasacion.Fecha.ToString("s"))
            );

            root.Add(elem);
            doc.Save(rutaXML);
        }
    }
}
