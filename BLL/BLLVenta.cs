using BE;
using DTOs;
using Mapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    public class BLLVenta
    {
        private readonly MPPVenta _mpp = new MPPVenta();

        // 1) Pantalla Autorizar/Rechazar: BE.Venta
        public List<Venta> ObtenerVentasPendientes() =>
            _mpp.ListarTodo()
                .Where(v => v.Estado.Equals("Pendiente", StringComparison.OrdinalIgnoreCase))
                .ToList();

        public bool AutorizarVenta(int ventaId)
        {
            var v = _mpp.BuscarPorId(ventaId)
                    ?? throw new ApplicationException("Venta no encontrada.");
            if (!v.Estado.Equals("Pendiente", StringComparison.OrdinalIgnoreCase))
                return false;

            // Ajuste: usamos "Aprobada" porque la UI filtra por ese estado
            _mpp.ActualizarEstado(ventaId, "Aprobada");
            return true;
        }

        public bool RechazarVenta(int ventaId, string motivo)
        {
            var v = _mpp.BuscarPorId(ventaId)
                    ?? throw new ApplicationException("Venta no encontrada.");
            if (!v.Estado.Equals("Pendiente", StringComparison.OrdinalIgnoreCase))
                return false;

            _mpp.ActualizarEstado(ventaId, "Rechazada", motivo);
            return true;
        }

        // 2) Pantalla EmitirFactura: DTOs de ventas "Aprobadas"
        public List<VentaDto> ObtenerVentasParaFacturar()
        {
            return _mpp.ListarTodo()
                .Where(v => v.Estado.Equals("Aprobada", StringComparison.OrdinalIgnoreCase))
                .Select(v => MapToDto(v))
                .ToList();
        }

        // 3) Pantalla RealizarEntrega: DTOs de ventas "Facturada"
        public List<VentaDto> ObtenerVentasParaEntrega()
        {
            return _mpp.ListarTodo()
                .Where(v => v.Estado.Equals("Facturada", StringComparison.OrdinalIgnoreCase))
                .Select(v => MapToDto(v))
                .ToList();
        }

        public void ConfirmarEntrega(int ventaId)
        {
            _mpp.ActualizarEstado(ventaId, "Entregada");
        }

        // 4) Cuando la UI necesita la entidad completa para el PDF
        public Venta ObtenerEntidad(int ventaId)
            => _mpp.BuscarPorId(ventaId)
               ?? throw new ApplicationException("Venta no encontrada.");

        // --- Helper para mapear BE.Venta → VentaDto ---
        private VentaDto MapToDto(Venta v)
        {
            // Cargamos nombre de cliente y vehículo desde los IDs
            var cli = new MPPCliente().BuscarPorId(v.Cliente.ID);
            var veh = new MPPVehiculo().BuscarPorId(v.Vehiculo.ID);
            var pago = new MPPPago().BuscarPorId(v.Pago.ID);

            return new VentaDto
            {
                ID = v.ID,
                Cliente = $"{cli.Nombre} {cli.Apellido}",
                Vehiculo = $"{veh.Marca} {veh.Modelo} ({veh.Dominio})",
                TipoPago = pago.TipoPago,
                Monto = pago.Monto,
                Fecha = v.Fecha,
                Estado = v.Estado,
                MotivoRechazo = v.MotivoRechazo
            };
        }
    }
}
