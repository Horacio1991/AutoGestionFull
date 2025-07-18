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

        // hace el backup y lo registra en bitácora
        public BackupDto RealizarBackup(int usuarioId, string username)
        {
            var backup = new Backup
            {
                IdUsuario = usuarioId,
                UsernameUsuario = username
            };

            // 1) crea el xml de backup.
            bool ok = _mpp.CrearBackup(backup);
            if (!ok)
                throw new ApplicationException("Error al crear el backup.");

            // 2) guarda un registro en el historial de backups.
            _mpp.GuardarBackupEnHistorial(backup);

            // 3) Registra la accion en la bitacora general del sistema.
            new BLLBitacora().RegistrarEvento(new BE.Bitacora
            {
                UsuarioID = usuarioId,
                UsuarioNombre = username,
                Detalle = "backup"
            });

            // 4) Devuelve un DTO representando el backup realizado.
            return new BackupDto
            {
                Id = backup.Id,
                Nombre = backup.Nombre,
                Fecha = backup.Fecha,
                UsuarioId = backup.IdUsuario,
                UsernameUsuario = backup.UsernameUsuario
            };
        }

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

        // Restaura el backup seleccionado, y lo registra en la bitácora.
        public void RestaurarBackup(string nombreBackup, int usuarioId, string username)
        {
            var backup = new Backup { Nombre = nombreBackup };
            bool ok = _mpp.RestaurarBackup(backup);
            if (!ok)
                throw new ApplicationException($"No se pudo restaurar el backup \"{nombreBackup}\".");

            new BLLBitacora().RegistrarEvento(new Bitacora
            {
                Detalle = "restore",
                UsuarioID = usuarioId,
                UsuarioNombre = username
            });
        }
    }
}
