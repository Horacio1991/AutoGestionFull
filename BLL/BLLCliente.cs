using System;
using System.Collections.Generic;
using BE;
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
