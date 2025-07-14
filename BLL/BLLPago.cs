using System;
using System.Collections.Generic;
using System.Linq;
using BE;
using Mapper;

namespace BLL
{
    public class BLLPago
    {
        private readonly MPPPago _mpp = new MPPPago();

        /// <summary>
        /// Devuelve todos los pagos registrados.
        /// </summary>
        public List<Pago> ListarTodo()
        {
            return _mpp.ListarTodo();
        }

        /// <summary>
        /// Busca un pago por su ID.
        /// </summary>
        public Pago BuscarPorId(int id)
        {
            return _mpp.ListarTodo().FirstOrDefault(p => p.ID == id);
        }

        /// <summary>
        /// Registra un nuevo pago.
        /// </summary>
        public void RegistrarPago(Pago pago)
        {
            if (pago == null) throw new ArgumentNullException(nameof(pago));
            if (pago.Monto <= 0) throw new ArgumentException("El monto debe ser mayor que cero.", nameof(pago.Monto));
            if (string.IsNullOrWhiteSpace(pago.TipoPago)) throw new ArgumentException("Debe indicar el tipo de pago.", nameof(pago.TipoPago));

            // asignar ID
            var todos = _mpp.ListarTodo();
            pago.ID = todos.Any() ? todos.Max(x => x.ID) + 1 : 1;
            pago.FechaPago = pago.FechaPago == default
                ? DateTime.Now
                : pago.FechaPago;

            _mpp.AltaPago(pago);
        }

        
    }
}
