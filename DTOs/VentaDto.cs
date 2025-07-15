namespace DTOs
{
    public class VentaDto
    {
        public int ID { get; set; }
        public string Cliente { get; set; }      // p.ej. "Juan Pérez"
        public string Vehiculo { get; set; }     // p.ej. "Ford Fiesta (ABC123)"
        public string TipoPago { get; set; }     // p.ej. "Tarjeta"
        public decimal Monto { get; set; }       // importe total
        public DateTime Fecha { get; set; }
        public string Estado { get; set; }       // "Pendiente", "Autorizada", "Rechazada", etc.
        public string MotivoRechazo { get; set; }
    }

}
