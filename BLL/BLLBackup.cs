using BE;
using DTOs;
using Mapper;

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
        public List<BackupDto> ObtenerBackupsDto()
        {
            return _mpp.ListarTodo()
                       .Select(b => new BackupDto
                       {
                           Id = b.Id,
                           Nombre = b.Nombre,
                           Fecha = b.Fecha,
                           UsuarioId = b.IdUsuario,
                           UsernameUsuario = b.UsernameUsuario
                       })
                       .ToList();
        }

        /// <summary>
        /// Ejecuta el backup y registra el historial, devolviendo un DTO con el nuevo backup.
        /// </summary>
        /// 
        /// <summary>
        /// Ejecuta el backup y registra el historial, devolviendo un DTO con el nuevo backup.
        /// </summary>
        public BackupDto RealizarBackup(int usuarioId, string username)
        {
            var backup = new Backup
            {
                IdUsuario = usuarioId,
                UsernameUsuario = username
            };

            // 1) Creación física
            bool ok = _mpp.CrearBackup(backup);
            if (!ok)
                throw new ApplicationException("Error al crear el backup.");

            // 2) Registrar en el XML de historial de backups
            _mpp.GuardarBackupEnHistorial(backup);

            // 3) Registrar también en la bitácora general
            new BLLBitacora().RegistrarEvento(new BE.Bitacora
            {
                UsuarioID = usuarioId,
                UsuarioNombre = username,
                Detalle = "backup"
            });

            // 4) Devolver el DTO
            return new BackupDto
            {
                Id = backup.Id,
                Nombre = backup.Nombre,
                Fecha = backup.Fecha,
                UsuarioId = backup.IdUsuario,
                UsernameUsuario = backup.UsernameUsuario
            };
        }


        /// <summary>
        /// Devuelve el historial de backups como lista de DTOs.
        /// </summary>
        public List<BackupDto> ObtenerHistorialDto()
        {
            return _mpp.ListarHistorial()
                       .Select(b => new BackupDto
                       {
                           Id = b.Id,
                           Nombre = b.Nombre,
                           Fecha = b.Fecha,
                           UsuarioId = b.IdUsuario,
                           UsernameUsuario = b.UsernameUsuario
                       })
                       .ToList();
        }

        /// <summary>
        /// Restaura el backup seleccionado.
        /// </summary>
        public void RestaurarBackup(string nombreBackup, int usuarioId, string username)
        {
            // Creamos un BE.Backup mínimo con el nombre
            var backup = new Backup { Nombre = nombreBackup };
            bool ok = _mpp.RestaurarBackup(backup);
            if (!ok)
                throw new ApplicationException($"No se pudo restaurar el backup \"{nombreBackup}\".");

            new BLLBitacora().RegistrarEvento(new Bitacora { Detalle = "restore", UsuarioID = usuarioId, UsuarioNombre = username });
        }
    }
}
