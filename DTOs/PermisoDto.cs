namespace DTOs
{
    public class PermisoDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public List<PermisoDto> Hijos { get; set; } = new();
    }
}
