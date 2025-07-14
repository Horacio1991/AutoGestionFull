using System;
using System.Collections.Generic;
using System.Linq;
using BE;
using Mapper;

namespace BLL
{
    public class BLLEvaluacionTecnica
    {
        private readonly MPPEvaluacionTecnica _mpp = new MPPEvaluacionTecnica();

        /// <summary>
        /// Recupera todas las evaluaciones técnicas registradas.
        /// </summary>
        public List<EvaluacionTecnica> ListarTodo()
        {
            return _mpp.ListarTodo();
        }

        /// <summary>
        /// Busca una evaluación técnica por su ID.
        /// </summary>
        public EvaluacionTecnica BuscarPorId(int id)
        {
            return _mpp.BuscarPorId(id);
        }

        /// <summary>
        /// Registra una nueva evaluación técnica.
        /// </summary>
        public void AltaEvaluacionTecnica(EvaluacionTecnica eval)
        {
            if (eval == null) throw new ArgumentNullException(nameof(eval));

            // Asignar ID secuencial
            var todas = _mpp.ListarTodo();
            eval.ID = todas.Any()
                ? todas.Max(e => e.ID) + 1
                : 1;

            // La fecha de inscripción puede fijarse aquí si tu BE la tuviera
            _mpp.AltaEvaluacion(eval);
        }
    }
}
