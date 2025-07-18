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

        // Busca una oferta por ID
        public OfertaCompra ObtenerPorId(int id)
        {
            if (id <= 0) throw new ArgumentException("ID inválido.", nameof(id));
            try
            {
                return _mapper.BuscarPorId(id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        // Busca ofertas por dominio del vehículo
        public List<OfertaCompra> ObtenerPorDominio(string dominio)
        {
            if (string.IsNullOrWhiteSpace(dominio))
                throw new ArgumentException("Dominio inválido.", nameof(dominio));
            try
            {
                return _mapper.BuscarPorDominio(dominio);
            }
            catch (Exception)
            {
                return new List<OfertaCompra>();
            }
        }

        // Registra una nueva oferta de compra
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

            oferta.Estado = "En evaluación";
            try
            {
                _mapper.Alta(oferta);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("No se pudo registrar la oferta de compra. " + ex.Message, ex);
            }
        }
    }
}
