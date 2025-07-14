using BE.BEComposite;
using Mapper;
using MAPPER;
using System.Collections.Generic;

namespace BLL
{
    public class BLLComponente
    {
        private readonly MPPComponente _mapper;

        public BLLComponente()
        {
            _mapper = new MPPComponente();
        }

        public List<BEComponente> ListarComponente()
            => _mapper.ListarTodo();

        public List<BEComponente> ObtenerPermisosUsuario(int idUsuario)
            => _mapper.ListarPermisosUsuario(idUsuario);

        public List<BEComponente> ListarPermisosUsuario(int idUsuario)
            => _mapper.ListarPermisosUsuario(idUsuario);

        public bool CrearPermiso(string nombrePermiso)
            => _mapper.AltaPermiso(nombrePermiso);

        public bool BajaPermiso(int idPermiso)
            => _mapper.BajaPermiso(idPermiso);

        public bool CrearRol(string nombreRol, List<BEPermiso> permisos)
            => _mapper.AltaRol(nombreRol, permisos);

        public bool ModificarRol(BERol rol, List<BEPermiso> nuevosPermisos)
            => _mapper.ModificarRol(rol, nuevosPermisos);

        public bool BajaRol(int idRol)
            => _mapper.BajaRol(idRol);

        public bool AsignarRolAUsuario(int idUsuario, int idRol)
            => _mapper.AsignarRolAUsuario(idUsuario, idRol);

        public bool AsignarPermisoARol(int idRol, int idPermiso)
            => _mapper.AsignarPermisoARol(idRol, idPermiso);

        public bool QuitarPermisoDeRol(int idRol, int idPermiso)
            => _mapper.QuitarPermisoDeRol(idRol, idPermiso);

        public bool AsignarComponenteAUsuario(int idUsuario, BEComponente comp)
            => _mapper.AsignarComponenteAUsuario(idUsuario, comp.Id);

        public bool EliminarComponenteDeUsuario(int idUsuario, BEComponente comp)
            => _mapper.EliminarComponenteDeUsuario(idUsuario, comp.Id);

        public bool UsuarioTienePermisoDirectoOIndirecto(int idUsuario, int permisoId)
        {
            var permisosUsuario = ObtenerPermisosUsuario(idUsuario);
            return TienePermiso(permisosUsuario, permisoId);
        }

        private bool TienePermiso(List<BEComponente> componentes, int permisoId)
        {
            foreach (var componente in componentes)
            {
                if (componente.Id == permisoId)
                    return true;
                if (componente.Hijos.Count > 0 && TienePermiso(componente.Hijos, permisoId))
                    return true;
            }
            return false;
        }
    }
}
