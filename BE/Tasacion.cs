namespace BE
{
    public class Tasacion
    {
        public int ID { get; set; }
        public OfertaCompra Oferta { get; set; }
        public decimal ValorFinal { get; set; }
        public DateTime Fecha { get; set; }
        public string EstadoStock { get; set; }
    }

}
