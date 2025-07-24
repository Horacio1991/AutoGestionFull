using BLL;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Vista.UserControls.Dashboard
{
    public partial class Dashboard : UserControl
    {
        private readonly BLLDashboard _bll = new BLLDashboard();
        private List<DashboardVentaDto> _ventas;
        private List<DashboardRankingDto> _ranking;

        public Dashboard()
        {
            InitializeComponent();

            cmbFiltroPeriodo.Items.AddRange(new object[]
            {
                "Hoy", "Últimos 7 días", "Últimos 30 días"
            });
            cmbFiltroPeriodo.SelectedIndexChanged += (_, __) => AplicarFiltro();
            cmbFiltroPeriodo.SelectedIndex = 0;

            AplicarFiltro();
        }

        private void AplicarFiltro()
        {
            DateTime hoy = DateTime.Today, desde, hasta;

            switch ((string)cmbFiltroPeriodo.SelectedItem)
            {
                case "Hoy":
                    desde = hasta = hoy; break;
                case "Últimos 7 días":
                    desde = hoy.AddDays(-6); hasta = hoy; break;
                case "Últimos 30 días":
                    desde = hoy.AddDays(-29); hasta = hoy; break;
                default:
                    desde = hasta = hoy; break;
            }

            // Total facturado
            decimal total = _bll.ObtenerTotalFacturado(desde, hasta);
            lblTotalFacturado.Text = $"{ObtenerTextoPeriodo()}: {total:C2}";

            // Trae los datos de ventas y ranking
            _ventas = _bll.ObtenerVentasFiltradas(desde, hasta);
            _ranking = _bll.ObtenerRanking(desde, hasta);

            dgvVentas.DataSource = _ventas;
            dgvVentas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgvRanking.DataSource = _ranking;
            dgvRanking.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            InicializarGraficos();
            DibujarGraficoVentas(_ventas);
            DibujarGraficoTorta(_ventas);
            DibujarGraficoRanking(_ranking);
        }

        // ---- Inicialización de gráficos ----
        private void InicializarGraficos()
        {
            // --- Gráfico de Ventas por Día (Columnas/Vertical) ---
            chartVentas.Series.Clear();
            var sVentas = new Series("Ventas por Día")
            {
                ChartType = SeriesChartType.Column,
                IsValueShownAsLabel = true
            };
            chartVentas.Series.Add(sVentas);
            chartVentas.ChartAreas[0].AxisX.Title = "Fecha";
            chartVentas.ChartAreas[0].AxisY.Title = "Total Vendido";
            chartVentas.Legends.Clear();

            // --- Gráfico de Formas de Pago (Torta) ---
            chartTorta.Series.Clear();
            var sPago = new Series("Ventas por Forma de Pago")
            {
                ChartType = SeriesChartType.Pie,
                IsValueShownAsLabel = true
            };
            chartTorta.Series.Add(sPago);
            chartTorta.Legends.Clear();
            chartTorta.Legends.Add(new Legend("Legend1") { Title = "Forma de Pago", Docking = Docking.Bottom });

            // --- Gráfico de Ranking de Vendedores (Barras/Horizontal) ---
            chartRanking.Series.Clear();
            var sRanking = new Series("Ranking de Vendedores")
            {
                ChartType = SeriesChartType.Bar,
                IsValueShownAsLabel = true
            };
            chartRanking.Series.Add(sRanking);
            chartRanking.ChartAreas[0].AxisY.Title = "Vendedor";
            chartRanking.ChartAreas[0].AxisX.Title = "Total Vendido";
            chartRanking.Legends.Clear();
        }

        // ---- Gráfico de Ventas por Día (Vertical) ----
        private void DibujarGraficoVentas(List<DashboardVentaDto> ventas)
        {
            chartVentas.Series.Clear();
            chartVentas.ChartAreas[0].AxisX.CustomLabels.Clear();
            chartVentas.Legends.Clear();
            chartVentas.Titles.Clear();
            var tituloVentas = new Title("Ventas por Día", Docking.Top, new Font("Arial", 14, FontStyle.Bold), Color.Black);
            tituloVentas.Alignment = ContentAlignment.TopCenter;
            chartVentas.Titles.Add(tituloVentas);


            var serie = new Series("Ventas")
            {
                ChartType = SeriesChartType.Column,
                IsValueShownAsLabel = true
            };

            // Agrupamos ventas por fecha (ya que pueden haber varias ventas el mismo día)
            var porDia = ventas
                .GroupBy(v => v.Fecha.Date)
                .Select(g => new { Fecha = g.Key, Total = g.Sum(x => x.Total) })
                .OrderBy(x => x.Fecha)
                .ToList();

            for (int i = 0; i < porDia.Count; i++)
            {
                var item = porDia[i];
                // El eje X es el índice (1,2,3...), no la fecha
                serie.Points.AddXY(i + 1, item.Total);
                serie.Points[i].ToolTip = $"{item.Fecha:dd/MM/yyyy}: {item.Total:C2}";

                // Etiqueta personalizada para el eje X (fechas)
                chartVentas.ChartAreas[0].AxisX.CustomLabels.Add(
                    new CustomLabel(i + 0.5, i + 1.5, item.Fecha.ToString("dd/MM"), 0, LabelMarkStyle.None)
                );
            }

            chartVentas.Series.Add(serie);
            chartVentas.ChartAreas[0].AxisX.Interval = 1;
            chartVentas.ChartAreas[0].AxisY.Title = "Monto ($)";
            chartVentas.ChartAreas[0].AxisX.Title = "Fecha";
            chartVentas.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chartVentas.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
        }

        // ---- Gráfico de Formas de Pago (Torta) ----
        private void DibujarGraficoTorta(List<DashboardVentaDto> ventas)
        {
            chartTorta.Series.Clear();
            chartTorta.Legends.Clear();
            chartTorta.Titles.Clear();
            var tituloTorta = new Title("Distribución por Forma de Pago", Docking.Top, new Font("Arial", 14, FontStyle.Bold), Color.Black);
            tituloTorta.Alignment = ContentAlignment.TopCenter;
            chartTorta.Titles.Add(tituloTorta);


            var serie = new Series("Formas de Pago")
            {
                ChartType = SeriesChartType.Pie,
                IsValueShownAsLabel = true
            };

            var porTipo = ventas
                .GroupBy(v => v.TipoPago ?? "Sin Dato")
                .Select(g => new { Tipo = g.Key, Total = g.Sum(x => x.Total) })
                .ToList();

            foreach (var item in porTipo)
            {
                int idx = serie.Points.AddXY(item.Tipo, item.Total);
                serie.Points[idx].LegendText = $"{item.Tipo}";
                serie.Points[idx].ToolTip = $"{item.Tipo}: {item.Total:C2}";
                serie.Points[idx].Label = $"{item.Tipo}: {item.Total:C2}";
            }

            chartTorta.Series.Add(serie);

            // Agregamos la leyenda si hace falta
            var legend = new Legend("Formas de Pago");
            chartTorta.Legends.Add(legend);
        }

        // ---- Gráfico de Ranking de Vendedores (Barras/Horizontal) ----
        private void DibujarGraficoRanking(List<DashboardRankingDto> ranking)
        {

            var chart = chartRanking;
            chart.Series.Clear();
            chart.ChartAreas[0].AxisY.CustomLabels.Clear();
            chart.Legends.Clear();
            // Título del gráfico de Ranking de Vendedores
            chart.Titles.Clear(); // Limpio títulos viejos si los hay
            var tituloRanking = new Title("Ranking de Vendedores", Docking.Top, new Font("Arial", 14, FontStyle.Bold), Color.Black);
            tituloRanking.Alignment = ContentAlignment.TopCenter;
            chart.Titles.Add(tituloRanking);


            var serie = new Series("Ranking")
            {
                ChartType = SeriesChartType.Bar, // Barras horizontales
                IsValueShownAsLabel = false // Vamos a usar Label personalizado, no el valor por defecto
            };

            // Si no hay datos, no dibujo nada
            if (ranking == null || ranking.Count == 0)
                return;

            for (int i = 0; i < ranking.Count; i++)
            {
                var item = ranking[i];
                int idx = serie.Points.AddXY(i + 1, item.Total);

                // Mostramos el nombre y el monto como etiqueta en la barra
                // Si querés solo el nombre, poné: serie.Points[idx].Label = item.Vendedor;
                serie.Points[idx].Label = $"{item.Vendedor}: {item.Total:N0}";

                // Tooltip para mayor claridad (se ve al pasar el mouse)
                serie.Points[idx].ToolTip = $"{item.Vendedor}: {item.Total:C2}";

                // Etiqueta personalizada en el eje Y (deja el nombre del vendedor en el eje)
                chart.ChartAreas[0].AxisY.CustomLabels.Add(
                    new CustomLabel(i + 0.5, i + 1.5, item.Vendedor, 0, LabelMarkStyle.None)
                );
            }

            // Opciones visuales
            serie["BarLabelStyle"] = "Center"; // Podés probar "Outside" si querés fuera de la barra

            chart.Series.Add(serie);
            chart.ChartAreas[0].AxisY.Interval = 1;
            chart.ChartAreas[0].AxisY.Title = "Vendedor";
            chart.ChartAreas[0].AxisX.Title = "Total Vendido ($)";

            // Leyenda clara (opcional, podés dejarla desactivada si no te gusta)
            var legend = new Legend("Vendedores");
            legend.Docking = Docking.Top;
            chart.Legends.Add(legend);
        }

        private string ObtenerTextoPeriodo() => cmbFiltroPeriodo.SelectedItem as string switch
        {
            "Hoy" => "Total del día",
            "Últimos 7 días" => "Total últimos 7 días",
            "Últimos 30 días" => "Total últimos 30 días",
            _ => "Total"
        };
    }
}
