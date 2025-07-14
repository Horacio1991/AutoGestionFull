using System;
using System.Collections.Generic;
using System.Linq;
using BE;
using Mapper;

namespace BLL
{
    public class BLLOfertaCompra
    {
        private readonly MPPOfertaCompra _mpp = new MPPOfertaCompra();

        /// <summary>
        /// Devuelve todas las ofertas activas.
        /// </summary>
        public List<OfertaCompra> ListarTodo()
        {
            return _mpp.ListarTodo();
        }

        /// <summary>
        /// Busca una oferta por su ID.
        /// </summary>
        public OfertaCompra BuscarPorId(int id)
        {
            return _mpp.ListarTodo().FirstOrDefault(o => o.ID == id);
        }

        /// <summary>
        /// Busca una oferta por dominio de vehículo.
        /// </summary>
        public OfertaCompra BuscarPorDominio(string dominio)
        {
            if (string.IsNullOrWhiteSpace(dominio))
                throw new ArgumentException("El dominio no puede estar vacío.", nameof(dominio));

            return _mpp.ListarTodo()
                       .FirstOrDefault(o =>
                           o.Vehiculo != null
                           && string.Equals(o.Vehiculo.Dominio, dominio, StringComparison.OrdinalIgnoreCase)
                       );
        }

        /// <summary>
        /// Registra una nueva oferta de compra.
        /// </summary>
        public void RegistrarOferta(OfertaCompra oferta)
        {
            if (oferta == null) throw new ArgumentNullException(nameof(oferta));
            if (oferta.Oferente == null) throw new ArgumentException("Debe informar el oferente.", nameof(oferta.Oferente));
            if (oferta.Vehiculo == null) throw new ArgumentException("Debe informar el vehículo.", nameof(oferta.Vehiculo));

            // Nuevo ID autogenerado
            var todas = _mpp.ListarTodo();
            oferta.ID = todas.Any() ? todas.Max(o => o.ID) + 1 : 1;
            oferta.Estado = oferta.Estado ?? "En evaluación";
            oferta.FechaInspeccion = oferta.FechaInspeccion == default
                ? DateTime.Now
                : oferta.FechaInspeccion;

            _mpp.Alta(oferta);
        }

        /// <summary>
        /// Cambia el estado de una oferta (p. ej. a "Aprobada" o "Rechazada").
        /// </summary>
        public void ModificarEstado(int ofertaId, string nuevoEstado)
        {
            var oferta = BuscarPorId(ofertaId)
                        ?? throw new ApplicationException("Oferta no encontrada.");
            oferta.Estado = nuevoEstado ?? throw new ArgumentNullException(nameof(nuevoEstado));
            _mpp.Modificar(oferta);
        }
    }
}
