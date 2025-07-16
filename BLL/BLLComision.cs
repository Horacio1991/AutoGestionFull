using BE;
using DTOs;
using Mapper;

namespace BLL
{
    public class BLLComision
    {
        private readonly MPPComision _mapper = new MPPComision();
        private readonly MPPVenta _mppVenta = new MPPVenta();

        /// <summary>
        /// Obtiene comisiones de un vendedor, filtradas por estado y rango de fechas,
        /// y las mapea a DTOs para la capa de presentación.
        /// </summary>
        public List<ComisionListDto> ObtenerComisiones(int vendedorId, string estado, DateTime desde, DateTime hasta)
        {
            var todas = _mapper.ListarTodo();

            var filtradas = todas
                .Where(c =>
                    c.Venta?.Vendedor?.ID == vendedorId
                    && string.Equals(c.Estado, estado, StringComparison.OrdinalIgnoreCase)
                    && c.Fecha.Date >= desde.Date
                    && c.Fecha.Date <= hasta.Date
                );

            // Mapeo a DTO
            return filtradas.Select(c => new ComisionListDto
            {
                ID = c.ID,
                Fecha = c.Fecha,
                Cliente = $"{c.Venta.Cliente.Nombre} {c.Venta.Cliente.Apellido}",
                Vehiculo = $"{c.Venta.Vehiculo.Marca} {c.Venta.Vehiculo.Modelo} ({c.Venta.Vehiculo.Dominio})",
                Monto = c.Monto,
                Estado = c.Estado,
                MotivoRechazo = c.MotivoRechazo
            }).ToList();
        }

        /// <summary>
        /// Lista las ventas con estado "Entregada" que aún no tienen comisión.
        /// </summary>
        public List<VentaComisionDto> ObtenerVentasSinComision()
        {
            var entregadas = _mppVenta.ListarTodo()
                .Where(v => v.Estado.Equals("Entregada", StringComparison.OrdinalIgnoreCase))
                .ToList();

            var existentes = _mapper.ListarTodo()
                .Select(c => c.Venta.ID)
                .ToHashSet();

            return entregadas
                .Where(v => !existentes.Contains(v.ID))
                .Select(v => new VentaComisionDto
                {
                    VentaID = v.ID,
                    Cliente = $"{v.Cliente.Nombre} {v.Cliente.Apellido}",
                    Vehiculo = $"{v.Vehiculo.Marca} {v.Vehiculo.Modelo} ({v.Vehiculo.Dominio})"
                })
                .ToList();
        }


        /// <summary>
        /// Registra una comisión (aprobar o rechazar) a partir de un DTO de entrada.
        /// </summary>
        public bool RegistrarComision(ComisionInputDto dto)
        {
            var com = new Comision
            {
                Venta = new Venta { ID = dto.VentaID },
                Monto = dto.Monto,
                Estado = dto.Estado,
                MotivoRechazo = dto.MotivoRechazo
            };
            _mapper.AltaComision(com);
            return true;
        }
    }
}
