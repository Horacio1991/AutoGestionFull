using DTOs;
using Mapper;

namespace BLL
{
    public class BLLComponente
    {
        private readonly MPPComponente _mapper;

        public BLLComponente()
        {
            _mapper = new MPPComponente();
        }

        // Listar roles disponibles
        public List<RolDto> ObtenerRoles()
            => _mapper.ListarRolesDto();

        // Listar permisos individuales
        public List<PermisoDto> ObtenerPermisos()
            => _mapper.ListarPermisosDto();

        // Permisos efectivos de un usuario (directos y por rol)
        public List<PermisoDto> ObtenerPermisosUsuario(int usuarioId)
            => _mapper.ListarPermisosUsuarioDto(usuarioId);

        // Crear/Borrar permiso individual
        public bool CrearPermiso(string nombrePermiso)
            => _mapper.AltaPermiso(nombrePermiso);

        public bool EliminarPermiso(int permisoId)
            => _mapper.BajaPermiso(permisoId);

        // Crear/modificar/borrar rol
        public bool CrearRol(string nombreRol, List<int> permisoIds)
            => _mapper.AltaRol(nombreRol, permisoIds);

        public bool ModificarRol(int rolId, string nuevoNombre, List<int> permisoIds)
            => _mapper.ModificarRol(rolId, nuevoNombre, permisoIds);

        public bool EliminarRol(int rolId)
            => _mapper.BajaRol(rolId);

        // Asignar/quitar permisos a un rol
        public bool AsignarPermisoARol(int rolId, int permisoId)
            => _mapper.AsignarPermisoARol(rolId, permisoId);

        public bool EliminarPermisoDeRol(int rolId, int permisoId)
            => _mapper.QuitarPermisoDeRol(rolId, permisoId);

        // Asignar/quitar roles o permisos a usuario
        public bool AsignarRolAUsuario(int usuarioId, int rolId)
            => _mapper.AsignarAUsuario(usuarioId, rolId);

        public bool EliminarRolDeUsuario(int usuarioId, int rolId)
            => _mapper.QuitarDeUsuario(usuarioId, rolId);

        public bool AsignarPermisoAUsuario(int usuarioId, int permisoId)
            => _mapper.AsignarAUsuario(usuarioId, permisoId);

        public bool EliminarPermisoDeUsuario(int usuarioId, int permisoId)
            => _mapper.QuitarDeUsuario(usuarioId, permisoId);
    }
}
