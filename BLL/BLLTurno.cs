using BE;
using DTOs;
using Mapper;

namespace BLL
{
    public class BLLTurno
    {
        private readonly MPPTurno _mpp = new MPPTurno();

        // Devuelve la lista de turnos pendientes de asistencia.
        public List<Turno> ObtenerTurnosParaAsistencia()
        {
            try
            {
                return _mpp.ListarPendientesAsistencia();
            }
            catch (Exception)
            {
                return new List<Turno>();
            }
        }

        // Registra la asistencia o inasistencia de un turno.
        public bool RegistrarAsistencia(int turnoId, string estado, string observaciones, out string error)
        {
            error = null;
            try
            {
                _mpp.RegistrarAsistencia(turnoId, estado, observaciones);
                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        // Registra un nuevo turno a partir de un DTO de entrada.
        public bool RegistrarTurno(TurnoInputDto input, out string error)
        {
            error = null;
            try
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
