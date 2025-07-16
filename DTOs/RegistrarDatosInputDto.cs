namespace DTOs
{
    public class RegistrarDatosInputDto
    {
        public int OfertaID { get; set; }
        public string EstadoStock { get; set; }   // "Disponible" | "Requiere reacondicionamiento"
    }
}
