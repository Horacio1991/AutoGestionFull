using BE;
using Mapper;
using System;

namespace BLL
{
    public class BLLFactura
    {
        private readonly MPPFactura _mppFactura = new MPPFactura();

        /// <summary>
        /// Emite una nueva factura para la venta indicada,
        /// y marca la venta como facturada.
        /// </summary>
        public Factura EmitirFactura(int ventaId)
        {
            // 1) Recuperar venta (sus datos de cliente/vehículo/pago)
            //    Aquí asumo que tienes MPPVenta.ParseVenta cargado en tu BE.Venta
            var venta = new MPPVenta().BuscarPorId(ventaId)
                        ?? throw new InvalidOperationException("Venta no encontrada.");

            // 2) Crear y guardar la factura
            var factura = new Factura
            {
                Cliente = venta.Cliente,
                Vehiculo = venta.Vehiculo,
                FormaPago = venta.Pago.TipoPago,
                Precio = venta.Pago.Monto
            };
            _mppFactura.AltaFactura(factura);

            // 3) Marcar venta en el XML
            _mppFactura.MarcarFacturada(ventaId);

            return factura;
        }
    }
}
