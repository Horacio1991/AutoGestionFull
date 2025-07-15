using BE;
using Mapper;

namespace BLL
{
    public class BLLVenta
    {
        private readonly MPPVenta _mpp;

        public BLLVenta()
        {
            _mpp = new MPPVenta();
        }

        /// <summary>Obtiene todas las ventas (estado Pendiente, Aprobada, Facturada, etc.).</summary>
        public List<Venta> ListarTodas() => _mpp.ListarTodo();

        /// <summary>Registra un nuevo pago+y venta en estado "Pendiente".</summary>
        public void RegistrarVenta(Venta venta)
        {
            if (venta == null) throw new ArgumentNullException(nameof(venta));
            // validaciones...
            _mpp.Alta(venta);
        }

        /// <summary>Obtiene las ventas en estado "Pendiente" (para autorizar/rechazar).</summary>
        public List<Venta> ObtenerVentasPendientes()
            => ListarTodas().FindAll(v => v.Estado.Equals("Pendiente", StringComparison.OrdinalIgnoreCase));

        /// <summary>Autoriza una venta: cambia estado a "Aprobada".</summary>
        public bool AutorizarVenta(int ventaId)
        {
            var v = _mpp.BuscarPorId(ventaId);
            if (v == null) throw new ApplicationException("Venta no encontrada.");

            // no autorizar si ya está vendida o facturada
            if (!v.Estado.Equals("Pendiente", StringComparison.OrdinalIgnoreCase))
                return false;

            _mpp.ActualizarEstado(ventaId, "Aprobada");
            return true;
        }

        /// <summary>Rechaza una venta: cambia estado a "Rechazada" y guarda motivo.</summary>
        public bool RechazarVenta(int ventaId, string motivo)
        {
            var v = _mpp.BuscarPorId(ventaId);
            if (v == null) throw new ApplicationException("Venta no encontrada.");

            if (!v.Estado.Equals("Pendiente", StringComparison.OrdinalIgnoreCase))
                return false;

            _mpp.ActualizarEstado(ventaId, "Rechazada", motivo);
            return true;
        }

        /// <summary>Obtiene las ventas autorizadas y no aún facturadas.</summary>
        public List<Venta> ObtenerVentasParaFacturar()
            => ListarTodas().FindAll(v => v.Estado.Equals("Aprobada", StringComparison.OrdinalIgnoreCase));

        /// <summary>Marca una venta como "Facturada".</summary>
        public void MarcarFacturada(int ventaId)
        {
            _mpp.ActualizarEstado(ventaId, "Facturada");
        }

        /// <summary>Obtiene las ventas facturadas y no aún entregadas.</summary>
        public List<Venta> ObtenerVentasParaEntrega()
            => ListarTodas().FindAll(v => v.Estado.Equals("Facturada", StringComparison.OrdinalIgnoreCase));

        /// <summary>Marca una venta como "Entregada".</summary>
        public void ConfirmarEntrega(int ventaId)
        {
            _mpp.ActualizarEstado(ventaId, "Entregada");
        }
    }
}
