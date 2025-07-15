namespace DTOs
{
    public class FacturaDto
    {
        public int ID { get; set; }
        public string Cliente { get; set; }
        public string Vehiculo { get; set; }
        public string FormaPago { get; set; }
        public decimal Precio { get; set; }
        public DateTime Fecha { get; set; }
    }
}
