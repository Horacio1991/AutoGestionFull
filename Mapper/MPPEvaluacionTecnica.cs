using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using BE;
using Servicios.Utilidades;

namespace Mapper
{
    public class MPPEvaluacionTecnica
    {
        private readonly string rutaXML = XmlPaths.BaseDatosLocal;

        public MPPEvaluacionTecnica()
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
                new XDocument(new XElement("BaseDeDatosLocal",
                    new XElement("Evaluaciones"))).Save(rutaXML);
            }
        }

        public List<EvaluacionTecnica> ListarTodo()
        {
            var doc = XDocument.Load(rutaXML);
            var root = doc.Root.Element("Evaluaciones");
            if (root == null) return new List<EvaluacionTecnica>();

            return root.Elements("Evaluacion")
                       .Where(x => (string)x.Attribute("Active") == "true")
                       .Select(x => new EvaluacionTecnica
                       {
                           ID = (int)x.Attribute("Id"),
                           EstadoMotor = (string)x.Element("EstadoMotor"),
                           EstadoCarroceria = (string)x.Element("EstadoCarroceria"),
                           EstadoInterior = (string)x.Element("EstadoInterior"),
                           EstadoDocumentacion = (string)x.Element("EstadoDocumentacion"),
                           Observaciones = (string)x.Element("Observaciones")
                       })
                       .ToList();
        }

        public EvaluacionTecnica BuscarPorId(int id)
        {
            return ListarTodo().FirstOrDefault(e => e.ID == id);
        }

        public void Alta(EvaluacionTecnica eval)
        {
            var doc = XDocument.Load(rutaXML);
            var root = doc.Root.Element("Evaluaciones");

            int nextId = root.Elements("Evaluacion")
                             .Select(x => (int)x.Attribute("Id"))
                             .DefaultIfEmpty(0)
                             .Max() + 1;

            eval.ID = nextId;

            var elem = new XElement("Evaluacion",
                new XAttribute("Id", nextId),
                new XAttribute("Active", "true"),
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
            var elem = doc.Root.Element("Evaluaciones")
                           .Elements("Evaluacion")
                           .FirstOrDefault(x => (int)x.Attribute("Id") == eval.ID);
            if (elem == null) throw new ApplicationException("Evaluación no encontrada.");

            elem.Element("EstadoMotor").SetValue(eval.EstadoMotor);
            elem.Element("EstadoCarroceria").SetValue(eval.EstadoCarroceria);
            elem.Element("EstadoInterior").SetValue(eval.EstadoInterior);
            elem.Element("EstadoDocumentacion").SetValue(eval.EstadoDocumentacion);
            elem.Element("Observaciones").SetValue(eval.Observaciones ?? string.Empty);

            doc.Save(rutaXML);
        }

        public void Baja(int id)
        {
            var doc = XDocument.Load(rutaXML);
            var elem = doc.Root.Element("Evaluaciones")
                           .Elements("Evaluacion")
                           .FirstOrDefault(x => (int)x.Attribute("Id") == id);
            if (elem == null) throw new ApplicationException("Evaluación no encontrada.");

            elem.SetAttributeValue("Active", "false");
            doc.Save(rutaXML);
        }
    }
}
