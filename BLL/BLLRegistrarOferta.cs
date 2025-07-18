using BE;
using DTOs;

namespace BLL
{
    public class BLLRegistrarOferta
    {
        private readonly BLLOferente _bllOferente = new BLLOferente();
        private readonly BLLVehiculo _bllVehiculo = new BLLVehiculo();
        private readonly BLLOfertaCompra _bllOferta = new BLLOfertaCompra();

        // Registra una oferta de compra a partir de los datos de entrada.
        // Devuelve true si todo fue OK, false si hubo error (mensaje se maneja arriba).
        public bool RegistrarOferta(OfertaInputDto input, out string error)
        {
            error = null;
            try
            {
                // 1) Manejo del oferente (buscar por DNI o registrar si no existe)
                var oferente = _bllOferente.ObtenerOferenteDtoPorDni(input.Oferente.Dni)
                              ?? _bllOferente.RegistrarOferenteDto(input.Oferente);

                // 2) Manejo del vehículo (buscar por dominio o registrar si no existe)
                var vehiculoExistente = _bllVehiculo.ObtenerPorDominioDto(input.Vehiculo.Dominio);
                VehiculoDto vehDto;
                if (vehiculoExistente == null)
                {
                    if (!_bllVehiculo.RegistrarVehiculoDto(input.Vehiculo, out vehDto, out error))
                        throw new ApplicationException(error);
                }
                else
                {
                    vehDto = vehiculoExistente;
                }

                // 3) Construcción de la entidad OfertaCompra
                var oferta = new OfertaCompra
                {
                    Oferente = new BE.Oferente { ID = oferente.ID },
                    Vehiculo = new BE.Vehiculo { ID = vehDto.ID },
                    FechaInspeccion = input.FechaInspeccion,
                    Estado = "En evaluación"
                };

                // 4) Persistencia de la oferta
                _bllOferta.RegistrarOferta(oferta);

                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }
    }
}
