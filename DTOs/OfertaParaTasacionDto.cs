namespace DTOs
{
    public class OfertaParaTasacionDto
    {
        public int OfertaID { get; set; }
        public string VehiculoResumen { get; set; }     // p.ej. “Ford Fiesta (ABC123)”
        public string EstadoMotor { get; set; }
        public string EstadoCarroceria { get; set; }
        public string EstadoInterior { get; set; }
        public string EstadoDocumentacion { get; set; }
        public decimal? RangoMin { get; set; }
        public decimal? RangoMax { get; set; }
    }

   
}
