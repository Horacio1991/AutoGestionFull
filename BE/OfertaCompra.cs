namespace BE
{
    public class OfertaCompra
    {
        public int ID { get; set; }
        public Oferente Oferente { get; set; }
        public Vehiculo Vehiculo { get; set; }
        public DateTime FechaInspeccion { get; set; }
        public string Estado { get; set; } = "En evaluación";
    }
}
