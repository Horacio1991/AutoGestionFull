// BLL/BLLBackup.cs
using BE;
using Mapper;
using System.Collections.Generic;

namespace BLL
{
    public class BLLBackup
    {
        private readonly MPPBackup _mpp;

        public BLLBackup()
        {
            _mpp = new MPPBackup();
        }

        /// <summary>
        /// Devuelve la lista de backups disponibles (archivos).
        /// </summary>
        public List<Backup> ObtenerBackups()
        {
            return _mpp.ListarTodo();
        }

        /// <summary>
        /// Crea un nuevo backup y lo registra en el historial.
        /// </summary>
        public bool RealizarBackup(int usuarioId, string username)
        {
            var backup = new Backup
            {
                IdUsuario = usuarioId,
                UsernameUsuario = username
            };

            bool ok = _mpp.CrearBackup(backup);
            if (ok)
                _mpp.GuardarBackupEnHistorial(backup);

            return ok;
        }

        /// <summary>
        /// Devuelve el historial de backups (registros).
        /// </summary>
        public List<Backup> ObtenerHistorial()
        {
            return _mpp.ListarHistorial();
        }

        /// <summary>
        /// Restaura el backup seleccionado.
        /// </summary>
        public bool RestaurarBackup(Backup backup)
        {
            return _mpp.RestaurarBackup(backup);
        }
    }
}
