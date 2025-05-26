using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoGestion.BE;
using AutoGestion.BLL;

namespace AutoGestion.CTRL_Vista
{
    public class ClienteController
    {
        private readonly ClienteBLL _clienteBLL = new();

        public Cliente BuscarCliente(string dni)
        {
            try
            {
                return _clienteBLL.BuscarClientePorDNI(dni);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al buscar cliente", ex);
            }
        }

        public Cliente RegistrarCliente(string dni, string nombre, string apellido, string contacto)
        {
            try
            {
                var cliente = new Cliente
                {
                    Dni = dni,
                    Nombre = nombre,
                    Apellido = apellido,
                    Contacto = contacto
                };

                return _clienteBLL.RegistrarCliente(cliente);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al registrar cliente", ex);
            }
        }
    }
}
