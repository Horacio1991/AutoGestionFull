using BE;
using DTOs;
using Mapper;

namespace BLL
{
    public class BLLTurno
    {
        private readonly MPPTurno _mpp = new MPPTurno();

        public List<Turno> ObtenerTurnosParaAsistencia()
            => _mpp.ListarPendientesAsistencia();

        public void RegistrarAsistencia(int turnoId, string estado, string observaciones)
            => _mpp.RegistrarAsistencia(turnoId, estado, observaciones);

        /// <summary>
        /// Registra un nuevo turno a partir de un DTO de entrada.
        /// </summary>
        public void RegistrarTurno(TurnoInputDto input)
        {
            if (string.IsNullOrWhiteSpace(input.DniCliente))
                throw new ApplicationException("DNI de cliente inválido.");
            if (string.IsNullOrWhiteSpace(input.DominioVehiculo))
                throw new ApplicationException("Dominio de vehículo inválido.");

            // 1) Cliente
            var cliente = new BLLCliente()
                .ObtenerPorDni(input.DniCliente)
                ?? throw new ApplicationException("Cliente no encontrado.");

            // 2) Vehículo (BE) mapeado desde DTO
            var vehDto = new BLLVehiculo()
                .ObtenerPorDominioDto(input.DominioVehiculo)
                ?? throw new ApplicationException("Vehículo no encontrado.");

            var veh = new Vehiculo { ID = vehDto.ID };

            // 3) Persistir turno
            var turno = new Turno
            {
                Cliente = cliente,
                Vehiculo = veh,
                Fecha = input.Fecha,
                Hora = input.Hora,
                Asistencia = "Pendiente",
                Observaciones = string.Empty
            };

            _mpp.AgregarTurno(turno);
        }
        // Lo usamos internamente para no volver a instanciar MPPVehiculo
        private Vehiculo ObtenerPorDominio(string dominio)
            => null;
    }
}
