using BE;
using DTOs;
using Mapper;

namespace BLL
{
    public class BLLVenta
    {
        private readonly MPPVenta _mpp = new MPPVenta();

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

        private VentaDto MapToDto(Venta v)
        {
            // Carga nombre de cliente, vehículo y datos de pago desde los IDs.
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
