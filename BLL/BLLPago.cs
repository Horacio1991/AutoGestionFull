using BE;
using DTOs;
using Mapper;

namespace BLL
{
    public class BLLPago
    {
        private readonly MPPVehiculo _mppVehiculo = new MPPVehiculo();
        private readonly MPPCliente _mppCliente = new MPPCliente();
        private readonly MPPPago _mppPago = new MPPPago();
        private readonly MPPVenta _mppVenta = new MPPVenta();

        public List<VehiculoDto> ObtenerVehiculosDisponibles()
        {
            try
            {
                return _mppVehiculo.ListarTodo()
                    .Where(v => v.Estado.Equals("Disponible", StringComparison.OrdinalIgnoreCase))
                    .Select(v => new VehiculoDto
                    {
                        ID = v.ID,
                        Marca = v.Marca,
                        Modelo = v.Modelo,
                        Año = v.Año,
                        Color = v.Color,
                        Km = v.Km,
                        Dominio = v.Dominio,
                        Estado = v.Estado
                    })
                    .ToList();
            }
            catch (Exception)
            {
                return new List<VehiculoDto>();
            }
        }

        public ClienteDto BuscarCliente(string dni)
        {
            try
            {
                var c = _mppCliente.BuscarPorDni(dni);
                if (c == null) return null;
                return new ClienteDto
                {
                    ID = c.ID,
                    Dni = c.Dni,
                    Nombre = c.Nombre,
                    Apellido = c.Apellido,
                    Contacto = c.Contacto
                };
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool RegistrarPagoYVenta(
            string clienteDni,
            string vehiculoDominio,
            string tipoPago,
            decimal monto,
            int cuotas,
            string detalles,
            int vendedorId,
            string vendedorNombre,
            out string error)
        {
            error = null;
            try
            {
                // Cliente
                var cliente = _mppCliente.BuscarPorDni(clienteDni)
                              ?? throw new ApplicationException("Cliente no existe.");

                // Vehículo
                var veh = _mppVehiculo.ListarTodo()
                          .FirstOrDefault(v => v.Dominio.Equals(vehiculoDominio, StringComparison.OrdinalIgnoreCase));
                if (veh == null) throw new ApplicationException("Vehículo no disponible.");

                // Crear Pago
                var pago = new Pago
                {
                    TipoPago = tipoPago,
                    Monto = monto,
                    Cuotas = cuotas,
                    Detalles = detalles,
                    FechaPago = DateTime.Now
                };
                _mppPago.Alta(pago);

                // Crear Venta (en estado "Pendiente")
                var venta = new Venta
                {
                    Vendedor = new Vendedor { ID = vendedorId, Nombre = vendedorNombre },
                    Cliente = cliente,
                    Vehiculo = veh,
                    Pago = pago,
                    Estado = "Pendiente",
                    Fecha = DateTime.Now
                };
                _mppVenta.Alta(venta);

                // Marcar vehículo como no disponible
                veh.Estado = "Vendido";
                _mppVehiculo.Actualizar(veh);

                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }
    }
}
