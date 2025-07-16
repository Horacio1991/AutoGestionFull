using BLL;
using DTOs;
using System.Linq;

namespace BLL
{
    public class BLLRegistrarDatos
    {
        private readonly BLLOfertaCompra _bllOferta = new BLLOfertaCompra();
        private readonly BLLEvaluacionTecnica _bllEval = new BLLEvaluacionTecnica();
        private readonly BLLVehiculo _bllVehiculo = new BLLVehiculo();

        /// <summary>
        /// Busca la oferta + su evaluación y arma el texto.
        /// </summary>
        public OfertaRegistroDto ObtenerOfertaPorDominio(string dominio)
        {
            // 1) Oferta
            var ofertas = _bllOferta.ObtenerPorDominio(dominio);
            var oferta = ofertas.FirstOrDefault();
            if (oferta == null) return null;

            // 2) Evaluación técnica de esa oferta
            var eval = _bllEval.ObtenerTodas()
                               .FirstOrDefault(e => e.OfertaID == oferta.ID);
            if (eval == null) return null;

            // 3) Construir el texto
            var texto = $"Motor: {eval.EstadoMotor}\r\n" +
                        $"Carrocería: {eval.EstadoCarroceria}\r\n" +
                        $"Interior: {eval.EstadoInterior}\r\n" +
                        $"Documentación: {eval.EstadoDocumentacion}\r\n" +
                        $"Observaciones: {eval.Observaciones}";

            return new OfertaRegistroDto
            {
                OfertaID = oferta.ID,
                EvaluacionTexto = texto
            };
        }

        /// <summary>
        /// Registra el estado de stock del vehículo asociado a la oferta.
        /// </summary>
        public void RegistrarDatos(RegistrarDatosInputDto input)
        {
            if (input.OfertaID <= 0)
                throw new ApplicationException("Oferta inválida.");
            if (string.IsNullOrWhiteSpace(input.EstadoStock))
                throw new ApplicationException("Estado de stock obligatorio.");

            // 1) Obtener oferta
            var oferta = _bllOferta.ObtenerPorId(input.OfertaID)
                        ?? throw new ApplicationException("Oferta no encontrada.");

            // 2) Actualizar estado en Vehiculo
            _bllVehiculo.ActualizarEstadoStock(oferta.Vehiculo.ID, input.EstadoStock);
        }
    }
}
