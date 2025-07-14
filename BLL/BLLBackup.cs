using BE;
using Mapper;


namespace BLL
{
    public class BLLBackup
    {
        private readonly MPPBackup _mpp = new MPPBackup();

        public List<Backup> ListarBackups()
        {
            return _mpp.ListarTodo();
        }

        public void CrearBackup(int usuarioId, string usuarioNombre)
        {
            var backup = new Backup
            {
                Nombre = "", // será asignado por el mapper
                IdUsuario = usuarioId,
                UsernameUsuario = usuarioNombre
            };

            bool creado = _mpp.CrearBackup(backup);
            if (!creado)
                throw new ApplicationException("No se pudo crear el backup.");

            _mpp.GuardarBackupEnHistorial(backup);
        }

        public List<Backup> ListarHistorial()
        {
            return _mpp.ListarHistorial();
        }

        public void RestaurarBackup(Backup backup)
        {
            if (backup == null) throw new ArgumentNullException(nameof(backup));

            bool ok = _mpp.RestaurarBackup(backup);
            if (!ok)
                throw new ApplicationException("No se pudo restaurar el backup.");
        }
    }
}
