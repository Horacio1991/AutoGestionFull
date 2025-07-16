using BE;
using Mapper;
using AutoGestion.Servicios.Pdf; // GeneradorComprobantePDF

namespace BLL
{
    public class BLLComprobanteEntrega
    {
        private readonly MPPComprobanteEntrega _mppComp = new MPPComprobanteEntrega();
        private readonly MPPVenta _mppVenta = new MPPVenta();
        private readonly MPPCliente _mppCliente = new MPPCliente();
        private readonly MPPVehiculo _mppVehiculo = new MPPVehiculo();
        private readonly MPPPago _mppPago = new MPPPago();

        /// <summary>
        /// Registra el comprobante, marca la venta como entregada,
        /// recupera la entidad completa y genera el PDF.
        /// </summary>
        public void EmitirComprobantePdf(int ventaId, string rutaPdf)
        {
            // 1) Registro de comprobante
            _mppComp.Alta(new ComprobanteEntrega { Venta = new Venta { ID = ventaId } });

            // 2) Marcar la venta como entregada
            _mppVenta.ActualizarEstado(ventaId, "Entregada");

            // 3) Recuperar la venta completa (IDs → entidades llenas)
            var venta = _mppVenta.BuscarPorId(ventaId)
                        ?? throw new ApplicationException("Venta no encontrada.");

            venta.Cliente = _mppCliente.BuscarPorId(venta.Cliente.ID);
            venta.Vehiculo = _mppVehiculo.BuscarPorId(venta.Vehiculo.ID);
            venta.Pago = _mppPago.BuscarPorId(venta.Pago.ID);

            // 4) Generar el PDF con todos los datos ya poblados
            GeneradorComprobantePDF.Generar(venta, rutaPdf);
        }
    }
}
