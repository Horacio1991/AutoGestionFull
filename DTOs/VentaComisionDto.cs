namespace DTOs
{
    public class VentaComisionDto
    {
        public int VentaID { get; set; }
        public string Vendedor { get; set; }    
        public string Cliente { get; set; }
        public string Vehiculo { get; set; }
        public decimal MontoVenta { get; set; } 
    }
}
