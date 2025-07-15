using System;
using System.Collections.Generic;
using BE;
using Mapper;

namespace BLL
{
    public class BLLOfertaCompra
    {
        private readonly MPPOfertaCompra _mapper;

        public BLLOfertaCompra()
        {
            _mapper = new MPPOfertaCompra();
        }

        public List<OfertaCompra> ObtenerTodas()
        {
            return _mapper.ListarTodo();
        }

        public OfertaCompra ObtenerPorId(int id)
        {
            if (id <= 0) throw new ArgumentException("ID inválido.", nameof(id));
            return _mapper.BuscarPorId(id);
        }

        public List<OfertaCompra> ObtenerPorDominio(string dominio)
        {
            if (string.IsNullOrWhiteSpace(dominio))
                throw new ArgumentException("Dominio inválido.", nameof(dominio));
            return _mapper.BuscarPorDominio(dominio);
        }

        public void RegistrarOferta(OfertaCompra oferta)
        {
            if (oferta == null)
                throw new ArgumentNullException(nameof(oferta));
            if (oferta.Oferente == null || oferta.Oferente.ID <= 0)
                throw new ApplicationException("Oferente inválido.");
            if (oferta.Vehiculo == null || oferta.Vehiculo.ID <= 0)
                throw new ApplicationException("Vehículo inválido.");
            if (oferta.FechaInspeccion == default)
                throw new ApplicationException("Fecha de inspección inválida.");

            // Inicializa estado por defecto
            oferta.Estado = "En evaluación";

            _mapper.Alta(oferta);
        }
    }
}
