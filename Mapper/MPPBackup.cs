using BE;
using Servicios.Utilidades;
using System.Xml.Linq;

namespace Mapper
{
    public class MPPBackup
    {
        private readonly string xmlFolderBackups = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Backup");
        private readonly string xmlHistorialBackups = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Backup", "HistorialBackup.xml");
        private readonly string rutaSistema = XmlPaths.BaseDatosLocal;

        public MPPBackup()
        {
            AsegurarHistorialBackup();
        }

        public List<Backup> ListarTodo()
        {
            var lista = new List<Backup>();
            if (!Directory.Exists(xmlFolderBackups))
                Directory.CreateDirectory(xmlFolderBackups);
            foreach (var archivo in Directory.GetFiles(xmlFolderBackups, "*.xml"))
            {
                lista.Add(new Backup
                {
                    Nombre = Path.GetFileNameWithoutExtension(archivo),
                    Fecha = File.GetCreationTime(archivo)
                });
            }
            return lista;
        }

        public bool CrearBackup(Backup backup)
        {
            try
            {
                if (!Directory.Exists(xmlFolderBackups))
                    Directory.CreateDirectory(xmlFolderBackups);

                string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                backup.Nombre = $"Backup-{timestamp}";
                backup.Fecha = DateTime.Now;

                string destino = Path.Combine(xmlFolderBackups, $"{backup.Nombre}.xml");
                File.Copy(rutaSistema, destino, overwrite: true);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public void GuardarBackupEnHistorial(Backup backup)
        {
            var doc = XDocument.Load(xmlHistorialBackups);

            // Calcula el nuevo ID incremental.
            int nuevoId = doc.Root.Elements("Bitacora")
                          .Select(e => (int)e.Attribute("Id"))
                          .DefaultIfEmpty(0)
                          .Max() + 1;

            var elem = new XElement("Bitacora",
                new XAttribute("Id", nuevoId),
                new XAttribute("Fecha", backup.Fecha.ToString("s")),
                new XAttribute("Usuario", backup.UsernameUsuario ?? "(desconocido)"),
                new XElement("Mensaje", $"Backup generado: {backup.Nombre}")
            );
            doc.Root.Add(elem);
            doc.Save(xmlHistorialBackups);
        }

        // Si no existe el historial, lo crea vacío con raíz <Bitacoras>.
        public void AsegurarHistorialBackup()
        {
            if (!File.Exists(xmlHistorialBackups))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(xmlHistorialBackups));
                new XDocument(new XElement("Bitacoras"))
                    .Save(xmlHistorialBackups);
            }
        }

        // Devuelve la lista de backups registrados en el historial.
        public List<Backup> ListarHistorial()
        {
            var lista = new List<Backup>();
            var doc = XDocument.Load(xmlHistorialBackups);

            foreach (var e in doc.Root.Elements("Bitacora"))
            {
                lista.Add(new Backup
                {
                    Id = (int)e.Attribute("Id"),
                    Fecha = DateTime.Parse((string)e.Attribute("Fecha")),
                    UsernameUsuario = (string)e.Attribute("Usuario"),
                    Nombre = ((string)e.Element("Mensaje"))?.Replace("Backup generado: ", "")
                });
            }
            return lista;
        }

        public bool RestaurarBackup(Backup backup)
        {
            try
            {
                var origen = Path.Combine(xmlFolderBackups, $"{backup.Nombre}.xml");
                if (!File.Exists(origen)) return false;
                File.Copy(origen, rutaSistema, overwrite: true);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
