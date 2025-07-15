using System;
using System.Collections.Generic;
using BE;
using Mapper;

namespace BLL
{
    public class BLLTasacion
    {
        private readonly MPPTasacion _mapper;
        private readonly MPPOfertaCompra _mppOferta;

        public BLLTasacion()
        {
            _mapper = new MPPTasacion();
            _mppOferta = new MPPOfertaCompra();
        }

        /// <summary>
        /// Obtiene todas las tasaciones registradas.
        /// </summary>
        public List<Tasacion> ObtenerTodas()
        {
            return _mapper.ListarTodo();
        }

        /// <summary>
        /// Registra una nueva tasación y actualiza el estado de la oferta.
        /// </summary>
        public void RegistrarTasacion(Tasacion tasacion)
        {
            if (tasacion == null)
                throw new ArgumentNullException(nameof(tasacion));
            if (tasacion.Oferta == null || tasacion.Oferta.ID <= 0)
                throw new ApplicationException("Oferta inválida para tasación.");
            if (tasacion.ValorFinal <= 0)
                throw new ApplicationException("Valor final de tasación debe ser mayor a cero.");

            // 1) Alta de la tasación
            _mapper.Alta(tasacion);

            // 2) Marcar oferta como tasada (cambia estado en el XML de ofertas)
            _mppOferta.MarcarTasada(tasacion.Oferta.ID);
        }
    }
}
