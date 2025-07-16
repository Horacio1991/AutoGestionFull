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

        public List<Oferente> ObtenerTodos()
        {
            return _mapper.ListarTodo();
        }

        public Oferente ObtenerPorId(int id)
        {
            return _mapper.BuscarPorId(id);
        }

        public Oferente ObtenerPorDni(string dni)
        {
            if (string.IsNullOrWhiteSpace(dni))
                throw new ArgumentException("DNI inválido.", nameof(dni));

            return _mapper.BuscarPorDni(dni);
        }

        public bool ExisteOferente(string dni)
        {
            return _mapper.Existe(dni);
        }

        // Devuelve un DTO si existe
        public OferenteDto ObtenerOferenteDtoPorDni(string dni)
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

        // Registra y retorna un DTO
        public OferenteDto RegistrarOferenteDto(OferenteDto dto)
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

        public void ModificarOferente(Oferente oferente)
        {
            if (oferente == null || oferente.ID <= 0)
                throw new ApplicationException("Oferente inválido.");
            _mapper.Modificar(oferente);
        }

        public void EliminarOferente(int id)
        {
            if (id <= 0)
                throw new ApplicationException("ID de oferente inválido.");
            _mapper.Baja(id);
        }
    }
}
