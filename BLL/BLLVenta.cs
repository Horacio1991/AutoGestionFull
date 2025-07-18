using BE;
using DTOs;
using Mapper;

namespace BLL
{
    public class BLLVenta
    {
        private readonly MPPVenta _mpp = new MPPVenta();

        // Devuelve la lista de ventas con estado "Pendiente" (entidad completa).
        public List<Venta> ObtenerVentasPendientes()
        {
            try
            {
                return _mpp.ListarTodo()
                    .Where(v => v.Estado.Equals("Pendiente", StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }
            catch (Exception)
            {
                return new List<Venta>();
            }
        }

        // Autoriza una venta pendiente (cambia a "Aprobada").
        public bool AutorizarVenta(int ventaId, out string error)
        {
            error = null;
            try
            {
                var v = _mpp.BuscarPorId(ventaId)
                        ?? throw new ApplicationException("Venta no encontrada.");
                if (!v.Estado.Equals("Pendiente", StringComparison.OrdinalIgnoreCase))
                {
                    error = "La venta no está en estado Pendiente.";
                    return false;
                }

                _mpp.ActualizarEstado(ventaId, "Aprobada");
                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        // Rechaza una venta pendiente (cambia a "Rechazada" con motivo).
        public bool RechazarVenta(int ventaId, string motivo, out string error)
        {
            error = null;
            try
            {
                var v = _mpp.BuscarPorId(ventaId)
                        ?? throw new ApplicationException("Venta no encontrada.");
                if (!v.Estado.Equals("Pendiente", StringComparison.OrdinalIgnoreCase))
                {
                    error = "La venta no está en estado Pendiente.";
                    return false;
                }

                _mpp.ActualizarEstado(ventaId, "Rechazada", motivo);
                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        // Devuelve DTOs de ventas aprobadas, para emitir factura. (Estado Aprobada)
        public List<VentaDto> ObtenerVentasParaFacturar()
        {
            try
            {
                return _mpp.ListarTodo()
                    .Where(v => v.Estado.Equals("Aprobada", StringComparison.OrdinalIgnoreCase))
                    .Select(v => MapToDto(v))
                    .ToList();
            }
            catch (Exception)
            {
                return new List<VentaDto>();
            }
        }

        // Devuelve DTOs de ventas facturadas, para entrega.(Estado Facturada)
        public List<VentaDto> ObtenerVentasParaEntrega()
        {
            try
            {
                return _mpp.ListarTodo()
                    .Where(v => v.Estado.Equals("Facturada", StringComparison.OrdinalIgnoreCase))
                    .Select(v => MapToDto(v))
                    .ToList();
            }
            catch (Exception)
            {
                return new List<VentaDto>();
            }
        }

        // Marca una venta como entregada.
        public bool ConfirmarEntrega(int ventaId, out string error)
        {
            error = null;
            try
            {
                _mpp.ActualizarEstado(ventaId, "Entregada");
                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        // Devuelve la entidad venta completa (para PDF).
        public Venta ObtenerEntidad(int ventaId)
        {
            try
            {
                return _mpp.BuscarPorId(ventaId)
                       ?? throw new ApplicationException("Venta no encontrada.");
            }
            catch (Exception)
            {
                return null;
            }
        }

        // Devuelve la lista de ventas pendientes como DTOs.
        public List<VentaDto> ObtenerVentasPendientesDto()
        {
            try
            {
                return ObtenerVentasPendientes()
                    .Select(v => MapToDto(v))
                    .ToList();
            }
            catch (Exception)
            {
                return new List<VentaDto>();
            }
        }

        //Helper: mapea una venta BE a VentaDto, resolviendo nombres completos.
        private VentaDto MapToDto(Venta v)
        {
            // Cargamos nombre de cliente, vehículo y datos de pago desde los IDs.
            var cli = new MPPCliente().BuscarPorId(v.Cliente.ID);
            var veh = new MPPVehiculo().BuscarPorId(v.Vehiculo.ID);
            var pago = new MPPPago().BuscarPorId(v.Pago.ID);

            return new VentaDto
            {
                ID = v.ID,
                Cliente = cli != null ? $"{cli.Nombre} {cli.Apellido}" : "(Sin cliente)",
                Vehiculo = veh != null ? $"{veh.Marca} {veh.Modelo} ({veh.Dominio})" : "(Sin vehículo)",
                TipoPago = pago?.TipoPago ?? "",
                Monto = pago?.Monto ?? 0,
                Fecha = v.Fecha,
                Estado = v.Estado,
                MotivoRechazo = v.MotivoRechazo
            };
        }
    }
}
