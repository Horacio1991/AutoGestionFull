using System;
using System.Collections.Generic;
using BE;
using Mapper;

namespace BLL
{
    public class BLLPago
    {
        private readonly MPPPago _mapper;

        public BLLPago()
        {
            _mapper = new MPPPago();
        }

        public List<Pago> ObtenerTodos()
        {
            return _mapper.ListarTodo();
        }

        public Pago ObtenerPorId(int id)
        {
            if (id <= 0) throw new ArgumentException("ID inválido.", nameof(id));
            return _mapper.BuscarPorId(id);
        }

        public Pago RegistrarPago(Pago pago)
        {
            if (pago == null)
                throw new ArgumentNullException(nameof(pago));
            if (string.IsNullOrWhiteSpace(pago.TipoPago))
                throw new ApplicationException("Tipo de pago obligatorio.");
            if (pago.Monto <= 0)
                throw new ApplicationException("Monto de pago debe ser mayor a cero.");
            if (pago.Cuotas < 0)
                throw new ApplicationException("Cuotas inválidas.");

            _mapper.Alta(pago);
            return pago;
        }
    }
}
