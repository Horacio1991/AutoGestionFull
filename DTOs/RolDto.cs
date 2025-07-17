namespace DTOs
{
    public class RolDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public List<PermisoDto> Permisos { get; set; } = new();
    }

}
