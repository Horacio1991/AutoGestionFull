namespace BE
{
    public class ComprobanteEntrega
    {
        public int ID { get; set; }
        public Venta Venta { get; set; }
        public DateTime FechaEntrega { get; set; }
    }
}
