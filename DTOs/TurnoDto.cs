namespace DTOs
{
    public class TurnoDto
    {
        public int ID { get; set; }
        public string Cliente { get; set; }      
        public string Vehiculo { get; set; }      
        public DateTime Fecha { get; set; }
        public TimeSpan Hora { get; set; }
        public string Asistencia { get; set; }   
        public string Observaciones { get; set; }
    }
}
