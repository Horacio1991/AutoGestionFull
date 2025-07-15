using System;
using System.Collections.Generic;
using System.Linq;
using BE;
using Mapper;

namespace BLL
{
    public class BLLFactura
    {
        private readonly MPPFactura _mapper = new MPPFactura();

        /// <summary>
        /// Obtiene todas las facturas registradas.
        /// </summary>
        public List<Factura> ListarTodo() =>
            _mapper.ListarTodo();

        /// <summary>
        /// Emite una nueva factura para la venta indicada:
        /// - Crea el registro de factura.
        /// - Marca la venta como facturada.
        /// </summary>
        public Factura EmitirFactura(int ventaId, Cliente cliente, Vehiculo vehiculo,
                                     string formaPago, decimal precio)
        {
            // 1) Crear BE.Factura
            var factura = new Factura
            {
                Cliente = cliente,
                Vehiculo = vehiculo,
                FormaPago = formaPago,
                Precio = precio
            };

            // 2) Persistir factura
            _mapper.AltaFactura(factura);

            // 3) Marcar venta como facturada
            _mapper.MarcarFacturada(ventaId);

            return factura;
        }

        /// <summary>
        /// Obtiene las facturas filtradas por ventaId (si se necesita) o todas.
        /// </summary>
        public Factura BuscarPorId(int facturaId) =>
            _mapper.BuscarPorId(facturaId);
    }
}
