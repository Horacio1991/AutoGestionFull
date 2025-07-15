using BE;
using DTOs;
using Mapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    public class BLLEvaluacionTecnica
    {
        private readonly MPPOfertaCompra _mppOferta = new MPPOfertaCompra();
        private readonly MPPEvaluacionTecnica _mppEval = new MPPEvaluacionTecnica();
        private readonly MPPVehiculo _mppVehiculo = new MPPVehiculo();

        public List<EvaluacionTecnica> ObtenerTodas()
            => _mppEval.ListarTodo();

        public EvaluacionTecnica ObtenerPorId(int id)
            => _mppEval.BuscarPorId(id);

        public void Registrar(EvaluacionTecnica eval, int ofertaId)
        {
            if (string.IsNullOrWhiteSpace(eval.EstadoMotor) ||
                string.IsNullOrWhiteSpace(eval.EstadoCarroceria) ||
                string.IsNullOrWhiteSpace(eval.EstadoInterior) ||
                string.IsNullOrWhiteSpace(eval.EstadoDocumentacion))
            {
                throw new ApplicationException("Todos los estados son obligatorios.");
            }
            _mppEval.AltaEvaluacion(eval, ofertaId);
        }

        public void Modificar(EvaluacionTecnica eval)
        {
            if (eval.ID <= 0)
                throw new ApplicationException("Evaluación inválida.");
            _mppEval.Modificar(eval);
        }

        public void Eliminar(int id)
            => _mppEval.Baja(id);

        public List<OfertaListDto> ObtenerOfertasParaEvaluar()
        {
            return _mppOferta.ListarTodo()
                .Where(o => o.Estado.Equals("En evaluación", StringComparison.OrdinalIgnoreCase))
                .Select(o =>
                {
                    var veh = _mppVehiculo.BuscarPorId(o.Vehiculo.ID);
                    string resumen = veh != null
                        ? $"{veh.Marca} {veh.Modelo} ({veh.Dominio})"
                        : $"Vehículo #{o.Vehiculo.ID}";
                    return new OfertaListDto
                    {
                        ID = o.ID,
                        VehiculoResumen = resumen,
                        FechaInspeccion = o.FechaInspeccion
                    };
                })
                .ToList();
        }

        public void RegistrarEvaluacion(EvaluacionInputDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            var eval = new EvaluacionTecnica
            {
                EstadoMotor = dto.EstadoMotor,
                EstadoCarroceria = dto.EstadoCarroceria,
                EstadoInterior = dto.EstadoInterior,
                EstadoDocumentacion = dto.EstadoDocumentacion,
                Observaciones = dto.Observaciones
            };
            Registrar(eval, dto.OfertaID);
        }
    }
}
