namespace BE
{
    public class Turno
    {
        public int ID { get; set; }
        public Cliente Cliente { get; set; }
        public Vehiculo Vehiculo { get; set; } 
        public DateTime Fecha { get; set; }
        public TimeSpan Hora { get; set; }
        public string Asistencia { get; set; } 
        public string Observaciones { get; set; }
    }
}
