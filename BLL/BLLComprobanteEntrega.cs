using System;
using System.Collections.Generic;
using System.Linq;
using BE;
using Mapper;

namespace BLL
{
    public class BLLComprobanteEntrega
    {
        private readonly MPPComprobanteEntrega _mpp = new MPPComprobanteEntrega();

        /// <summary>
        /// Recupera todos los comprobantes de entrega activos.
        /// </summary>
        public List<ComprobanteEntrega> ListarTodo()
        {
            return _mpp.ListarTodo();
        }

        /// <summary>
        /// Busca un comprobante de entrega por su ID.
        /// </summary>
        public ComprobanteEntrega BuscarPorId(int id)
        {
            return _mpp.BuscarPorId(id);
        }

        /// <summary>
        /// Registra un nuevo comprobante de entrega.
        /// </summary>
        public void AltaComprobanteEntrega(ComprobanteEntrega comprobante)
        {
            if (comprobante == null) throw new ArgumentNullException(nameof(comprobante));

            // Asignar ID secuencial
            var todos = _mpp.ListarTodo();
            comprobante.ID = todos.Any()
                ? todos.Max(c => c.ID) + 1
                : 1;

            comprobante.FechaEntrega = DateTime.Now;
            _mpp.AltaComprobante(comprobante);
        }
    }
}
