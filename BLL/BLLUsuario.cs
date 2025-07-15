using BE;
using BE.BEComposite;
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

        // Autenticación
        public bool ValidarLogin(string username, string passwordPlain)
        {
            var user = _mapper.BuscarPorUsername(username);
            if (user == null) return false;

            var encrypted = Encriptacion.EncriptarPassword(passwordPlain);
            return user.Password == encrypted;
        }

        // Listar todos
        public List<Usuario> ListarTodo() => _mapper.ListarTodo();

        // Registro
        public void RegistrarUsuario(string username, string passwordPlain)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("Username es obligatorio.");
            if (_mapper.BuscarPorUsername(username) != null)
                throw new InvalidOperationException("El usuario ya existe.");

            var encrypted = Encriptacion.EncriptarPassword(passwordPlain);
            var user = new Usuario { Username = username, Password = encrypted };
            _mapper.Agregar(user);
        }

        // Existencia
        public bool ExisteUsuario(string username) =>
            _mapper.BuscarPorUsername(username) != null;

        // Modificación
        public void ModificarUsuario(int userId, string newUsername, string newPasswordPlain)
        {
            var existing = _mapper.ListarTodo().Find(u => u.Id == userId);
            if (existing == null)
                throw new InvalidOperationException("Usuario no encontrado.");

            existing.Username = newUsername;
            existing.Password = Encriptacion.EncriptarPassword(newPasswordPlain);
            _mapper.Actualizar(existing);
        }

        // Eliminación
        public void EliminarUsuario(int userId)
        {
            _mapper.Eliminar(userId);
        }

        // Roles / Permisos
        public List<BEComponente> ListarComponente() =>
            _bllComponente.ListarComponente();

        public List<BEComponente> ObtenerPermisosUsuario(int userId) =>
            _bllComponente.ObtenerPermisosUsuario(userId);
    }
}
