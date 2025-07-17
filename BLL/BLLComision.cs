using BE;
using DTOs;
using Mapper;

namespace BLL
{
    public class BLLComision
    {
        private readonly MPPComision _comMapper = new MPPComision();
        private readonly MPPVenta _vtaMapper = new MPPVenta();
        private readonly MPPCliente _cliMapper = new MPPCliente();
        private readonly MPPVehiculo _vehMapper = new MPPVehiculo();

        /// <summary>
        /// Obtiene todas las comisiones (filtradas) y las mapea a DTOs ligeros.
        /// </summary>
        public List<ComisionListDto> ObtenerComisiones(int vendedorId,
                                                       string estado,
                                                       DateTime desde,
                                                       DateTime hasta)
        {
            // 1) filtramos en BE
            var lista = _comMapper.ListarTodo()
                     .Where(c =>
                     {
                         var v = _vtaMapper.BuscarPorId(c.Venta.ID);
                         return v != null
                             && v.Vendedor.ID == vendedorId
                             && c.Fecha.Date >= desde.Date
                             && c.Fecha.Date <= hasta.Date
                             && string.Equals(c.Estado, estado, StringComparison.OrdinalIgnoreCase);
                     });

            // 2) mapeamos a DTO
            return lista.Select(c =>
            {
                var venta = _vtaMapper.BuscarPorId(c.Venta.ID)!;
                var cli = _cliMapper.BuscarPorId(venta.Cliente.ID)!;
                var veh = _vehMapper.BuscarPorId(venta.Vehiculo.ID)!;

                return new ComisionListDto
                {
                    ID = c.ID,
                    Fecha = c.Fecha,
                    Cliente = $"{cli.Nombre} {cli.Apellido}",
                    Vehiculo = $"{veh.Marca} {veh.Modelo} ({veh.Dominio})",
                    Monto = c.Monto,
                    Estado = c.Estado
                };
            })
            .ToList();
        }

        /// <summary>
        /// Obtiene todas las ventas entregadas que no tienen comisión aún,
        /// y las mapea a DTO para registrar comisión.
        /// </summary>
        public List<VentaComisionDto> ObtenerVentasSinComision()
        {
            var entregadas = _vtaMapper.ListarTodo()
                 .Where(v => v.Estado.Equals("Entregada", StringComparison.OrdinalIgnoreCase))
                 .ToList();

            var conCom = _comMapper.ListarTodo()
                         .Select(c => c.Venta.ID)
                         .ToHashSet();

            return entregadas
               .Where(v => !conCom.Contains(v.ID))
               .Select(v =>
               {
                   var cli = _cliMapper.BuscarPorId(v.Cliente.ID)!;
                   var veh = _vehMapper.BuscarPorId(v.Vehiculo.ID)!;
                   return new VentaComisionDto
                   {
                       VentaID = v.ID,
                       Cliente = $"{cli.Nombre} {cli.Apellido}",
                       Vehiculo = $"{veh.Marca} {veh.Modelo} ({veh.Dominio})"
                   };
               })
               .ToList();
        }

        public bool RegistrarComision(ComisionInputDto dto)
        {
            var c = new Comision
            {
                Venta = new Venta { ID = dto.VentaID },
                Monto = dto.Monto,
                Estado = dto.Estado,
                MotivoRechazo = dto.MotivoRechazo
            };
            _comMapper.AltaComision(c);
            return true;
        }
    }
}
