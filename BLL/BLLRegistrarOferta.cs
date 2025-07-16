using BE;
using DTOs;

namespace BLL
{
    public class BLLRegistrarOferta
    {
        private readonly BLLOferente _bllOferente = new BLLOferente();
        private readonly BLLVehiculo _bllVehiculo = new BLLVehiculo();
        private readonly BLLOfertaCompra _bllOferta = new BLLOfertaCompra();

        public bool RegistrarOferta(OfertaInputDto input)
        {
            // 1) Asegurar Oferente
            var oferDto = _bllOferente.ObtenerOferenteDtoPorDni(input.Oferente.Dni)
                          ?? _bllOferente.RegistrarOferenteDto(input.Oferente);

            // 1) Intento recuperar el vehículo existente (como DTO)
            var vehDto = _bllVehiculo.ObtenerPorDominioDto(input.Vehiculo.Dominio)
                         // 2) Si no existe, lo doy de alta y obtengo el DTO
                         ?? _bllVehiculo.RegistrarVehiculoDto(input.Vehiculo);

            // 3) Crear BE.OfertaCompra
            var be = new OfertaCompra
            {
                Oferente = new Oferente { ID = oferDto.ID },
                Vehiculo = new Vehiculo { ID = vehDto.ID },
                FechaInspeccion = input.FechaInspeccion,
                Estado = "En evaluación"
            };

            _bllOferta.RegistrarOferta(be);
            return true;
        }
    }
}
