using BE;
using DTOs;
using Mapper;


namespace BLL
{
    public class BLLTasacion
    {
        private readonly MPPTasacion _mppTas = new MPPTasacion();
        private readonly MPPOfertaCompra _mppOferta = new MPPOfertaCompra();
        private readonly MPPVehiculo _mppVeh = new MPPVehiculo();

        /// <summary>
        /// Lista las ofertas en estado "En evaluación" junto con su evaluación y rango (si existe).
        /// </summary>
        public List<OfertaParaTasacionDto> ObtenerOfertasParaTasacion()
        {
            // 1) Obtengo todas las ofertas pendientes de tasar
            var ofertas = _mppOferta.ListarTodo()
                .Where(o => o.Estado.Equals("En evaluación", StringComparison.OrdinalIgnoreCase))
                .ToList();

            // 2) Cargo todas las evaluaciones técnicas
            var evaluaciones = new MPPEvaluacionTecnica().ListarTodo();

            // 3) Mapear a DTO
            var lista = new List<OfertaParaTasacionDto>();
            foreach (var o in ofertas)
            {
                var veh = _mppVeh.BuscarPorId(o.Vehiculo.ID);
                var resumen = veh != null
                    ? $"{veh.Marca} {veh.Modelo} ({veh.Dominio})"
                    : $"Vehículo #{o.Vehiculo.ID}";

                var eval = evaluaciones.FirstOrDefault(e => e.OfertaID == o.ID);
                lista.Add(new OfertaParaTasacionDto
                {
                    OfertaID = o.ID,
                    VehiculoResumen = resumen,
                    EstadoMotor = eval?.EstadoMotor ?? string.Empty,
                    EstadoCarroceria = eval?.EstadoCarroceria ?? string.Empty,
                    EstadoInterior = eval?.EstadoInterior ?? string.Empty,
                    EstadoDocumentacion = eval?.EstadoDocumentacion ?? string.Empty,
                    RangoMin = null,  // Si tienes lógica de rango, ponla aquí
                    RangoMax = null
                });
            }
            return lista;
        }

        /// <summary>
        /// Registra la tasación (valor final + cambio de estado) y actualiza stock del vehículo.
        /// </summary>
        public void RegistrarTasacion(TasacionInputDto input)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));
            if (input.OfertaID <= 0) throw new ApplicationException("Oferta inválida.");
            if (input.ValorFinal <= 0) throw new ApplicationException("Valor final inválido.");
            if (string.IsNullOrWhiteSpace(input.EstadoStock))
                throw new ApplicationException("Estado de stock obligatorio.");

            // 1) Registro de la Tasación
            var tas = new Tasacion
            {
                Oferta = new OfertaCompra { ID = input.OfertaID },
                ValorFinal = input.ValorFinal
            };
            _mppTas.Alta(tas);

            // 2) Marco la oferta como "Tasada"
            _mppOferta.MarcarTasada(input.OfertaID);

            // 3) Actualizo el estado del vehículo
            var oferta = new BLLOfertaCompra().ObtenerPorId(input.OfertaID)
                        ?? throw new ApplicationException("Oferta no encontrada.");
            new BLLVehiculo().ActualizarEstadoStock(oferta.Vehiculo.ID, input.EstadoStock);
        }
    }
}
