namespace DTOs
{
    public class OfertaListDto
    {
        public int ID { get; set; }
        public string VehiculoResumen { get; set; }
        public DateTime FechaInspeccion { get; set; }

        public string DisplayTexto => $"{VehiculoResumen} – {FechaInspeccion:dd/MM/yyyy}";
    }

}
