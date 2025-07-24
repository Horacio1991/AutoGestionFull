using BE;
using DTOs;
using Mapper;

namespace BLL
{
    public class BLLDashboard
    {
        private readonly MPPFactura _mppFactura = new MPPFactura();
        private readonly MPPVenta _mppVenta = new MPPVenta();
        private readonly MPPPago _mppPago = new MPPPago();
        private readonly MPPUsuario _mppUsuario = new MPPUsuario();

        // Devuelve ventas con TipoPago, por día, para columnas y torta
        //public List<DashboardVentaDto> ObtenerVentasFiltradas(DateTime desde, DateTime hasta)
        //{
        //    try
        //    {
        //        // Listo todas las facturas del rango
        //        var facturas = _mppFactura.ListarTodo()
        //            .Where(f => f.Fecha.Date >= desde.Date && f.Fecha.Date <= hasta.Date)
        //            .ToList();

        //        // Listo ventas y pagos para buscar coincidencias
        //        var ventas = _mppVenta.ListarTodo();
        //        var pagos = _mppPago.ListarTodo();

        //        var lista = new List<DashboardVentaDto>();
        //        foreach (var f in facturas)
        //        {
        //            // Busco la venta por coincidencia de cliente, vehiculo, fecha y precio
        //            var venta = ventas.FirstOrDefault(v =>
        //                v.Cliente?.Dni == f.Cliente?.Dni &&
        //                v.Vehiculo?.Dominio == f.Vehiculo?.Dominio &&
        //                v.Fecha.Date == f.Fecha.Date &&
        //                v.Pago != null &&
        //                v.Pago.Monto == f.Precio
        //             );


        //            string tipoPago = "Sin Dato";
        //            if (venta != null && venta.Pago != null)
        //            {
        //                var pago = pagos.FirstOrDefault(p => p.ID == venta.Pago.ID);
        //                if (pago != null && !string.IsNullOrEmpty(pago.TipoPago))
        //                    tipoPago = pago.TipoPago;
        //            }

        //            lista.Add(new DashboardVentaDto
        //            {
        //                Fecha = f.Fecha.Date,
        //                Total = f.Precio,
        //                TipoPago = tipoPago
        //            });
        //        }

        //        // Agrupo por fecha y tipo de pago (para gráfico de torta)
        //        return lista
        //            .GroupBy(x => new { x.Fecha, x.TipoPago })
        //            .Select(g => new DashboardVentaDto
        //            {
        //                Fecha = g.Key.Fecha,
        //                Total = g.Sum(x => x.Total),
        //                TipoPago = g.Key.TipoPago
        //            })
        //            .OrderBy(x => x.Fecha)
        //            .ToList();
        //    }
        //    catch (Exception)
        //    {
        //        return new List<DashboardVentaDto>();
        //    }
        //}
        // Ranking de vendedores por total vendido (barras)

        public List<DashboardVentaDto> ObtenerVentasFiltradas(DateTime desde, DateTime hasta)
        {
            try
            {
                return _mppFactura.ListarTodo()
                    .Where(f => f.Fecha.Date >= desde.Date && f.Fecha.Date <= hasta.Date)
                    .Select(f => new DashboardVentaDto
                    {
                        Fecha = f.Fecha.Date,
                        Total = f.Precio,
                        TipoPago = f.FormaPago ?? "Sin Dato"
                    })
                    .ToList();
            }
            catch (Exception)
            {
                return new List<DashboardVentaDto>();
            }
        }

        public List<DashboardRankingDto> ObtenerRanking(DateTime desde, DateTime hasta)
        {
            try
            {
                // Ventas facturadas o entregadas en el rango
                var ventas = _mppVenta.ListarTodo()
                   .Where(v =>
                       (v.Estado.Equals("Facturada", StringComparison.OrdinalIgnoreCase)
                        || v.Estado.Equals("Entregada", StringComparison.OrdinalIgnoreCase))
                       && v.Fecha.Date >= desde.Date
                       && v.Fecha.Date <= hasta.Date)
                   .ToList();

                var usuarios = _mppUsuario.ListarTodo();
                var pagos = _mppPago.ListarTodo();

                var datos = ventas.Select(v =>
                {
                    var pago = pagos.FirstOrDefault(p => p.ID == v.Pago.ID) ?? new Pago { Monto = 0 };
                    var usr = usuarios.FirstOrDefault(u => u.Id == v.Vendedor.ID);
                    string username = usr?.Username ?? $"Usuario #{v.Vendedor.ID}";
                    return new
                    {
                        Vendedor = username,
                        Importe = pago.Monto
                    };
                });

                return datos
                    .GroupBy(x => x.Vendedor)
                    .Select(g => new DashboardRankingDto
                    {
                        Vendedor = g.Key,
                        Total = g.Sum(x => x.Importe)
                    })
                    .OrderByDescending(r => r.Total)
                    .ToList();
            }
            catch
            {
                return new List<DashboardRankingDto>();
            }
        }

        // Total facturado (para el label)
        public decimal ObtenerTotalFacturado(DateTime desde, DateTime hasta)
        {
            try
            {
                return _mppFactura.ListarTodo()
                    .Where(f => f.Fecha.Date >= desde.Date && f.Fecha.Date <= hasta.Date)
                    .Sum(f => f.Precio);
            }
            catch
            {
                return 0;
            }
        }
    }
}
