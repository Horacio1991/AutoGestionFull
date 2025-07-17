using System;
using System.Collections.Generic;
using System.Linq;
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

        public List<OfertaParaTasacionDto> ObtenerOfertasParaTasacion()
        {
            // 1) Traer ofertas en evaluación
            var ofertas = _mppOferta.ListarTodo()
                .Where(o => o.Estado.Equals("En evaluación", StringComparison.OrdinalIgnoreCase))
                .ToList();

            var resultado = new List<OfertaParaTasacionDto>();

            foreach (var oferta in ofertas)
            {
                // 2) Vehículo completo
                var veh = _vehiculoMapper.BuscarPorId(oferta.Vehiculo.ID)
                          ?? throw new ApplicationException($"Vehículo con Id={oferta.Vehiculo.ID} no encontrado.");

                // 3) Histórico de tasaciones previas de ese modelo
                var historico = _mppTasacion.ListarTodo()
                    .Where(t => t.Oferta != null
                             && t.Oferta.Vehiculo != null
                             && t.Oferta.Vehiculo.Marca == veh.Marca
                             && t.Oferta.Vehiculo.Modelo == veh.Modelo)
                    .Select(t => t.ValorFinal)
                    .ToList();

                decimal? rangoMin = historico.Any() ? historico.Min() : (decimal?)null;
                decimal? rangoMax = historico.Any() ? historico.Max() : (decimal?)null;

                // 4) Cargar evaluación técnica asociada
                var eval = _mppEval.BuscarPorOferta(oferta.ID)
                           ?? throw new ApplicationException($"No hay evaluación técnica para la oferta {oferta.ID}.");

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

            return resultado;
        }

        public void RegistrarTasacion(TasacionInputDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            // 0) Recuperar oferta para tener su VehiculoId
            var oferta = _mppOferta.BuscarPorId(dto.OfertaID)
                        ?? throw new ApplicationException($"Oferta #{dto.OfertaID} no encontrada.");

            // 1) Grabar la nueva tasación
            var t = new Tasacion
            {
                Oferta = new OfertaCompra { ID = dto.OfertaID },
                ValorFinal = dto.ValorFinal,
                EstadoStock = dto.EstadoStock
            };
            _mppTasacion.AltaTasacion(t);

            // 2) Y ahora actualizar el stock del vehículo
            var veh = _vehiculoMapper.BuscarPorId(oferta.Vehiculo.ID)
                      ?? throw new ApplicationException($"Vehículo #{oferta.Vehiculo.ID} no encontrado al actualizar stock.");
            veh.Estado = dto.EstadoStock;
            _vehiculoMapper.Actualizar(veh);
        }

    }
}
