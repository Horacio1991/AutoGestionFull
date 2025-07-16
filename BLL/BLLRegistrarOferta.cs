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
            // 1) Manejo del oferente (igual que antes)
            var oferente = _bllOferente.ObtenerOferenteDtoPorDni(input.Oferente.Dni)
                          ?? _bllOferente.RegistrarOferenteDto(input.Oferente);

            // 2) Manejo del vehículo
            //    Buscamos por dominio (campo único) y si no existe, lo damos de alta
            var vehiculoExistente = _bllVehiculo.ObtenerPorDominioDto(input.Vehiculo.Dominio);
            VehiculoDto vehDto;
            if (vehiculoExistente == null)
            {
                vehDto = _bllVehiculo.RegistrarVehiculoDto(input.Vehiculo);
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
                Estado = "Pendiente"
            };

            // 4) Persistencia de la oferta
            _bllOferta.RegistrarOferta(oferta);
            return true;
        }

    }
}
