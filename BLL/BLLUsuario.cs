using BE;
using BE.BEComposite;
using DTOs;
using Mapper;
using Servicios;

namespace BLL
{
    public class BLLUsuario
    {
        private readonly MPPUsuario _mapper;
        private readonly BLLComponente _bllComponente;

        public BLLUsuario()
        {
            _mapper = new MPPUsuario();
            _bllComponente = new BLLComponente();
        }

        // Valida credenciales contra el XML.
        public bool ValidarLogin(string username, string passwordPlain)
        {
            try
            {
                var user = _mapper.BuscarPorUsername(username);
                if (user == null) return false;

                var encrypted = Encriptacion.EncriptarPassword(passwordPlain);
                return user.Password == encrypted;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<UsuarioDto> ListarUsuariosDto()
        {
            try
            {
                var beUsuarios = _mapper.ListarTodo();
                var lista = new List<UsuarioDto>();

                foreach (var u in beUsuarios)
                {
                    var permisosDto = _bllComponente.ObtenerPermisosUsuario(u.Id);

                    lista.Add(new UsuarioDto
                    {
                        ID = u.Id,
                        Username = u.Username,
                        Permisos = permisosDto
                    });
                }

                return lista;
            }
            catch (Exception)
            {
                return new List<UsuarioDto>();
            }
        }

        public bool RegistrarUsuario(string username, string passwordPlain, out string error)
        {
            error = null;
            try
            {
                if (string.IsNullOrWhiteSpace(username))
                    throw new ArgumentException("Username es obligatorio.");
                if (_mapper.BuscarPorUsername(username) != null)
                    throw new InvalidOperationException("El usuario ya existe.");

                var encrypted = Encriptacion.EncriptarPassword(passwordPlain);
                var be = new Usuario { Username = username, Password = encrypted };
                _mapper.Agregar(be);
                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        public bool ModificarUsuario(int userId, string newUsername, string newPasswordPlain, out string error)
        {
            error = null;
            try
            {
                var existing = _mapper.ListarTodo().FirstOrDefault(u => u.Id == userId);
                if (existing == null)
                    throw new InvalidOperationException("Usuario no encontrado.");

                // Si cambia el username verifica duplicado
                if (!existing.Username.Equals(newUsername, StringComparison.OrdinalIgnoreCase)
                    && _mapper.BuscarPorUsername(newUsername) != null)
                {
                    throw new InvalidOperationException("Ya existe otro usuario con ese nombre.");
                }

                existing.Username = newUsername;
                existing.Password = Encriptacion.EncriptarPassword(newPasswordPlain);
                _mapper.Actualizar(existing);
                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        public bool EliminarUsuario(int userId, out string error)
        {
            error = null;
            try
            {
                _mapper.Eliminar(userId);
                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        public string ObtenerPasswordPlain(int userId)
        {
            var be = _mapper.ListarTodo().FirstOrDefault(u => u.Id == userId);
            if (be == null) throw new ApplicationException("Usuario no encontrado.");
            return Encriptacion.DesencriptarPassword(be.Password);
        }

        // Devuelve usuario (ID, Username, Permisos).
        public UsuarioDto ObtenerUsuarioDto(string username)
        {
            var be = _mapper.BuscarPorUsername(username)
                     ?? throw new InvalidOperationException("Usuario no encontrado.");

            var permisosDto = _bllComponente.ObtenerPermisosUsuario(be.Id);

            return new UsuarioDto
            {
                ID = be.Id,
                Username = be.Username,
                Permisos = permisosDto
            };
        }

        public string ObtenerPasswordEncrypted(int userId)
        {
            var be = _mapper.ListarTodo().FirstOrDefault(u => u.Id == userId)
                     ?? throw new ApplicationException("Usuario no encontrado.");
            return be.Password;
        }

        //Mapea recursivamente un BEComponente (Permiso o Rol) a PermisoDto.
        private PermisoDto MapComponenteADto(BEComponente comp)
        {
            var pd = new PermisoDto
            {
                Id = comp.Id,
                Nombre = comp.Nombre
            };
  
            foreach (var hijo in comp.Hijos)
                pd.Hijos.Add(MapComponenteADto(hijo)); 
            return pd;
        }
    }
}
