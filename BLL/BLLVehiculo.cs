using BE;
using Mapper;

namespace BLL
{
    public class BLLVehiculo
    {
        private readonly MPPVehiculo _mapper = new MPPVehiculo();

        /// <summary>Vehículos con estado "Disponible".</summary>
        public List<Vehiculo> ObtenerDisponibles()
            => _mapper.ListarDisponibles();

        /// <summary>Buscar por modelo exacto entre disponibles.</summary>
        public List<Vehiculo> BuscarPorModelo(string modelo)
            => _mapper.BuscarPorModelo(modelo);

        /// <summary>Buscar por marca exacta entre disponibles.</summary>
        public List<Vehiculo> BuscarPorMarca(string marca)
            => _mapper.BuscarPorMarca(marca);

        /// <summary>Traer vehículo completo por ID.</summary>
        public Vehiculo BuscarPorId(int id)
            => _mapper.BuscarPorId(id);
    }
}
