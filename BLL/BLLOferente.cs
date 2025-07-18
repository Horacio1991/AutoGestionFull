using BE;
using DTOs;
using Mapper;

namespace BLL
{
    public class BLLOferente
    {
        private readonly MPPOferente _mapper;

        public BLLOferente()
        {
            _mapper = new MPPOferente();
        }

       public Oferente ObtenerPorDni(string dni)
        {
            try
            {
                return _mapper.BuscarPorDni(dni);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public OferenteDto ObtenerOferenteDtoPorDni(string dni)
        {
            try
            {
                var be = _mapper.BuscarPorDni(dni);
                if (be == null) return null;
                return new OferenteDto
                {
                    ID = be.ID,
                    Dni = be.Dni,
                    Nombre = be.Nombre,
                    Apellido = be.Apellido,
                    Contacto = be.Contacto
                };
            }
            catch (Exception)
            {
                return null;
            }
        }

        public OferenteDto RegistrarOferenteDto(OferenteDto dto)
        {
            try
            {
                if (_mapper.Existe(dto.Dni))
                    throw new ApplicationException("Ya existe un oferente con ese DNI.");

                var be = new Oferente
                {
                    Dni = dto.Dni,
                    Nombre = dto.Nombre,
                    Apellido = dto.Apellido,
                    Contacto = dto.Contacto
                };
                _mapper.Alta(be);

                // Recuperar para obtener ID
                var guardado = _mapper.BuscarPorDni(dto.Dni);
                return new OferenteDto
                {
                    ID = guardado.ID,
                    Dni = guardado.Dni,
                    Nombre = guardado.Nombre,
                    Apellido = guardado.Apellido,
                    Contacto = guardado.Contacto
                };
            }
            catch (Exception ex)
            {
                throw new ApplicationException("No se pudo registrar el oferente. " + ex.Message, ex);
            }
        }

    }
}
