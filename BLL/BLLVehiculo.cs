using BE;
using DTOs;
using Mapper;

namespace BLL
{
    public class BLLVehiculo
    {
        private readonly MPPVehiculo _mapper = new MPPVehiculo();

        public List<VehiculoDto> ObtenerVehiculosDisponiblesDto()
        {
            try
            {
                return _mapper.ListarDisponibles()
                              .Select(MapToDto)
                              .ToList();
            }
            catch (Exception)
            {
                return new List<VehiculoDto>();
            }
        }

        public List<VehiculoDto> BuscarPorModeloDto(string modelo)
        {
            try
            {
                return _mapper.ListarDisponibles()
                              .Where(v => v.Modelo.Equals(modelo, StringComparison.OrdinalIgnoreCase))
                              .Select(MapToDto)
                              .ToList();
            }
            catch (Exception)
            {
                return new List<VehiculoDto>();
            }
        }

        public List<VehiculoDto> BuscarPorMarcaDto(string marca)
        {
            try
            {
                return _mapper.ListarDisponibles()
                              .Where(v => v.Marca.Equals(marca, StringComparison.OrdinalIgnoreCase))
                              .Select(MapToDto)
                              .ToList();
            }
            catch (Exception)
            {
                return new List<VehiculoDto>();
            }
        }

        public Vehiculo BuscarPorId(int id)
        {
            try
            {
                return _mapper.BuscarPorId(id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool ActualizarEstadoStock(int vehiculoId, string nuevoEstado, out string error)
        {
            error = null;
            try
            {
                var veh = _mapper.BuscarPorId(vehiculoId)
                          ?? throw new ApplicationException("Vehículo no encontrado.");
                veh.Estado = nuevoEstado;
                _mapper.Actualizar(veh);
                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        public VehiculoDto ObtenerPorDominioDto(string dominio)
        {
            try
            {
                var beVeh = _mapper.ListarTodo()
                                   .FirstOrDefault(v =>
                                       v.Dominio.Equals(dominio, StringComparison.OrdinalIgnoreCase));
                return beVeh is null ? null : MapToDto(beVeh);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool RegistrarVehiculoDto(VehiculoDto dto, out VehiculoDto vehiculoCreado, out string error)
        {
            vehiculoCreado = null;
            error = null;
            try
            {
                if (dto == null)
                    throw new ArgumentNullException(nameof(dto));
                if (string.IsNullOrWhiteSpace(dto.Dominio))
                    throw new ApplicationException("El dominio del vehículo es obligatorio.");

                var be = new Vehiculo
                {
                    Marca = dto.Marca,
                    Modelo = dto.Modelo,
                    Año = dto.Año,
                    Color = dto.Color,
                    Km = dto.Km,
                    Dominio = dto.Dominio,
                    Estado = dto.Estado ?? "Disponible"
                };

                _mapper.Alta(be);

                // Leer nuevamente para capturar el ID generado
                var creado = _mapper.ListarTodo()
                                    .FirstOrDefault(v => v.Dominio == be.Dominio);
                if (creado == null)
                    throw new ApplicationException("No se pudo crear el vehículo correctamente.");

                vehiculoCreado = MapToDto(creado);
                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        private VehiculoDto MapToDto(Vehiculo v) => new VehiculoDto
        {
            ID = v.ID,
            Marca = v.Marca,
            Modelo = v.Modelo,
            Año = v.Año,
            Color = v.Color,
            Km = v.Km,
            Dominio = v.Dominio,
            Estado = v.Estado
        };
    }
}
