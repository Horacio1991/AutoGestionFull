
namespace Servicios.Utilidades
{
    public static class XmlPaths
    {
        private static readonly string BaseDir =
            AppDomain.CurrentDomain.BaseDirectory;

        public static string BaseDatosLocal =>
            Path.Combine(BaseDir, "BaseDeDatos", "BaseDatosLocal.xml");

        public static string Bitacoras =>
            Path.Combine(BaseDir, "BaseDeDatos", "Bitacoras.xml");

        public static string BackupFolder =>
            Path.Combine(BaseDir, "Backup");

        public static string BackupHistoryFile =>
            Path.Combine(BackupFolder, "HistorialBackup.xml");
    }
}
