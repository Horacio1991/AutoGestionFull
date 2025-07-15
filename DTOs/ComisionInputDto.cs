namespace DTOs
{
    public class ComisionInputDto
    {
        public int VentaID { get; set; }
        public decimal Monto { get; set; }
        public string Estado { get; set; }          // "Aprobada" | "Rechazada"
        public string MotivoRechazo { get; set; }    // sólo si Estado=="Rechazada"
    }
}
