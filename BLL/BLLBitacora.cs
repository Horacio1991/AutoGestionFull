using System.Collections.Generic;
using BE;
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
    }
}
