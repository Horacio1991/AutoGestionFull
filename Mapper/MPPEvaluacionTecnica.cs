using BE;
using Servicios.Utilidades;
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

        // Asegura que la estructura del XML esté creada
        private void EnsureStructure()
        {
            try
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
            catch (Exception)
            {
            }
        }

        // trae todas las evaluaciones activas
        public List<EvaluacionTecnica> ListarTodo()
        {
            try
            {
                var doc = XDocument.Load(rutaXML);
                var root = doc.Root.Element("Evaluaciones");
                if (root == null) return new List<EvaluacionTecnica>();

                return root.Elements("Evaluacion")
                           .Where(x => (string)x.Attribute("Active") == "true")
                           .Select(ParseEvaluacion)
                           .ToList();
            }
            catch (Exception)
            {
                return new List<EvaluacionTecnica>();
            }
        }

        // se le pasa la ofertaId y devuelve la evaluación técnica asociada
        public EvaluacionTecnica BuscarPorOferta(int ofertaId)
        {
            try
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
            catch (Exception)
            {
                return null;
            }
        }

        public void AltaEvaluacion(EvaluacionTecnica eval, int ofertaId)
        {
            try
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
            catch (Exception ex)
            {
                throw new ApplicationException("No se pudo dar de alta la evaluación técnica. " + ex.Message, ex);
            }
        }

        // parsea un XElement a EvaluacionTecnica
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
