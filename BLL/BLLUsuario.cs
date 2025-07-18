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

        //Lista todos los usuarios como DTOs 
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

        // Registra un nuevo usuario (username + contraseña en texto plano).
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

        // Modifica usuario existente (por ID).
        public bool ModificarUsuario(int userId, string newUsername, string newPasswordPlain, out string error)
        {
            error = null;
            try
            {
                var existing = _mapper.ListarTodo().FirstOrDefault(u => u.Id == userId);
                if (existing == null)
                    throw new InvalidOperationException("Usuario no encontrado.");

                // Si cambia el username, verificar duplicado
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

        // Baja lógica de usuario. Porque no se elimina físicamente, sino que se marca como inactivo.
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

        // Devuelve la contraseña en texto plano para un usuario existente.
        public string ObtenerPasswordPlain(int userId)
        {
            var be = _mapper.ListarTodo().FirstOrDefault(u => u.Id == userId);
            if (be == null) throw new ApplicationException("Usuario no encontrado.");
            return Encriptacion.DesencriptarPassword(be.Password);
        }

        // Devuelve un UsuarioDto con todos los datos del usuario (ID, Username, Permisos).
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

        // Devuelve la contraseña encriptada
        public string ObtenerPasswordEncrypted(int userId)
        {
            var be = _mapper.ListarTodo().FirstOrDefault(u => u.Id == userId)
                     ?? throw new ApplicationException("Usuario no encontrado.");
            return be.Password;
        }

        //Mapea recursivamente un BEComponente (Permiso o Rol) a PermisoDto.
        // esto es para construir la estructura de permisos del usuario.
        private PermisoDto MapComponenteADto(BEComponente comp)
        {
            // 1) DTO base para el componente
            var pd = new PermisoDto
            {
                Id = comp.Id,
                Nombre = comp.Nombre
                //Lista de hijos vacia por defecto
            };
            // 2) Recorro cada hijo (Si es PS va a estar vacia)
            foreach (var hijo in comp.Hijos)
                pd.Hijos.Add(MapComponenteADto(hijo)); //Agrego el hijo (mapeado) a la lista
            return pd;
        }
    }
}
