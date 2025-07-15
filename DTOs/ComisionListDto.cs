namespace DTOs
{
    public class ComisionListDto
    {
        public int ID { get; set; }
        public DateTime Fecha { get; set; }
        public string Cliente { get; set; }
        public string Vehiculo { get; set; }
        public decimal Monto { get; set; }
        public string Estado { get; set; }
    }
}
