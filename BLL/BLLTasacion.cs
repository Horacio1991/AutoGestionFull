using System;
using System.Collections.Generic;
using System.Linq;
using BE;
using Mapper;

namespace BLL
{
    public class BLLTasacion
    {
        private readonly MPPTasacion _mpp = new MPPTasacion();

        /// <summary>
        /// Lista todas las tasaciones activas.
        /// </summary>
        public List<Tasacion> ListarTodo()
        {
            return _mpp.ListarTodo();
        }

        /// <summary>
        /// Busca una tasación por su ID.
        /// </summary>
        public Tasacion BuscarPorId(int id)
        {
            return ListarTodo().FirstOrDefault(t => t.ID == id);
        }

        /// <summary>
        /// Registra una nueva tasación.
        /// </summary>
        public void RegistrarTasacion(Tasacion tasacion)
        {
            if (tasacion == null) throw new ArgumentNullException(nameof(tasacion));
            if (tasacion.Oferta == null) throw new ArgumentException("La tasación debe referenciar una oferta.", nameof(tasacion.Oferta));
            if (tasacion.ValorFinal <= 0) throw new ArgumentException("El valor final debe ser mayor que cero.", nameof(tasacion.ValorFinal));

            var todas = _mpp.ListarTodo();
            tasacion.ID = todas.Any() ? todas.Max(x => x.ID) + 1 : 1;
            tasacion.Fecha = tasacion.Fecha == default ? DateTime.Now : tasacion.Fecha;

            _mpp.Alta(tasacion);
        }

        /// <summary>
        /// Anula (baja) una tasación existente.
        /// </summary>
        public void AnularTasacion(int id)
        {
            var t = BuscarPorId(id)
                    ?? throw new ApplicationException("Tasación no encontrada.");
            _mpp.Baja(id);
        }
    }
}
