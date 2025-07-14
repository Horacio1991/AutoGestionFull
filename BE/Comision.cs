namespace BE
{
    public class Comision
    {
        public int ID { get; set; }
        public Venta Venta { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Monto { get; set; }
        public string Estado { get; set; }
        public string MotivoRechazo { get; set; }
    }
}
