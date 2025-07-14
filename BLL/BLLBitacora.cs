using BE;
using Mapper;
using MAPPER;
using System;
using System.Collections.Generic;

namespace BLL
{
    public class BLLBitacora
    {
        private readonly MPPBitacora _mpp = new MPPBitacora();

        public List<Bitacora> ListarTodo()
        {
            return _mpp.ListarTodo();
        }

        public void Registrar(Bitacora bit)
        {
            if (bit == null) throw new ArgumentNullException(nameof(bit));

            bool ok = _mpp.GuardarRegistro(bit);
            if (!ok)
                throw new ApplicationException("No se pudo guardar el registro en la bitácora.");
        }
    }
}
