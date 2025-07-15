namespace DTOs
{
    public class TurnoDto
    {
        public int ID { get; set; }
        public string Cliente { get; set; }       // nombre completo
        public string Vehiculo { get; set; }      // “Ford Fiesta (ABC123)”
        public DateTime Fecha { get; set; }
        public TimeSpan Hora { get; set; }
        public string Asistencia { get; set; }    // "Pendiente", "Asistió", ...
        public string Observaciones { get; set; }
    }
}
