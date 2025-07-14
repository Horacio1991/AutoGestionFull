using System;
using System.Collections.Generic;
using System.Linq;
using BE;
using Mapper;

namespace BLL
{
    public class BLLFactura
    {
        private readonly MPPFactura _mpp = new MPPFactura();

        /// <summary>
        /// Recupera todas las facturas existentes.
        /// </summary>
        public List<Factura> ListarTodo()
        {
            return _mpp.ListarTodo();
        }

        /// <summary>
        /// Busca una factura por su ID.
        /// </summary>
        public Factura BuscarPorId(int id)
        {
            return _mpp.BuscarPorId(id);
        }

        /// <summary>
        /// Emite (genera) una nueva factura.
        /// </summary>
        public void AltaFactura(Factura factura)
        {
            if (factura == null) throw new ArgumentNullException(nameof(factura));

            // Asignar ID secuencial
            var todas = _mpp.ListarTodo();
            factura.ID = todas.Any()
                ? todas.Max(f => f.ID) + 1
                : 1;

            factura.Fecha = DateTime.Now;
            _mpp.AltaFactura(factura);
        }

        
    }
}
