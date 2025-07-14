using System;
using System.Collections.Generic;
using System.Linq;
using BE;
using Mapper;

namespace BLL
{
    public class BLLComision
    {
        private readonly MPPComision _mpp = new MPPComision();

        /// <summary>
        /// Recupera todas las comisiones activas.
        /// </summary>
        public List<Comision> ListarTodo()
        {
            return _mpp.ListarTodo();
        }

        /// <summary>
        /// Recupera una comisión por su ID.
        /// </summary>
        public Comision BuscarPorId(int id)
        {
            return _mpp.BuscarPorId(id);
        }

        /// <summary>
        /// Registra una nueva comisión.
        /// </summary>
        public void AltaComision(Comision comision)
        {
            if (comision == null) throw new ArgumentNullException(nameof(comision));

            // Asignar ID secuencial
            var todas = _mpp.ListarTodo();
            comision.ID = todas.Any()
                ? todas.Max(c => c.ID) + 1
                : 1;

            _mpp.AltaComision(comision);
        }

        /// <summary>
        /// Modifica una comisión existente.
        /// </summary>
        public void ModificarComision(Comision comision)
        {
            if (comision == null) throw new ArgumentNullException(nameof(comision));

            var lista = _mpp.ListarTodo();
            var idx = lista.FindIndex(c => c.ID == comision.ID);
            if (idx < 0)
                throw new InvalidOperationException($"No se encontró la comisión con ID {comision.ID}");

            lista[idx] = comision;
            _mpp.GuardarLista(lista);
        }

        /// <summary>
        /// Elimina (desactiva) una comisión.
        /// </summary>
        public void BajaComision(int id)
        {
            var lista = _mpp.ListarTodo();
            var com = lista.FirstOrDefault(c => c.ID == id);
            if (com == null)
                throw new InvalidOperationException($"No se encontró la comisión con ID {id}");

            // Marcamos como inactiva en el XML (o simplemente la removemos de la lista activa)
            lista.Remove(com);
            _mpp.GuardarLista(lista);
        }
    }
}
