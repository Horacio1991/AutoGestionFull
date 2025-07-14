using BE.BEComposite;
using System.Collections.Generic;

namespace BE
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        // lista de roles/permisos (componentes del composite)
        public List<BEComponente> Rol { get; set; } = new List<BEComponente>();

        public Usuario() { }

        public Usuario(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public Usuario(int id, string username, string password)
        {
            Id = id;
            Username = username;
            Password = password;
        }
    }
}
