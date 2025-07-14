using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using BE;
using Servicios.Utilidades;

namespace Mapper
{
    public class MPPTurno
    {
        private readonly string rutaXML = XmlPaths.BaseDatosLocal;

        /// <summary>
        /// Lista todos los turnos activos.
        /// </summary>
        public List<Turno> ListarTodo()
        {
            if (!File.Exists(rutaXML))
                return new List<Turno>();

            var doc = XDocument.Load(rutaXML);
            var root = doc.Root.Element("Turnos");
            if (root == null)
                return new List<Turno>();

            return root
                .Elements("Turno")
                .Where(x => (string)x.Attribute("Active") == "true")
                .Select(x => new Turno
                {
                    ID = (int)x.Attribute("Id"),
                    Cliente = new Cliente { ID = (int)x.Element("ClienteId") },
                    Vehiculo = new Vehiculo { ID = (int)x.Element("VehiculoId") },
                    Fecha = DateTime.Parse(x.Element("Fecha").Value),
                    Hora = TimeSpan.Parse(x.Element("Hora").Value),
                    Asistencia = x.Element("Asistencia")?.Value,
                    Observaciones = x.Element("Observaciones")?.Value
                })
                .ToList();
        }

        /// <summary>
        /// Agrega un nuevo turno (equivalente a AltaTurno).
        /// </summary>
        public void AltaTurno(Turno turno)
        {
            XDocument doc;
            if (File.Exists(rutaXML))
            {
                doc = XDocument.Load(rutaXML);
            }
            else
            {
                Directory.CreateDirectory(Path.GetDirectoryName(rutaXML));
                doc = new XDocument(new XElement("BaseDeDatosLocal", new XElement("Turnos")));
            }

            var root = doc.Root.Element("Turnos");
            int nuevoId = root.Elements("Turno").Any()
                ? root.Elements("Turno").Max(x => (int)x.Attribute("Id")) + 1
                : 1;

            turno.ID = nuevoId;

            var nuevo = new XElement("Turno",
                new XAttribute("Id", turno.ID),
                new XAttribute("Active", "true"),
                new XElement("ClienteId", turno.Cliente.ID),
                new XElement("VehiculoId", turno.Vehiculo.ID),
                new XElement("Fecha", turno.Fecha.ToString("s")),
                new XElement("Hora", turno.Hora.ToString()),
                new XElement("Asistencia", turno.Asistencia ?? "Pendiente"),
                new XElement("Observaciones", turno.Observaciones ?? "")
            );

            root.Add(nuevo);
            doc.Save(rutaXML);
        }

        /// <summary>
        /// Modifica un turno existente.
        /// </summary>
        public void ModificarTurno(Turno turno)
        {
            if (!File.Exists(rutaXML))
                throw new FileNotFoundException("Base de datos no encontrada.");

            var doc = XDocument.Load(rutaXML);
            var elem = doc.Root.Element("Turnos")
                          ?.Elements("Turno")
                          .FirstOrDefault(x => (int)x.Attribute("Id") == turno.ID);

            if (elem == null)
                throw new ApplicationException("Turno no encontrado.");

            // Actualiza los campos que pueden cambiar
            elem.Element("ClienteId").Value = turno.Cliente.ID.ToString();
            elem.Element("VehiculoId").Value = turno.Vehiculo.ID.ToString();
            elem.Element("Fecha").Value = turno.Fecha.ToString("s");
            elem.Element("Hora").Value = turno.Hora.ToString();
            elem.SetElementValue("Asistencia", turno.Asistencia);
            elem.SetElementValue("Observaciones", turno.Observaciones);

            doc.Save(rutaXML);
        }

        /// <summary>
        /// Da de baja (pone Active="false") un turno.
        /// </summary>
        public void BajaTurno(int turnoId)
        {
            if (!File.Exists(rutaXML))
                throw new FileNotFoundException("Base de datos no encontrada.");

            var doc = XDocument.Load(rutaXML);
            var elem = doc.Root.Element("Turnos")
                          ?.Elements("Turno")
                          .FirstOrDefault(x => (int)x.Attribute("Id") == turnoId);

            if (elem == null)
                throw new ApplicationException("Turno no encontrado.");

            elem.SetAttributeValue("Active", "false");
            doc.Save(rutaXML);
        }

        /// <summary>
        /// Registra únicamente la asistencia de un turno.
        /// </summary>
        public void RegistrarAsistencia(int turnoId, string estado, string observaciones)
        {
            if (!File.Exists(rutaXML))
                throw new FileNotFoundException("Base de datos no encontrada.");

            var doc = XDocument.Load(rutaXML);
            var elem = doc.Root.Element("Turnos")
                          ?.Elements("Turno")
                          .FirstOrDefault(x => (int)x.Attribute("Id") == turnoId);

            if (elem == null)
                throw new ApplicationException("Turno no encontrado.");

            elem.SetElementValue("Asistencia", estado);
            elem.SetElementValue("Observaciones", observaciones ?? "");
            doc.Save(rutaXML);
        }
    }
}
