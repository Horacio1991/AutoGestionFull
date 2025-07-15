using AutoGestion.CTRL_Vista;
using AutoGestion.DTOs;
using System.Windows.Forms.DataVisualization.Charting;
namespace Vista.UserControls.Dashboard
{
    public partial class Dashboard : UserControl
    {
        private readonly DashboardController _ctrl = new();
        private List<DashboardVentaDto> _ventas;
        private List<DashboardRankingDto> _ranking;

        public Dashboard()
        {
            InitializeComponent();

            // 1) Inicializo el combo de periodos
            cmbFiltroPeriodo.Items.AddRange(new object[]
            {
                "Hoy", "Últimos 7 días", "Últimos 30 días"
            });
            cmbFiltroPeriodo.SelectedIndexChanged += (_, __) => AplicarFiltro();
            cmbFiltroPeriodo.SelectedIndex = 0;

            // 2) Cargo la primera vez
            AplicarFiltro();
        }

        private void cmbFiltroPeriodo_SelectedIndexChanged(object sender, EventArgs e)
            => AplicarFiltro();


        // Determina el rango de fechas según el filtro seleccionado
        // y actualiza ambos gráficos (ventas y ranking).

        private void AplicarFiltro()
        {
            try
            {
                DateTime hoy = DateTime.Today;
                DateTime desde, hasta;

                switch (cmbFiltroPeriodo.SelectedItem as string)
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
                var total = _ctrl.ObtenerTotalFacturado(desde, hasta);
                lblTotalFacturado.Text = $"{ObtenerTextoPeriodo()}: {total:C2}";

                // Ventas para la grilla
                _ventas = _ctrl.ObtenerVentasFiltradas(desde, hasta);
                dgvVentas.DataSource = _ventas;
                dgvVentas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                // Gráfico de ventas por día
                CargarGraficoVentas(_ventas);

                // Ranking para grilla y gráfico
                _ranking = _ctrl.ObtenerRanking(desde, hasta);
                dgvRanking.DataSource = _ranking;
                dgvRanking.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                CargarGraficoRanking(_ranking);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error al cargar dashboard:\n{ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void CargarGraficoVentas(List<DashboardVentaDto> ventas)
        {
            chartVentas.Series.Clear();
            var serie = new Series("Ventas")
            {
                ChartType = SeriesChartType.Column,
                IsValueShownAsLabel = true
            };
            var porDia = ventas
                .GroupBy(v => v.Fecha)
                .Select(g => new { Fecha = g.Key, Total = g.Sum(x => x.Total) })
                .ToList();

            for (int i = 0; i < porDia.Count; i++)
            {
                var item = porDia[i];
                serie.Points.AddXY(i + 1, item.Total);
                serie.Points[i].ToolTip = $"{item.Fecha}: {item.Total:C2}";
                chartVentas.ChartAreas[0].AxisX.CustomLabels.Add(
                    new CustomLabel(i + 0.5, i + 1.5, item.Fecha, 0, LabelMarkStyle.None)
                );
            }

            chartVentas.Series.Add(serie);
            chartVentas.ChartAreas[0].AxisX.Interval = 1;
            chartVentas.Legends[0].Enabled = false;
        }

        private void CargarGraficoRanking(List<DashboardRankingDto> ranking)
        {
            chartRanking.Series.Clear();
            var serie = new Series("Ranking")
            {
                ChartType = SeriesChartType.Bar,
                IsValueShownAsLabel = true
            };

            for (int i = 0; i < ranking.Count; i++)
            {
                var item = ranking[i];
                serie.Points.AddXY(i + 1, item.Total);
                serie.Points[i].ToolTip = $"{item.Vendedor}: {item.Total:C2}";
                chartRanking.ChartAreas[0].AxisY.CustomLabels.Add(
                    new CustomLabel(i + 0.5, i + 1.5, item.Vendedor, 0, LabelMarkStyle.None)
                );
            }

            chartRanking.Series.Add(serie);
            chartRanking.ChartAreas[0].AxisY.Interval = 1;
            chartRanking.Legends[0].Enabled = false;
        }


        // Traduce el filtro seleccionado en el combo a un texto para el label.
        private string ObtenerTextoPeriodo() => cmbFiltroPeriodo.SelectedItem as string switch
        {
            "Hoy" => "Total del día",
            "Últimos 7 días" => "Total últimos 7 días",
            "Últimos 30 días" => "Total últimos 30 días",
            _ => "Total"
        };

    }
}
