using System;
using System.Collections.Generic;
using System.Linq;
using BE;
using Mapper;

namespace BLL
{
    public class BLLTurno
    {
        private readonly MPPTurno _mpp = new MPPTurno();

        /// <summary>
        /// Lista todos los turnos.
        /// </summary>
        public List<Turno> ListarTodo()
        {
            return _mpp.ListarTodo();
        }

        /// <summary>
        /// Obtiene los turnos pendientes de registrar asistencia.
        /// </summary>
        public List<Turno> ObtenerParaAsistencia()
        {
            return _mpp.ListarTodo()
                       .Where(t => t.Fecha.Date <= DateTime.Today
                                   && string.Equals(t.Asistencia, "Pendiente", StringComparison.OrdinalIgnoreCase))
                       .ToList();
        }

        /// <summary>
        /// Busca un turno por su ID.
        /// </summary>
        public Turno BuscarPorId(int id)
        {
            return ListarTodo().FirstOrDefault(t => t.ID == id);
        }

        /// <summary>
        /// Registra un nuevo turno.
        /// </summary>
        public void RegistrarTurno(Turno turno)
        {
            if (turno == null) throw new ArgumentNullException(nameof(turno));
            if (turno.Cliente == null) throw new ArgumentException("El turno debe referenciar un cliente.", nameof(turno.Cliente));
            if (turno.Vehiculo == null) throw new ArgumentException("El turno debe referenciar un vehículo.", nameof(turno.Vehiculo));
            if (turno.Fecha.Date < DateTime.Today) throw new ArgumentException("La fecha del turno no puede ser pasada.", nameof(turno.Fecha));

            var todos = _mpp.ListarTodo();
            turno.ID = todos.Any() ? todos.Max(t => t.ID) + 1 : 1;
            turno.Asistencia = "Pendiente";
            _mpp.AltaTurno(turno);
        }

        /// <summary>
        /// Registra la asistencia de un turno.
        /// </summary>
        public void RegistrarAsistencia(int turnoId, string estado, string observaciones = null)
        {
            var turno = BuscarPorId(turnoId)
                       ?? throw new ApplicationException("Turno no encontrado.");
            if (string.IsNullOrWhiteSpace(estado)) throw new ArgumentException("Debe indicar un estado de asistencia.", nameof(estado));

            turno.Asistencia = estado;
            turno.Observaciones = observaciones?.Trim();
            _mpp.ModificarTurno(turno);
        }

        /// <summary>
        /// Anula (baja) un turno existente.
        /// </summary>
        public void AnularTurno(int id)
        {
            var t = BuscarPorId(id)
                    ?? throw new ApplicationException("Turno no encontrado.");
            _mpp.BajaTurno(id);
        }
    }
}
