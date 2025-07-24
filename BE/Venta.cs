namespace BE
{
    public class Venta
    {
        public int ID { get; set; }
        public Vendedor Vendedor { get; set; }
        public Cliente Cliente { get; set; }
        public Vehiculo Vehiculo { get; set; }
        public Pago Pago { get; set; }
        public string Estado { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
        public string MotivoRechazo { get; set; }

    }
}
