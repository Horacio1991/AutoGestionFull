using BE;
using DTOs;
using Mapper;
using AutoGestion.Servicios.Pdf;

namespace BLL
{
    public class BLLFactura
    {
        private readonly MPPFactura _mppFactura = new MPPFactura();
        private readonly MPPVenta _mppVenta = new MPPVenta();
        private readonly MPPCliente _mppCliente = new MPPCliente();
        private readonly MPPVehiculo _mppVehiculo = new MPPVehiculo();
        private readonly MPPPago _mppPago = new MPPPago();

        // Emite la factura para la venta indicada, marca la venta como facturada,
        // genera el PDF en la ruta destino y devuelve un DTO con los datos listos.
        public FacturaDto EmitirFactura(int ventaId, string rutaPdfDestino)
        {
            try
            {
                // 1) Recuperar datos completos de la venta
                var venta = _mppVenta.BuscarPorId(ventaId)
                            ?? throw new InvalidOperationException("Venta no encontrada.");

                var cliente = _mppCliente.BuscarPorId(venta.Cliente.ID)
                              ?? throw new InvalidOperationException("Cliente no encontrado.");
                var vehiculo = _mppVehiculo.BuscarPorId(venta.Vehiculo.ID)
                               ?? throw new InvalidOperationException("Vehículo no encontrado.");
                var pago = _mppPago.BuscarPorId(venta.Pago.ID)
                           ?? throw new InvalidOperationException("Pago no encontrado.");

                // 2) Crear y persistir Factura en XML
                var facturaBE = new Factura
                {
                    Cliente = cliente,
                    Vehiculo = vehiculo,
                    FormaPago = pago.TipoPago,
                    Precio = pago.Monto
                };
                _mppFactura.AltaFactura(facturaBE);

                // 3) Marcar venta como facturada
                _mppFactura.MarcarFacturada(ventaId);

                // 4) Generar el PDF
                GeneradorFacturaPDF.Generar(facturaBE, rutaPdfDestino);

                // 5) Mapear a DTO y devolver
                return new FacturaDto
                {
                    ID = facturaBE.ID,
                    Cliente = $"{cliente.Nombre} {cliente.Apellido}",
                    Vehiculo = $"{vehiculo.Marca} {vehiculo.Modelo} ({vehiculo.Dominio})",
                    FormaPago = facturaBE.FormaPago,
                    Precio = facturaBE.Precio,
                    Fecha = facturaBE.Fecha
                };
            }
            catch (Exception ex)
            {
                // Lanzás la excepción para manejar desde la UI, o podés mostrar un mensaje acá si lo preferís
                throw new ApplicationException("No se pudo emitir la factura. " + ex.Message, ex);
            }
        }
    }
}
