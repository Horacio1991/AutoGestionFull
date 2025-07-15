using System;
using System.Collections.Generic;
using BE;
using Mapper;

namespace BLL
{
    public class BLLEvaluacionTecnica
    {
        private readonly MPPEvaluacionTecnica _mapper;

        public BLLEvaluacionTecnica()
        {
            _mapper = new MPPEvaluacionTecnica();
        }

        public List<EvaluacionTecnica> ObtenerTodas()
        {
            return _mapper.ListarTodo();
        }

        public EvaluacionTecnica ObtenerPorId(int id)
        {
            return _mapper.BuscarPorId(id);
        }

        public void Registrar(EvaluacionTecnica eval)
        {
            if (string.IsNullOrWhiteSpace(eval.EstadoMotor) ||
                string.IsNullOrWhiteSpace(eval.EstadoCarroceria) ||
                string.IsNullOrWhiteSpace(eval.EstadoInterior) ||
                string.IsNullOrWhiteSpace(eval.EstadoDocumentacion))
            {
                throw new ApplicationException("Todos los estados son obligatorios.");
            }

            _mapper.Alta(eval);
        }

        public void Modificar(EvaluacionTecnica eval)
        {
            if (eval.ID <= 0)
                throw new ApplicationException("Evaluación inválida.");
            _mapper.Modificar(eval);
        }

        public void Eliminar(int id)
        {
            _mapper.Baja(id);
        }
    }
}
