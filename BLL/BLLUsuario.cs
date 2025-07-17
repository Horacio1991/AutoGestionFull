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

        /// <summary>
        /// Valida credenciales contra el XML.
        /// </summary>
        public bool ValidarLogin(string username, string passwordPlain)
        {
            var user = _mapper.BuscarPorUsername(username);
            if (user == null) return false;

            var encrypted = Encriptacion.EncriptarPassword(passwordPlain);
            return user.Password == encrypted;
        }

        /// <summary>
        /// Lista todos los usuarios como DTOs (sin exponer BE).
        /// </summary>
        public List<UsuarioDto> ListarUsuariosDto()
        {
            var beUsuarios = _mapper.ListarTodo();
            var lista = new List<UsuarioDto>();

            foreach (var u in beUsuarios)
            {
                // obtengo directamente los permisos como DTOs
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

        /// <summary>
        /// Registra un nuevo usuario (username + contraseña en texto plano).
        /// </summary>
        public void RegistrarUsuario(string username, string passwordPlain)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("Username es obligatorio.");
            if (_mapper.BuscarPorUsername(username) != null)
                throw new InvalidOperationException("El usuario ya existe.");

            var encrypted = Encriptacion.EncriptarPassword(passwordPlain);
            var be = new Usuario { Username = username, Password = encrypted };
            _mapper.Agregar(be);
        }

        /// <summary>
        /// Modifica usuario existente (por ID).
        /// </summary>
        public void ModificarUsuario(int userId, string newUsername, string newPasswordPlain)
        {
            var existing = _mapper.ListarTodo().FirstOrDefault(u => u.Id == userId);
            if (existing == null)
                throw new InvalidOperationException("Usuario no encontrado.");

            // si cambia el username, verificar duplicado
            if (!existing.Username.Equals(newUsername, StringComparison.OrdinalIgnoreCase)
                && _mapper.BuscarPorUsername(newUsername) != null)
            {
                throw new InvalidOperationException("Ya existe otro usuario con ese nombre.");
            }

            existing.Username = newUsername;
            existing.Password = Encriptacion.EncriptarPassword(newPasswordPlain);
            _mapper.Actualizar(existing);
        }

        /// <summary>
        /// Baja lógica de usuario.
        /// </summary>
        public void EliminarUsuario(int userId)
        {
            _mapper.Eliminar(userId);
        }

        /// <summary>
        /// Mapea recursivamente un BEComponente a PermisoDto.
        /// </summary>
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

        /// <summary>
        /// Devuelve la contraseña en texto plano para un usuario existente.
        /// </summary>
        public string ObtenerPasswordPlain(int userId)
        {
            var be = _mapper.ListarTodo().FirstOrDefault(u => u.Id == userId);
            if (be == null) throw new ApplicationException("Usuario no encontrado.");
            return Encriptacion.DesencriptarPassword(be.Password);
        }

        /// <summary>
        /// Devuelve un UsuarioDto con todos los datos del usuario (ID, Username, Permisos).
        /// </summary>

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
    }
}
