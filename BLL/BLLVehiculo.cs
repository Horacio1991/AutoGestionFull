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
        // 2) Registrar (dar de alta) un nuevo vehículo a partir de un DTO y devolver el DTO resultante
        public VehiculoDto RegistrarVehiculoDto(VehiculoDto dto)
        {
            // Mapeo DTO → entidad BE.Vehiculo
            var beVeh = new Vehiculo
            {
                Marca = dto.Marca,
                Modelo = dto.Modelo,
                Año = dto.Año,
                Color = dto.Color,
                Km = dto.Km,
                Dominio = dto.Dominio,
                Estado = dto.Estado ?? "Disponible"
            };

            // Darlo de alta en el XML
            _mapper.Actualizar(beVeh); // o si tienes un Alta() crea otro método en MPPVehiculo

            // Asumimos que la ID se generó, recargamos para obtenerla
            var creado = _mapper.ListarTodo()
                                .First(v => v.Dominio == beVeh.Dominio);
            // Y devolvemos el DTO con la ID
            return new VehiculoDto
            {
                ID = creado.ID,
                Marca = creado.Marca,
                Modelo = creado.Modelo,
                Año = creado.Año,
                Color = creado.Color,
                Km = creado.Km,
                Dominio = creado.Dominio,
                Estado = creado.Estado
            };
        }
        // 1) Buscar un VehiculoDto por dominio
        public VehiculoDto ObtenerPorDominioDto(string dominio)
        {
            var beVeh = _mapper.ListarTodo()
                               .FirstOrDefault(v =>
                                   v.Dominio.Equals(dominio, StringComparison.OrdinalIgnoreCase));
            if (beVeh == null) return null;
            return new VehiculoDto
            {
                ID = beVeh.ID,
                Marca = beVeh.Marca,
                Modelo = beVeh.Modelo,
                Año = beVeh.Año,
                Color = beVeh.Color,
                Km = beVeh.Km,
                Dominio = beVeh.Dominio,
                Estado = beVeh.Estado
            };
        }
    }
}
