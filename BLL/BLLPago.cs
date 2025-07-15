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

        // 1) Lista vehículos con estado "Disponible"
        public List<VehiculoDto> ObtenerVehiculosDisponibles()
            => _mppVehiculo.ListarTodo()
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

        // 2) Buscar cliente por DNI
        public ClienteDto BuscarCliente(string dni)
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

        // 3) Registra Pago y Venta en XML
        //    devuelve false + mensaje en error si algo falla
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
                // a) Cliente
                var cliente = _mppCliente.BuscarPorDni(clienteDni)
                              ?? throw new ApplicationException("Cliente no existe.");

                // b) Vehículo
                var veh = _mppVehiculo.ListarTodo()
                          .FirstOrDefault(v => v.Dominio.Equals(vehiculoDominio, StringComparison.OrdinalIgnoreCase));
                if (veh == null) throw new ApplicationException("Vehículo no disponible.");

                // c) Crear Pago
                var pago = new Pago
                {
                    TipoPago = tipoPago,
                    Monto = monto,
                    Cuotas = cuotas,
                    Detalles = detalles,
                    FechaPago = DateTime.Now
                };
                _mppPago.Alta(pago);

                // d) Crear Venta (en estado "Pendiente")
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

                // e) Marcar vehículo como no disponible
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
