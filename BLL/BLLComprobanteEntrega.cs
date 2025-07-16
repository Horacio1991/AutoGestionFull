using BE;
using Mapper;

namespace BLL
{
    public class BLLComprobanteEntrega
    {
        private readonly MPPComprobanteEntrega _mpp;

        public BLLComprobanteEntrega()
        {
            _mpp = new MPPComprobanteEntrega();
        }

        /// <summary>Registra un comprobante de entrega para la venta indicada.</summary>
        public ComprobanteEntrega RegistrarComprobante(int ventaId)
        {
            var comprobante = new ComprobanteEntrega
            {
                Venta = new Venta { ID = ventaId }
            };

            _mpp.Alta(comprobante);
            return comprobante;
        }

        /// <summary>Obtiene todos los comprobantes de entrega.</summary>
        public List<ComprobanteEntrega> ListarTodos()
            => _mpp.ListarTodo();

        /// <summary>Obtiene los comprobantes de entrega asociados a una venta.</summary>
        public List<ComprobanteEntrega> ObtenerPorVenta(int ventaId)
            => _mpp.ListarPorVenta(ventaId);

        /// <summary>Busca un comprobante de entrega por su Id.</summary>
        public ComprobanteEntrega ObtenerPorId(int id)
            => _mpp.BuscarPorId(id);
    }
}
