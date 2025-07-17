using BE;
using DTOs;
using Mapper;


namespace BLL
{
    public class BLLRegistrarTurno
    {
        private readonly BLLCliente _bllCliente = new BLLCliente();
        private readonly BLLVehiculo _bllVehiculo = new BLLVehiculo();
        private readonly MPPTurno _mppTurno = new MPPTurno();

        public void RegistrarTurno(TurnoInputDto input)
        {
            // 1) Cliente: obligar que exista
            var clienteDto = _bllCliente.ObtenerPorDni(input.DniCliente)
                             ?? throw new ApplicationException("Cliente no existe.");

            // 2) Vehículo: buscar por dominio en DTOs
            var vehDto = _bllVehiculo.ObtenerPorDominioDto(input.DominioVehiculo)
                         ?? throw new ApplicationException("Vehículo no encontrado.");

            // 3) Construir el BE.Turno con sólo los IDs
            var turno = new Turno
            {
                Cliente = new Cliente { ID = clienteDto.ID },
                Vehiculo = new Vehiculo { ID = vehDto.ID },
                Fecha = input.Fecha,
                Hora = input.Hora,
                Asistencia = "Pendiente",
                Observaciones = input.Observaciones
            };

            // 4) Persistir en el XML
            _mppTurno.AgregarTurno(turno);
        }
    }
}
