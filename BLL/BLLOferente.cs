using System;
using System.Collections.Generic;
using BE;
using Mapper;

namespace BLL
{
    public class BLLOferente
    {
        private readonly MPPOferente _mapper;

        public BLLOferente()
        {
            _mapper = new MPPOferente();
        }

        public List<Oferente> ObtenerTodos()
        {
            return _mapper.ListarTodo();
        }

        public Oferente ObtenerPorId(int id)
        {
            return _mapper.BuscarPorId(id);
        }

        public Oferente ObtenerPorDni(string dni)
        {
            if (string.IsNullOrWhiteSpace(dni))
                throw new ArgumentException("DNI inválido.", nameof(dni));

            return _mapper.BuscarPorDni(dni);
        }

        public bool ExisteOferente(string dni)
        {
            return _mapper.Existe(dni);
        }

        public void RegistrarOferente(Oferente oferente)
        {
            if (oferente == null)
                throw new ArgumentNullException(nameof(oferente));
            if (string.IsNullOrWhiteSpace(oferente.Dni))
                throw new ApplicationException("DNI es obligatorio.");
            if (string.IsNullOrWhiteSpace(oferente.Nombre) ||
                string.IsNullOrWhiteSpace(oferente.Apellido) ||
                string.IsNullOrWhiteSpace(oferente.Contacto))
            {
                throw new ApplicationException("Nombre, apellido y contacto son obligatorios.");
            }
            if (ExisteOferente(oferente.Dni))
                throw new ApplicationException("Ya existe un oferente con ese DNI.");

            _mapper.Alta(oferente);
        }

        public void ModificarOferente(Oferente oferente)
        {
            if (oferente == null || oferente.ID <= 0)
                throw new ApplicationException("Oferente inválido.");
            _mapper.Modificar(oferente);
        }

        public void EliminarOferente(int id)
        {
            if (id <= 0)
                throw new ApplicationException("ID de oferente inválido.");
            _mapper.Baja(id);
        }
    }
}
