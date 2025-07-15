// BLL/BLLTurno.cs
using BE;
using Mapper;
using System.Collections.Generic;

namespace BLL
{
    public class BLLTurno
    {
        private readonly MPPTurno _mppTurno;

        public BLLTurno()
        {
            _mppTurno = new MPPTurno();
        }

        /// <summary>
        /// Obtiene todos los turnos pendientes de asistencia.
        /// </summary>
        public List<Turno> ObtenerTurnosParaAsistencia()
        {
            return _mppTurno.ListarPendientesAsistencia();
        }

        /// <summary>
        /// Registra la asistencia (Asistió / No asistió) de un turno.
        /// </summary>
        public void RegistrarAsistencia(int turnoId, string estado, string observaciones)
        {
            _mppTurno.RegistrarAsistencia(turnoId, estado, observaciones);
        }

        /// <summary>
        /// Registra un nuevo turno.
        /// </summary>
        public void RegistrarTurno(Turno turno)
        {
            _mppTurno.AgregarTurno(turno);
        }
    }
}
