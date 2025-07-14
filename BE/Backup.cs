namespace BE
{
    public class Backup
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Nombre { get; set; }
        public int IdUsuario { get; set; }
        public string UsernameUsuario { get; set; }

        public Backup(string nombre = "Backup_")
        {
            Fecha = DateTime.Now;
            Nombre = nombre;
        }
    }
}
