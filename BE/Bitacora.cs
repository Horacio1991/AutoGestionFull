namespace BE
{
    public class Bitacora
    {
        public int ID { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string Detalle { get; set; }
        public int UsuarioID { get; set; }
        public string UsuarioNombre { get; set; }
    }
}
