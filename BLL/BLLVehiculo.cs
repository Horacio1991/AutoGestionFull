// BLL/BLLVehiculo.cs
using System;
using System.Collections.Generic;
using System.Linq;
using BE;
using DTOs;
using Mapper;

namespace BLL
{
    public class BLLVehiculo
    {
        private readonly MPPVehiculo _mapper = new MPPVehiculo();

        /// <summary>Vehículos disponibles como DTOs.</summary>
        public List<VehiculoDto> ObtenerVehiculosDisponiblesDto()
            => _mapper.ListarDisponibles()
                      .Select(MapToDto)
                      .ToList();

        /// <summary>Buscar disponibles por modelo exacto.</summary>
        public List<VehiculoDto> BuscarPorModeloDto(string modelo)
            => _mapper.ListarDisponibles()
                      .Where(v => v.Modelo.Equals(modelo, StringComparison.OrdinalIgnoreCase))
                      .Select(MapToDto)
                      .ToList();

        /// <summary>Buscar disponibles por marca exacta.</summary>
        public List<VehiculoDto> BuscarPorMarcaDto(string marca)
            => _mapper.ListarDisponibles()
                      .Where(v => v.Marca.Equals(marca, StringComparison.OrdinalIgnoreCase))
                      .Select(MapToDto)
                      .ToList();

        /// <summary>Traer vehículo completo BE por ID.</summary>
        public Vehiculo BuscarPorId(int id)
            => _mapper.BuscarPorId(id);

        /// <summary>Actualizar solo el estado de stock.</summary>
        public void ActualizarEstadoStock(int vehiculoId, string nuevoEstado)
        {
            var veh = _mapper.BuscarPorId(vehiculoId)
                      ?? throw new ApplicationException("Vehículo no encontrado.");
            veh.Estado = nuevoEstado;
            _mapper.Actualizar(veh);
        }

        /// <summary>Buscar un DTO por dominio.</summary>
        public VehiculoDto ObtenerPorDominioDto(string dominio)
        {
            var beVeh = _mapper.ListarTodo()
                               .FirstOrDefault(v =>
                                   v.Dominio.Equals(dominio, StringComparison.OrdinalIgnoreCase));
            return beVeh is null ? null : MapToDto(beVeh);
        }

        /// <summary>Registrar un vehículo desde DTO y devolver el DTO con ID.</summary>
        public VehiculoDto RegistrarVehiculoDto(VehiculoDto dto)
        {
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
                                .First(v => v.Dominio == be.Dominio);

            return MapToDto(creado);
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
