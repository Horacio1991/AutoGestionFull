using System.Collections.Generic;
using BE.BEComposite;
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

        public List<BEComponente> ListarComponente()
            => _mapper.ListarTodo();

        public List<BEComponente> ObtenerPermisosUsuario(int idUsuario)
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

        public bool AsignarComponenteAUsuario(int idUsuario, BEComponente comp)
            => _mapper.AsignarComponenteAUsuario(idUsuario, comp.Id);

        public bool EliminarComponenteDeUsuario(int idUsuario, BEComponente comp)
            => _mapper.EliminarComponenteDeUsuario(idUsuario, comp.Id);

        public bool UsuarioTienePermisoDirectoOIndirecto(int idUsuario, int permisoId)
        {
            var permis = ObtenerPermisosUsuario(idUsuario);
            return TienePermiso(permis, permisoId);
        }

        private bool TienePermiso(IEnumerable<BEComponente> comps, int permisoId)
        {
            foreach (var c in comps)
            {
                if (c.Id == permisoId) return true;
                if (c is BERol rol && TienePermiso(rol.Hijos, permisoId))
                    return true;
            }
            return false;
        }
    }
}
