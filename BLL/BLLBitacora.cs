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

        /// <summary>
        /// Devuelve todos los registros de la bitácora.
        /// </summary>
        public List<Bitacora> ObtenerRegistros()
        {
            return _mapper.ListarTodo();
        }

        /// <summary>
        /// Agrega un nuevo registro a la bitácora.
        /// </summary>
        public void RegistrarEvento(Bitacora registro)
        {
            _mapper.Alta(registro);
        }

        /// <summary>
        /// Devuelve los registros de la bitácora mapeados a DTOs.
        /// </summary>
        public List<BitacoraDto> ObtenerRegistrosDto()
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

    }
}
