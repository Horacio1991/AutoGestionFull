namespace DTOs
{
    public class BackupDto
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Nombre { get; set; }
        public int UsuarioId { get; set; }
        public string UsernameUsuario { get; set; }
    }
}
