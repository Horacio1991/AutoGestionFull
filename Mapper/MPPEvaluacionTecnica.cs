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

        public MPPEvaluacionTecnica()
        {
            EnsureStructure();
        }

        private void EnsureStructure()
        {
            var dir = Path.GetDirectoryName(rutaXML);
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);

            if (!File.Exists(rutaXML))
            {
                new XDocument(
                    new XElement("BaseDeDatosLocal",
                        new XElement("Evaluaciones")
                    )
                ).Save(rutaXML);
            }
            else
            {
                var doc = XDocument.Load(rutaXML);
                if (doc.Root.Element("Evaluaciones") == null)
                {
                    doc.Root.Add(new XElement("Evaluaciones"));
                    doc.Save(rutaXML);
                }
            }
        }

        public List<EvaluacionTecnica> ListarTodo()
        {
            var doc = XDocument.Load(rutaXML);
            var root = doc.Root.Element("Evaluaciones");
            if (root == null) return new List<EvaluacionTecnica>();

            return root.Elements("Evaluacion")
                       .Where(x => (string)x.Attribute("Active") == "true")
                       .Select(ParseEvaluacion)
                       .ToList();
        }

        public EvaluacionTecnica BuscarPorId(int id)
        {
            return ListarTodo().FirstOrDefault(e => e.ID == id);
        }

        /// <summary>
        /// Busca la evaluación técnica asociada a una oferta.
        /// </summary>
        public EvaluacionTecnica BuscarPorOferta(int ofertaId)
        {
            var doc = XDocument.Load(rutaXML);
            var root = doc.Root.Element("Evaluaciones");
            if (root == null) return null;

            var x = root.Elements("Evaluacion")
                        .FirstOrDefault(e =>
                            (int)e.Element("OfertaId") == ofertaId
                            && (string)e.Attribute("Active") == "true"
                        );
            return x == null ? null : ParseEvaluacion(x);
        }

        public void AltaEvaluacion(EvaluacionTecnica eval, int ofertaId)
        {
            var doc = XDocument.Load(rutaXML);
            var root = doc.Root.Element("Evaluaciones")
                       ?? throw new ApplicationException("Sección Evaluaciones no encontrada.");

            int nextId = root.Elements("Evaluacion")
                             .Select(x => (int)x.Attribute("Id"))
                             .DefaultIfEmpty(0)
                             .Max() + 1;

            eval.ID = nextId;

            var elem = new XElement("Evaluacion",
                new XAttribute("Id", nextId),
                new XAttribute("Active", "true"),
                new XElement("OfertaId", ofertaId),
                new XElement("EstadoMotor", eval.EstadoMotor),
                new XElement("EstadoCarroceria", eval.EstadoCarroceria),
                new XElement("EstadoInterior", eval.EstadoInterior),
                new XElement("EstadoDocumentacion", eval.EstadoDocumentacion),
                new XElement("Observaciones", eval.Observaciones ?? string.Empty)
            );
            root.Add(elem);
            doc.Save(rutaXML);
        }

        public void Modificar(EvaluacionTecnica eval)
        {
            var doc = XDocument.Load(rutaXML);
            var root = doc.Root.Element("Evaluaciones");
            var x = root?.Elements("Evaluacion")
                         .FirstOrDefault(e => (int)e.Attribute("Id") == eval.ID);
            if (x == null) throw new ApplicationException("Evaluación no encontrada.");

            x.SetElementValue("EstadoMotor", eval.EstadoMotor);
            x.SetElementValue("EstadoCarroceria", eval.EstadoCarroceria);
            x.SetElementValue("EstadoInterior", eval.EstadoInterior);
            x.SetElementValue("EstadoDocumentacion", eval.EstadoDocumentacion);
            x.SetElementValue("Observaciones", eval.Observaciones ?? string.Empty);
            doc.Save(rutaXML);
        }

        public void Baja(int id)
        {
            var doc = XDocument.Load(rutaXML);
            var root = doc.Root.Element("Evaluaciones");
            var x = root?.Elements("Evaluacion")
                         .FirstOrDefault(e => (int)e.Attribute("Id") == id);
            if (x == null) throw new ApplicationException("Evaluación no encontrada.");

            x.SetAttributeValue("Active", "false");
            doc.Save(rutaXML);
        }

        private EvaluacionTecnica ParseEvaluacion(XElement x) => new EvaluacionTecnica
        {
            ID = (int)x.Attribute("Id"),
            Oferta = new OfertaCompra { ID = (int)x.Element("OfertaId") },
            EstadoMotor = (string)x.Element("EstadoMotor"),
            EstadoCarroceria = (string)x.Element("EstadoCarroceria"),
            EstadoInterior = (string)x.Element("EstadoInterior"),
            EstadoDocumentacion = (string)x.Element("EstadoDocumentacion"),
            Observaciones = (string)x.Element("Observaciones")
        };
    }
}
