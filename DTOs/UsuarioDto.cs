namespace DTOs
{
    public class UsuarioDto
    {
        public int ID { get; set; }
        public string Username { get; set; }

        public string PasswordEncrypted { get; set; }
        public List<PermisoDto> Permisos { get; set; }
    }

}
