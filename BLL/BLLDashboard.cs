using DTOs;
using Mapper;

namespace BLL
{
    public class BLLDashboard
    {
        private readonly MPPFactura _mppFactura = new MPPFactura();
        private readonly MPPVenta _mppVenta = new MPPVenta();

        /// <summary>
        /// Suma el total de todas las facturas entre dos fechas.
        /// </summary>
        public decimal ObtenerTotalFacturado(DateTime desde, DateTime hasta)
        {
            var todas = _mppFactura.ListarTodo()
                                   .Where(f => f.Fecha.Date >= desde.Date && f.Fecha.Date <= hasta.Date);
            return todas.Sum(f => f.Precio);
        }

        /// <summary>
        /// Devuelve una lista de ventas (fecha + total) para el período.
        /// </summary>
        public List<DashboardVentaDto> ObtenerVentasFiltradas(DateTime desde, DateTime hasta)
        {
            return _mppFactura.ListarTodo()
                .Where(f => f.Fecha.Date >= desde.Date && f.Fecha.Date <= hasta.Date)
                .Select(f => new DashboardVentaDto
                {
                    Fecha = f.Fecha.Date,
                    Total = f.Precio
                })
                .OrderBy(d => d.Fecha)
                .ToList();
        }

        /// <summary>
        /// Ranking de vendedores por total facturado en el período.
        /// </summary>
        public List<DashboardRankingDto> ObtenerRanking(DateTime desde, DateTime hasta)
        {
            // Necesitamos combinar facturas con ventas para conocer vendedor
            var ventas = _mppVenta.ListarTodo()
                .Where(v => v.Estado.Equals("Facturada", StringComparison.OrdinalIgnoreCase))
                .ToDictionary(v => v.ID, v => v.Vendedor);

            return _mppFactura.ListarTodo()
                .Where(f => f.Fecha.Date >= desde.Date && f.Fecha.Date <= hasta.Date)
                .Where(f => ventas.ContainsKey(f.ID))
                .GroupBy(f => ventas[f.ID].ToString())
                .Select(g => new DashboardRankingDto
                {
                    Vendedor = g.Key,
                    Total = g.Sum(f => f.Precio)
                })
                .OrderByDescending(r => r.Total)
                .ToList();
        }
    }
}
