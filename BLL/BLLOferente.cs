using System;
using System.Collections.Generic;
using System.Linq;
using BE;
using Mapper;

namespace BLL
{
    public class BLLOferente
    {
        private readonly MPPOferente _mpp = new MPPOferente();

        /// <summary>
        /// Devuelve todos los oferentes activos.
        /// </summary>
        public List<Oferente> ListarTodo()
        {
            return _mpp.ListarTodo();
        }

        /// <summary>
        /// Busca un oferente por su DNI.
        /// </summary>
        public Oferente BuscarPorDni(string dni)
        {
            if (string.IsNullOrWhiteSpace(dni))
                throw new ArgumentException("El DNI no puede estar vacío.", nameof(dni));

            return _mpp.BuscarPorDni(dni);
        }

        /// <summary>
        /// Busca un oferente por su ID.
        /// </summary>
        public Oferente BuscarPorId(int id)
        {
            return _mpp.ListarTodo().FirstOrDefault(o => o.ID == id);
        }

        /// <summary>
        /// Verifica si ya existe un oferente con ese DNI.
        /// </summary>
        public bool ExisteOferente(string dni)
        {
            return BuscarPorDni(dni) != null;
        }

        /// <summary>
        /// Da de alta un nuevo oferente.
        /// </summary>
        public void AltaOferente(Oferente oferente)
        {
            if (oferente == null) throw new ArgumentNullException(nameof(oferente));
            if (string.IsNullOrWhiteSpace(oferente.Dni))
                throw new ArgumentException("El DNI es obligatorio.", nameof(oferente.Dni));
            if (ExisteOferente(oferente.Dni))
                throw new ApplicationException("Ya existe un oferente con ese DNI.");

            // Asignar nuevo ID automáticamente
            var todos = _mpp.ListarTodo();
            oferente.ID = todos.Any() ? todos.Max(o => o.ID) + 1 : 1;

            _mpp.AltaOferente(oferente);
        }
    }
}
