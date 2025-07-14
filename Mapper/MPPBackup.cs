using BE;
using Servicios.Utilidades;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Mapper
{
    public class MPPBackup
    {
        private readonly string _xmlBaseData = XmlPaths.BaseDatosLocal;
        private readonly string _xmlFolderBackups = XmlPaths.BackupFolder;
        private readonly string _xmlHistorialBackups = XmlPaths.BackupHistoryFile;

        public MPPBackup()
        {
            AsegurarHistorialBackup();
        }

        public List<Backup> ListarTodo()
        {
            var lista = new List<Backup>();
            try
            {
                if (!Directory.Exists(_xmlFolderBackups))
                    Directory.CreateDirectory(_xmlFolderBackups);

                foreach (var archivo in Directory.GetFiles(_xmlFolderBackups, "*.xml"))
                {
                    lista.Add(new Backup
                    {
                        Nombre = Path.GetFileNameWithoutExtension(archivo),
                        Fecha = File.GetCreationTime(archivo)
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error MPPBackup.ListarTodo: {ex.Message}");
            }
            return lista;
        }

        public bool CrearBackup(Backup backup)
        {
            try
            {
                if (!Directory.Exists(_xmlFolderBackups))
                    Directory.CreateDirectory(_xmlFolderBackups);

                var ts = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                backup.Nombre = $"Backup-{ts}";
                backup.Fecha = DateTime.Now;

                var destino = Path.Combine(_xmlFolderBackups, $"{backup.Nombre}.xml");
                File.Copy(_xmlBaseData, destino, overwrite: true);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error MPPBackup.CrearBackup: {ex.Message}");
                return false;
            }
        }

        public void GuardarBackupEnHistorial(Backup backup)
        {
            try
            {
                var doc = XDocument.Load(_xmlHistorialBackups);
                var root = doc.Root;
                var next = root.Elements("Backup").Max(e => (int?)e.Attribute("Id") ?? 0) + 1;

                var elem = new XElement("Backup",
                    new XAttribute("Id", next),
                    new XAttribute("Nombre", backup.Nombre),
                    new XAttribute("Fecha", backup.Fecha.ToString("yyyy-MM-dd HH:mm:ss")),
                    new XAttribute("IdUsuario", backup.IdUsuario),
                    new XAttribute("UsernameUsuario", backup.UsernameUsuario ?? "(desconocido)")
                );

                root.Add(elem);
                doc.Save(_xmlHistorialBackups);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error MPPBackup.GuardarBackupEnHistorial: {ex.Message}");
            }
        }

        private void AsegurarHistorialBackup()
        {
            try
            {
                var dir = Path.GetDirectoryName(_xmlHistorialBackups);
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);

                if (!File.Exists(_xmlHistorialBackups))
                {
                    new XDocument(new XElement("Backups"))
                        .Save(_xmlHistorialBackups);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error MPPBackup.AsegurarHistorialBackup: {ex.Message}");
            }
        }

        public List<Backup> ListarHistorial()
        {
            var lista = new List<Backup>();
            try
            {
                if (!File.Exists(_xmlHistorialBackups))
                    return lista;

                var doc = XDocument.Load(_xmlHistorialBackups);
                foreach (var e in doc.Root.Elements("Backup"))
                {
                    lista.Add(new Backup
                    {
                        Id = (int)e.Attribute("Id"),
                        Nombre = (string)e.Attribute("Nombre"),
                        Fecha = DateTime.Parse((string)e.Attribute("Fecha")),
                        IdUsuario = (int)e.Attribute("IdUsuario"),
                        UsernameUsuario = (string)e.Attribute("UsernameUsuario")
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error MPPBackup.ListarHistorial: {ex.Message}");
            }
            return lista;
        }

        public bool RestaurarBackup(Backup backup)
        {
            try
            {
                var fuente = Path.Combine(_xmlFolderBackups, $"{backup.Nombre}.xml");
                if (!File.Exists(fuente))
                {
                    Console.WriteLine("Backup no encontrado.");
                    return false;
                }

                File.Copy(fuente, _xmlBaseData, overwrite: true);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error MPPBackup.RestaurarBackup: {ex.Message}");
                return false;
            }
        }
    }
}
