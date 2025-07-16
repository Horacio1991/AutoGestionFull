using BE;
using DTOs;
using Mapper;

namespace BLL
{
    public class BLLVehiculo
    {
        private readonly MPPVehiculo _mapper = new MPPVehiculo();

        /// <summary>
        /// Devuelve todos los vehículos con estado "Disponible" como DTOs.
        /// </summary>
        public List<VehiculoDto> ObtenerVehiculosDisponiblesDto()
        {
            return _mapper.ListarDisponibles()
                .Select(v => new VehiculoDto
                {
                    ID = v.ID,
                    Marca = v.Marca,
                    Modelo = v.Modelo,
                    Año = v.Año,
                    Color = v.Color,
                    Km = v.Km,
                    Dominio = v.Dominio,
                    Estado = v.Estado
                })
                .ToList();
        }

        /// <summary>
        /// Busca vehículos disponibles por modelo exacto y los devuelve como DTOs.
        /// </summary>
        public List<VehiculoDto> BuscarPorModeloDto(string modelo)
        {
            return _mapper.ListarDisponibles()
                .Where(v => v.Modelo.Equals(modelo, StringComparison.OrdinalIgnoreCase))
                .Select(v => new VehiculoDto
                {
                    ID = v.ID,
                    Marca = v.Marca,
                    Modelo = v.Modelo,
                    Año = v.Año,
                    Color = v.Color,
                    Km = v.Km,
                    Dominio = v.Dominio,
                    Estado = v.Estado
                })
                .ToList();
        }

        /// <summary>
        /// Busca vehículos disponibles por marca exacta y los devuelve como DTOs.
        /// </summary>
        public List<VehiculoDto> BuscarPorMarcaDto(string marca)
        {
            return _mapper.ListarDisponibles()
                .Where(v => v.Marca.Equals(marca, StringComparison.OrdinalIgnoreCase))
                .Select(v => new VehiculoDto
                {
                    ID = v.ID,
                    Marca = v.Marca,
                    Modelo = v.Modelo,
                    Año = v.Año,
                    Color = v.Color,
                    Km = v.Km,
                    Dominio = v.Dominio,
                    Estado = v.Estado
                })
                .ToList();
        }

        /// <summary>Traer vehículo completo por ID.</summary>
        public Vehiculo BuscarPorId(int id)
            => _mapper.BuscarPorId(id);

        public void ActualizarEstadoStock(int vehiculoId, string nuevoEstado)
        {
            var veh = _mapper.BuscarPorId(vehiculoId)
                      ?? throw new ApplicationException("Vehículo no encontrado.");
            veh.Estado = nuevoEstado;
            _mapper.Actualizar(veh);
        }

        // Devuelve un DTO buscándolo por dominio
        public Vehiculo ObtenerPorDominio(string dominio)
            => _mapper.BuscarPorDominio(dominio);
        // Registra un vehículo a partir de un DTO
        public VehiculoDto RegistrarVehiculo(VehiculoDto dto)
        {
            var be = new Vehiculo
            {
                Marca = dto.Marca,
                Modelo = dto.Modelo,
                Año = dto.Año,
                Color = dto.Color,
                Km = dto.Km,
                Dominio = dto.Dominio,
                Estado = dto.Estado
            };
            _mapper.Alta(be);
            var guardado = _mapper.BuscarPorId(be.ID);
            return new VehiculoDto
            {
                ID = guardado.ID,
                Marca = guardado.Marca,
                Modelo = guardado.Modelo,
                Año = guardado.Año,
                Color = guardado.Color,
                Km = guardado.Km,
                Dominio = guardado.Dominio,
                Estado = guardado.Estado
            };
        }
    }
}
