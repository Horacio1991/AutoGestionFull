using BE;
using Mapper;

namespace BLL
{
    public class BLLCliente
    {
        private readonly MPPCliente _mpp = new MPPCliente();

        public List<Cliente> ListarTodo()
        {
            return _mpp.ListarTodo();
        }

        public Cliente BuscarPorId(int id)
        {
            return _mpp.BuscarPorId(id);
        }

        public Cliente BuscarPorDni(string dni)
        {
            if (string.IsNullOrWhiteSpace(dni))
                throw new ArgumentException("DNI inválido.", nameof(dni));

            return _mpp.BuscarPorDni(dni);
        }

        public bool ExisteCliente(string dni)
        {
            return _mpp.ExisteCliente(dni);
        }

        public void AltaCliente(Cliente cliente)
        {
            if (cliente == null)
                throw new ArgumentNullException(nameof(cliente));

            _mpp.AltaCliente(cliente);
        }

        public void ModificarCliente(Cliente cliente)
        {
            if (cliente == null)
                throw new ArgumentNullException(nameof(cliente));

            _mpp.ModificarCliente(cliente);
        }

        public void BajaCliente(int id)
        {
            _mpp.BajaCliente(id);
        }
    }
}
