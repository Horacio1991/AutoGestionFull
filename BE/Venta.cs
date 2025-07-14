using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Venta
    {
        public int ID { get; set; }
        public Vendedor Vendedor { get; set; }
        public Cliente Cliente { get; set; }
        public Vehiculo Vehiculo { get; set; }
        public Pago Pago { get; set; }
        public string Estado { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
        public decimal Total => Pago?.Monto ?? 0;
        public string MotivoRechazo { get; set; }

    }
}
