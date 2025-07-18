using BE;
using DTOs;
using Mapper;


namespace BLL
{
    public class BLLBitacora
    {
        private readonly MPPBitacora _mapper;

        public BLLBitacora()
        {
            _mapper = new MPPBitacora();
        }

        public void RegistrarEvento(Bitacora registro)
        {
            try
            {
                _mapper.Alta(registro);
            }
            catch (Exception ex)
            {

            }
        }

        public List<BitacoraDto> ObtenerRegistrosDto()
        {
            try
            {
                return _mapper.ListarTodo()
                              .Select(b => new BitacoraDto
                              {
                                  ID = b.ID,
                                  FechaRegistro = b.FechaRegistro,
                                  Detalle = b.Detalle,
                                  UsuarioNombre = b.UsuarioNombre
                              })
                              .ToList();
            }
            catch (Exception ex)
            {
                return new List<BitacoraDto>();
            }
        }
    }
}
