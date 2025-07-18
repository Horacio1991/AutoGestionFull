using BE;
using Servicios.Utilidades;
using System.Xml.Linq;

namespace Mapper
{
    public class MPPTurno
    {
        private readonly string rutaXML = XmlPaths.BaseDatosLocal;

        // Devuelve todos los turnos con Asistencia = "Pendiente" y Active = true.
        public List<Turno> ListarPendientesAsistencia()
        {
            try
            {
                if (!File.Exists(rutaXML))
                    return new List<Turno>();

                var doc = XDocument.Load(rutaXML);
                var root = doc.Root.Element("Turnos");
                if (root == null)
                    return new List<Turno>();

                return root
                    .Elements("Turno")
                    .Where(x => (string)x.Attribute("Active") == "true"
                             && (string)x.Element("Asistencia") == "Pendiente")
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
            catch (Exception)
            {
                return new List<Turno>();
            }
        }

        // Registra la asistencia o no de un turno
        public void RegistrarAsistencia(int turnoId, string estado, string observaciones)
        {
            try
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
            catch (Exception ex)
            {
                throw new ApplicationException("No se pudo registrar la asistencia del turno. " + ex.Message, ex);
            }
        }

        // Agrega un nuevo turno al XML.
        public void AgregarTurno(Turno turno)
        {
            try
            {
                XDocument doc;
                if (File.Exists(rutaXML))
                    doc = XDocument.Load(rutaXML);
                else
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(rutaXML));
                    doc = new XDocument(new XElement("BaseDeDatosLocal", new XElement("Turnos")));
                }

                var root = doc.Root.Element("Turnos");
                if (root == null)
                {
                    root = new XElement("Turnos");
                    doc.Root.Add(root);
                }

                // calcular nuevo ID
                int nuevoId = root.Elements("Turno")
                                  .Select(x => (int)x.Attribute("Id"))
                                  .DefaultIfEmpty(0)
                                  .Max() + 1;
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
            catch (Exception ex)
            {
                throw new ApplicationException("No se pudo agregar el turno. " + ex.Message, ex);
            }
        }
    }
}
