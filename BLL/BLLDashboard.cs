using DTOs;
using Mapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    public class BLLDashboard
    {
        private readonly MPPFactura _mppFactura = new MPPFactura();
        private readonly MPPVenta _mppVenta = new MPPVenta();
        private readonly MPPPago _mppPago = new MPPPago();
        private readonly MPPUsuario _mppUsuario = new MPPUsuario();

        /// <summary>
        /// Suma el total de todas las facturas entre dos fechas.
        /// </summary>
        public decimal ObtenerTotalFacturado(DateTime desde, DateTime hasta)
        {
            return _mppFactura.ListarTodo()
                .Where(f => f.Fecha.Date >= desde.Date && f.Fecha.Date <= hasta.Date)
                .Sum(f => f.Precio);
        }

        /// <summary>
        /// Devuelve una lista de ventas (fecha + total) para el período.
        /// </summary>
        public List<DashboardVentaDto> ObtenerVentasFiltradas(DateTime desde, DateTime hasta)
        {
            return _mppFactura.ListarTodo()
                .Where(f => f.Fecha.Date >= desde.Date && f.Fecha.Date <= hasta.Date)
                .GroupBy(f => f.Fecha.Date)
                .Select(g => new DashboardVentaDto
                {
                    Fecha = g.Key,
                    Total = g.Sum(f => f.Precio)
                })
                .OrderBy(d => d.Fecha)
                .ToList();
        }

        /// <summary>
        /// Ranking de vendedores por total facturado en el período.
        /// </summary>
        public List<DashboardRankingDto> ObtenerRanking(DateTime desde, DateTime hasta)
        {
            // 1) Ventas facturadas en el rango
            var ventas = _mppVenta.ListarTodo()
               .Where(v =>
               (v.Estado.Equals("Facturada", StringComparison.OrdinalIgnoreCase)
               || v.Estado.Equals("Entregada", StringComparison.OrdinalIgnoreCase))
               && v.Fecha.Date >= desde.Date
               && v.Fecha.Date <= hasta.Date
               )
               .ToList();



            // 2) Extraemos (username, monto) con fallback si usuario no existe
            var datos = ventas.Select(v =>
            {
                var pago = _mppPago.BuscarPorId(v.Pago.ID)
                          ?? throw new ApplicationException($"Pago {v.Pago.ID} no encontrado.");

                var usr = _mppUsuario.BuscarPorId(v.Vendedor.ID);
                string username = usr?.Username;
                if (string.IsNullOrWhiteSpace(username))
                    username = $"Usuario #{v.Vendedor.ID}";

                return new
                {
                    Vendedor = username,
                    Importe = pago.Monto
                };
            });

            // 3) Agrupar y sumar
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
    }
}
