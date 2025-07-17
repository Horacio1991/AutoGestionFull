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

            // 1) Obtener BE.Cliente
            var cliente = new BLLCliente()
                .ObtenerPorDni(input.DniCliente)
                ?? throw new ApplicationException("Cliente no encontrado.");

            // 2) Obtener BE.Vehiculo
            var vehiculo = ObtenerPorDominio(input.DominioVehiculo)
                ?? throw new ApplicationException("Vehículo no encontrado.");

            // 3) Crear BE.Turno y persistir
            var turno = new Turno
            {
                Cliente = cliente,
                Vehiculo = vehiculo,
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
