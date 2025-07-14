namespace BE
{
    public class Pago
    {
        public int ID { get; set; }
        public string TipoPago { get; set; }
        public decimal Monto { get; set; }
        public int Cuotas { get; set; }
        public string Detalles { get; set; }
        public DateTime FechaPago { get; set; } = DateTime.Now;
    }
}
