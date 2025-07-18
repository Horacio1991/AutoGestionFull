using BE.BEComposite;

namespace BE
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        // Cada usuario puede tener uno o varios componentes (roles/permisos) asignados.
        public List<BEComponente> Rol { get; set; } = new List<BEComponente>();

        public Usuario() { }
    }
}
