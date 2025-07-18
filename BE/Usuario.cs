using BE.BEComposite;

namespace BE
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public List<BEComponente> Rol { get; set; } = new List<BEComponente>();
        public Usuario() { }
    }
}
