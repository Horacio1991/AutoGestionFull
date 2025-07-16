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

        public List<Cliente> ObtenerTodos()
        {
            return _mapper.ListarTodo();
        }

        public Cliente ObtenerPorId(int id)
        {
            return _mapper.BuscarPorId(id);
        }

        public Cliente ObtenerPorDni(string dni)
        {
            return _mapper.BuscarPorDni(dni);
        }

        public void Registrar(Cliente cliente)
        {
            if (string.IsNullOrWhiteSpace(cliente.Dni))
                throw new ApplicationException("DNI es obligatorio.");
            if (_mapper.BuscarPorDni(cliente.Dni) != null)
                throw new ApplicationException("Ya existe un cliente con ese DNI.");

            _mapper.Alta(cliente);
        }

        public ClienteDto RegistrarCliente(ClienteInputDto input)
        {
            // 1) Validaciones
            if (string.IsNullOrWhiteSpace(input.Dni))
                throw new ApplicationException("DNI es obligatorio.");
            if (_mapper.BuscarPorDni(input.Dni) != null)
                throw new ApplicationException("Ya existe un cliente con ese DNI.");

            // 2) Mapeo DTO → BE
            var entidad = new Cliente
            {
                Dni = input.Dni,
                Nombre = input.Nombre,
                Apellido = input.Apellido,
                Contacto = input.Contacto
            };

            // 3) Registrar en XML
            _mapper.Alta(entidad);

            // 4) Recuperar la entidad con ID asignado
            var guardado = _mapper.BuscarPorDni(entidad.Dni);

            // 5) Mapear BE → DTO de salida
            return new ClienteDto
            {
                ID = guardado.ID,
                Dni = guardado.Dni,
                Nombre = guardado.Nombre,
                Apellido = guardado.Apellido,
                Contacto = guardado.Contacto
            };
        }

        public void Modificar(Cliente cliente)
        {
            if (cliente.ID <= 0)
                throw new ApplicationException("Cliente inválido.");
            _mapper.Modificar(cliente);
        }

        public void Eliminar(int id)
        {
            _mapper.Baja(id);
        }

        public bool Existe(string dni)
        {
            return _mapper.BuscarPorDni(dni) != null;
        }
    }
}
