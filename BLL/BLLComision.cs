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

        // Obtiene todas las comisiones filtradas y las mapea a DTOs.
        public List<ComisionListDto> ObtenerComisiones(int vendedorId, string estado, DateTime desde, DateTime hasta)
        {
            try
            {
                // 1) Filtrado de comisiones
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

                // 2) Mapeo a DTO
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
                        Estado = c.Estado,
                        MotivoRechazo = c.MotivoRechazo
                    };
                }).ToList();
            }
            catch (Exception ex)
            {
                return new List<ComisionListDto>(); //devuelve lista vacia en caso de error
            }
        }

        // Obtiene todas las ventas entregadas sin comisión y las mapea a DTO.
        public List<VentaComisionDto> ObtenerVentasSinComision()
        {
            try
            {
                var entregadas = _vtaMapper.ListarTodo()
                     .Where(v => v.Estado.Equals("Entregada", StringComparison.OrdinalIgnoreCase))
                     .ToList();

                var conCom = _comMapper.ListarTodo()
                             .Select(c => c.Venta.ID)
                             .ToHashSet();

                // Necesitás el MPPUsuario
                var usuMapper = new MPPUsuario();
                var pagoMapper = new MPPPago();

                return entregadas
                   .Where(v => !conCom.Contains(v.ID))
                   .Select(v =>
                   {
                       var cli = _cliMapper.BuscarPorId(v.Cliente.ID)!;
                       var veh = _vehMapper.BuscarPorId(v.Vehiculo.ID)!;
                       var pago = pagoMapper.BuscarPorId(v.Pago.ID); // Cargar pago completo
                       var vendedorObj = usuMapper.BuscarPorId(v.Vendedor.ID); // Cargar vendedor completo

                       return new VentaComisionDto
                       {
                           VentaID = v.ID,
                           Cliente = $"{cli.Nombre} {cli.Apellido}",
                           Vehiculo = $"{veh.Marca} {veh.Modelo} ({veh.Dominio})",
                           Vendedor = vendedorObj != null ? vendedorObj.Username : "N/A",
                           MontoVenta = pago != null ? pago.Monto : 0
                       };
                   })
                   .ToList();
            }
            catch (Exception ex)
            {
                return new List<VentaComisionDto>();
            }
        }

        // Registra una comisión para una venta.
        public bool RegistrarComision(ComisionInputDto dto)
        {
            try
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
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
