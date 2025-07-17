using System.Collections.Generic;
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

        public List<RolDto> ObtenerRoles()
            => _mapper.ListarRolesDto();

        public List<PermisoDto> ObtenerPermisos()
            => _mapper.ListarPermisosDto();

        public List<PermisoDto> ObtenerPermisosUsuario(int usuarioId)
            => _mapper.ListarPermisosUsuarioDto(usuarioId);

        public bool CrearPermiso(string nombrePermiso)
            => _mapper.AltaPermiso(nombrePermiso);

        public bool EliminarPermiso(int permisoId)
            => _mapper.BajaPermiso(permisoId);

        public bool CrearRol(string nombreRol, List<int> permisoIds)
            => _mapper.AltaRol(nombreRol, permisoIds);

        public bool ModificarRol(int rolId, string nuevoNombre, List<int> permisoIds)
            => _mapper.ModificarRol(rolId, nuevoNombre, permisoIds);

        public bool EliminarRol(int rolId)
            => _mapper.BajaRol(rolId);

        // ————————————————
        // los métodos *que te faltaban* en la UI:
        public bool AsignarPermisoARol(int rolId, int permisoId)
            => _mapper.AsignarPermisoARol(rolId, permisoId);

        public bool EliminarPermisoDeRol(int rolId, int permisoId)
            => _mapper.QuitarPermisoDeRol(rolId, permisoId);

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
