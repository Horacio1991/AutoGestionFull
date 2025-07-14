namespace BE
{
    public class Turno
    {
        public int ID { get; set; }
        public Cliente Cliente { get; set; }
        public Vehiculo Vehiculo { get; set; } // Vehiculo que se atiende en el turno
        public DateTime Fecha { get; set; }
        public TimeSpan Hora { get; set; }
        public string Asistencia { get; set; } // Pendiente / Asistió / No asistió
        public string Observaciones { get; set; }
    }
}
