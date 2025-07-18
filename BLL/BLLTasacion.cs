using BE;
using DTOs;
using Mapper;

namespace BLL
{
    public class BLLTasacion
    {
        private readonly MPPOfertaCompra _mppOferta = new MPPOfertaCompra();
        private readonly MPPEvaluacionTecnica _mppEval = new MPPEvaluacionTecnica();
        private readonly MPPTasacion _mppTasacion = new MPPTasacion();
        private readonly MPPVehiculo _vehiculoMapper = new MPPVehiculo();

        // traer ofertas pendientes de tasación (En estado "En evaluación")
        public List<OfertaParaTasacionDto> ObtenerOfertasParaTasacion()
        {
            var resultado = new List<OfertaParaTasacionDto>();
            try
            {
                var ofertas = _mppOferta.ListarTodo()
                    .Where(o => o.Estado.Equals("En evaluación", StringComparison.OrdinalIgnoreCase))
                    .ToList();

                foreach (var oferta in ofertas)
                {
                    // Vehículo completo
                    var veh = _vehiculoMapper.BuscarPorId(oferta.Vehiculo.ID);
                    if (veh == null) continue;

                    // Histórico de tasaciones previas de ese modelo
                    // se usa para calcular el rango de tasación automático
                    var historico = _mppTasacion.ListarTodo()
                        .Where(t => t.Oferta != null
                                 && t.Oferta.Vehiculo != null
                                 && t.Oferta.Vehiculo.Marca == veh.Marca
                                 && t.Oferta.Vehiculo.Modelo == veh.Modelo)
                        .Select(t => t.ValorFinal)
                        .ToList();

                    decimal? rangoMin = historico.Any() ? historico.Min() : (decimal?)null;
                    decimal? rangoMax = historico.Any() ? historico.Max() : (decimal?)null;

                    // Cargar evaluación técnica asociada
                    var eval = _mppEval.BuscarPorOferta(oferta.ID);
                    if (eval == null) continue;

                    resultado.Add(new OfertaParaTasacionDto
                    {
                        OfertaID = oferta.ID,
                        VehiculoResumen = $"{veh.Marca} {veh.Modelo} ({veh.Dominio})",
                        FechaInspeccion = oferta.FechaInspeccion,
                        EstadoMotor = eval.EstadoMotor,
                        EstadoCarroceria = eval.EstadoCarroceria,
                        EstadoInterior = eval.EstadoInterior,
                        EstadoDocumentacion = eval.EstadoDocumentacion,
                        RangoMin = rangoMin,
                        RangoMax = rangoMax
                    });
                }
            }
            catch (Exception)
            {
                // Devuelve lista vacía si hay error en algún acceso a datos
                return new List<OfertaParaTasacionDto>();
            }
            return resultado;
        }

        public void RegistrarTasacion(TasacionInputDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            try
            {
                // Recuperar oferta para tener su VehiculoId
                var oferta = _mppOferta.BuscarPorId(dto.OfertaID)
                            ?? throw new ApplicationException($"Oferta #{dto.OfertaID} no encontrada.");

                // Grabar la nueva tasación
                var t = new Tasacion
                {
                    Oferta = new OfertaCompra { ID = dto.OfertaID },
                    ValorFinal = dto.ValorFinal,
                    EstadoStock = dto.EstadoStock
                };
                _mppTasacion.AltaTasacion(t);

                // Actualizar el stock del vehículo
                var veh = _vehiculoMapper.BuscarPorId(oferta.Vehiculo.ID)
                          ?? throw new ApplicationException($"Vehículo #{oferta.Vehiculo.ID} no encontrado al actualizar stock.");
                veh.Estado = dto.EstadoStock;
                _vehiculoMapper.Actualizar(veh);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("No se pudo registrar la tasación. " + ex.Message, ex);
            }
        }
    }
}
