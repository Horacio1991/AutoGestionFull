using DTOs;


namespace BLL
{
    public class BLLRegistrarDatos
    {
        private readonly BLLOfertaCompra _bllOferta = new BLLOfertaCompra();
        private readonly BLLEvaluacionTecnica _bllEval = new BLLEvaluacionTecnica();
        private readonly BLLVehiculo _bllVehiculo = new BLLVehiculo();

        public OfertaRegistroDto ObtenerOfertaPorDominio(string dominio)
        {
            try
            {
                var ofertas = _bllOferta.ObtenerPorDominio(dominio);
                var oferta = ofertas.FirstOrDefault();
                if (oferta == null) return null;

                // Busco evaluación por ID de la oferta
                var eval = _bllEval.ObtenerTodas()
                                   .FirstOrDefault(e => e.ID == oferta.ID);
                if (eval == null) return null;

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
            catch (Exception)
            {
                return null;
            }
        }

        public void RegistrarDatos(RegistrarDatosInputDto input)
        {
            if (input.OfertaID <= 0)
                throw new ApplicationException("Oferta inválida.");
            if (string.IsNullOrWhiteSpace(input.EstadoStock))
                throw new ApplicationException("Estado de stock obligatorio.");

            try
            {
                var oferta = _bllOferta.ObtenerPorId(input.OfertaID)
                            ?? throw new ApplicationException("Oferta no encontrada.");

                string error;
                if (!_bllVehiculo.ActualizarEstadoStock(oferta.Vehiculo.ID, input.EstadoStock, out error))
                    throw new ApplicationException(error);

            }
            catch (Exception ex)
            {
                throw new ApplicationException("No se pudo registrar los datos del vehículo. " + ex.Message, ex);
            }
        }
    }
}
