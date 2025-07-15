using System;
using System.Collections.Generic;
using System.Linq;
using BE;
using Mapper;

namespace BLL
{
    public class BLLComision
    {
        private readonly MPPComision _mapper = new MPPComision();

        /// <summary>
        /// Obtiene comisiones de un vendedor, filtradas por estado y rango de fechas.
        /// </summary>
        public List<Comision> ObtenerComisiones(int vendedorId, string estado, DateTime desde, DateTime hasta)
        {
            // Listar todas y luego filtrar
            var todas = _mapper.ListarTodo();

            return todas
                .Where(c =>
                    c.Venta?.Vendedor?.ID == vendedorId
                    && string.Equals(c.Estado, estado, StringComparison.OrdinalIgnoreCase)
                    && c.Fecha.Date >= desde.Date
                    && c.Fecha.Date <= hasta.Date
                )
                .ToList();
        }

        /// <summary>
        /// Registra una nueva comisión (aprobar/rechazar).
        /// </summary>
        public void RegistrarComision(Comision comision)
        {
            _mapper.AltaComision(comision);
        }
    }
}
