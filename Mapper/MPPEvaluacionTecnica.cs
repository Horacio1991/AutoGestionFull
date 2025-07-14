// Mapper/MPPEvaluacionTecnica.cs
using BE;
using Servicios.Utilidades;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Mapper
{
    public class MPPEvaluacionTecnica
    {
        private readonly string rutaXML = XmlPaths.BaseDatosLocal;

        public List<EvaluacionTecnica> ListarTodo()
        {
            if (!File.Exists(rutaXML))
                return new List<EvaluacionTecnica>();

            var doc = XDocument.Load(rutaXML);
            var elems = doc.Root.Element("EvaluacionesTecnicas")?
                        .Elements("EvaluacionTecnica")
                        .Where(x => x.Attribute("Active")?.Value == "true")
                      ?? Enumerable.Empty<XElement>();

            return elems.Select(x => new EvaluacionTecnica
            {
                ID = (int)x.Attribute("Id"),
                EstadoMotor = x.Element("EstadoMotor")?.Value,
                EstadoCarroceria = x.Element("EstadoCarroceria")?.Value,
                EstadoInterior = x.Element("EstadoInterior")?.Value,
                EstadoDocumentacion = x.Element("EstadoDocumentacion")?.Value,
                Observaciones = x.Element("Observaciones")?.Value
            }).ToList();
        }

        public EvaluacionTecnica BuscarPorId(int id) =>
            ListarTodo().FirstOrDefault(e => e.ID == id);

        public void GuardarLista(List<EvaluacionTecnica> lista)
        {
            var doc = File.Exists(rutaXML) ? XDocument.Load(rutaXML)
                                           : new XDocument(new XElement("BaseDeDatosLocal", new XElement("EvaluacionesTecnicas")));
            var root = doc.Root.Element("EvaluacionesTecnicas") ?? new XElement("EvaluacionesTecnicas");
            if (doc.Root.Element("EvaluacionesTecnicas") == null)
                doc.Root.Add(root);

            root.RemoveAll();
            foreach (var e in lista)
            {
                var elem = new XElement("EvaluacionTecnica",
                    new XAttribute("Id", e.ID),
                    new XAttribute("Active", "true"),
                    new XElement("EstadoMotor", e.EstadoMotor),
                    new XElement("EstadoCarroceria", e.EstadoCarroceria),
                    new XElement("EstadoInterior", e.EstadoInterior),
                    new XElement("EstadoDocumentacion", e.EstadoDocumentacion),
                    new XElement("Observaciones", e.Observaciones)
                );
                root.Add(elem);
            }
            doc.Save(rutaXML);
        }

        public void AltaEvaluacion(EvaluacionTecnica eval)
        {
            var lista = ListarTodo();
            lista.RemoveAll(e => e.ID == eval.ID);
            lista.Add(eval);
            GuardarLista(lista);
        }
    }
}
