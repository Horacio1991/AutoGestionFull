using BE;
using DTOs;
using Mapper;

namespace BLL
{
    public class BLLTurno
    {
        private readonly MPPTurno _mpp = new MPPTurno();

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

        public bool RegistrarTurno(TurnoInputDto input, out string error)
        {
            error = null;
            try
            {
                if (string.IsNullOrWhiteSpace(input.DniCliente))
                    throw new ApplicationException("DNI de cliente inválido.");
                if (string.IsNullOrWhiteSpace(input.DominioVehiculo))
                    throw new ApplicationException("Dominio de vehículo inválido.");

                var cliente = new BLLCliente()
                    .ObtenerPorDni(input.DniCliente)
                    ?? throw new ApplicationException("Cliente no encontrado.");

                var vehDto = new BLLVehiculo()
                    .ObtenerPorDominioDto(input.DominioVehiculo)
                    ?? throw new ApplicationException("Vehículo no encontrado.");

                var veh = new Vehiculo { ID = vehDto.ID };

                // --- NUEVO: Validar que NO haya un turno ya asignado para ese vehículo, día y hora ---
                var turnosExistentes = _mpp.ListarPendientesAsistencia();
                bool yaExiste = turnosExistentes.Any(t =>
                    t.Vehiculo.ID == veh.ID &&
                    t.Fecha.Date == input.Fecha.Date &&
                    t.Hora == input.Hora
                );
                if (yaExiste)
                {
                    error = "El vehículo ya tiene un turno asignado en esa fecha y hora. Seleccione otro horario.";
                    return false;
                }

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
