using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoGestion.BE;
using AutoGestion.DAO;

namespace AutoGestion.BLL
{
    public class ClienteBLL
    {
        private readonly XmlRepository<Cliente> _repo;

        public ClienteBLL()
        {
            _repo = new XmlRepository<Cliente>("clientes.xml");
        }

        public Cliente BuscarClientePorDNI(string dni)
        {
            return _repo.ObtenerTodos().FirstOrDefault(c => c.Dni == dni);
        }

        public Cliente RegistrarCliente(Cliente cliente)
        {
            cliente.FechaRegistro = DateTime.Now;
            _repo.Agregar(cliente);
            return cliente;
        }
    }
}
