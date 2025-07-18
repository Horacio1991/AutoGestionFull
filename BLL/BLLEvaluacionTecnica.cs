using BE;
using DTOs;
using Mapper;

namespace BLL
{
    public class BLLEvaluacionTecnica
    {
        private readonly MPPOfertaCompra _mppOferta = new MPPOfertaCompra();
        private readonly MPPEvaluacionTecnica _mppEval = new MPPEvaluacionTecnica();
        private readonly MPPVehiculo _mppVehiculo = new MPPVehiculo();

        public List<EvaluacionTecnica> ObtenerTodas()
        {
            try
            {
                return _mppEval.ListarTodo();
            }
            catch (Exception)
            {
                return new List<EvaluacionTecnica>();
            }
        }

        // Registrar evaluación técnica para una oferta
        public void Registrar(EvaluacionTecnica eval, int ofertaId)
        {
            if (string.IsNullOrWhiteSpace(eval.EstadoMotor) ||
                string.IsNullOrWhiteSpace(eval.EstadoCarroceria) ||
                string.IsNullOrWhiteSpace(eval.EstadoInterior) ||
                string.IsNullOrWhiteSpace(eval.EstadoDocumentacion))
            {
                throw new ApplicationException("Todos los estados son obligatorios.");
            }
            try
            {
                _mppEval.AltaEvaluacion(eval, ofertaId);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("No se pudo registrar la evaluación técnica. " + ex.Message, ex);
            }
        }

        // Ofertas con estado "En evaluación" para mostrar en combo
        public List<OfertaListDto> ObtenerOfertasParaEvaluar()
        {
            try
            {
                var todas = _mppOferta.ListarTodo();

                return todas
                    .Where(o => o.Estado.Equals("En evaluación", StringComparison.OrdinalIgnoreCase))
                    .Select(o =>
                    {
                        var veh = _mppVehiculo.BuscarPorId(o.Vehiculo.ID)
                                  ?? throw new ApplicationException($"Vehículo #{o.Vehiculo.ID} no encontrado");
                        return new OfertaListDto
                        {
                            ID = o.ID,
                            FechaInspeccion = o.FechaInspeccion,
                            VehiculoResumen = $"{veh.Marca} {veh.Modelo} ({veh.Dominio})"
                        };
                    })
                    .ToList();
            }
            catch (Exception)
            {
                return new List<OfertaListDto>();
            }
        }

        public void RegistrarEvaluacion(EvaluacionInputDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));
            try
            {
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
            catch (Exception ex)
            {
                throw new ApplicationException("No se pudo registrar la evaluación técnica. " + ex.Message, ex);
            }
        }
    }
}
