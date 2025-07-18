using BE;
using DTOs;
using Mapper;


namespace BLL
{
    public class BLLCliente
    {
        private readonly MPPCliente _mapper;

        public BLLCliente()
        {
            _mapper = new MPPCliente();
        }

        // Busca un cliente por ID.
        public Cliente ObtenerPorId(int id)
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

        // Busca un cliente por DNI.
        public Cliente ObtenerPorDni(string dni)
        {
            try
            {
                return _mapper.BuscarPorDni(dni);
            }
            catch (Exception)
            {
                return null;
            }
        }

        // Registra un cliente a partir de un DTO de entrada y devuelve DTO de salida.
        public ClienteDto RegistrarCliente(ClienteInputDto input)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(input.Dni))
                    throw new ApplicationException("DNI es obligatorio.");
                if (_mapper.BuscarPorDni(input.Dni) != null)
                    throw new ApplicationException("Ya existe un cliente con ese DNI.");

                var entidad = new Cliente
                {
                    Dni = input.Dni,
                    Nombre = input.Nombre,
                    Apellido = input.Apellido,
                    Contacto = input.Contacto
                };

                _mapper.Alta(entidad);

                var guardado = _mapper.BuscarPorDni(entidad.Dni);

                return new ClienteDto
                {
                    ID = guardado.ID,
                    Dni = guardado.Dni,
                    Nombre = guardado.Nombre,
                    Apellido = guardado.Apellido,
                    Contacto = guardado.Contacto
                };
            }
            catch (Exception ex)
            {
                throw new ApplicationException("No se pudo registrar el cliente. " + ex.Message, ex);
            }
        }

    }
}
