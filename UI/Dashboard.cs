using BLL;
using DTOs;
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
            try
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

                // 1) Ventas
                _ventas = _bll.ObtenerVentasFiltradas(desde, hasta);
                dgvVentas.DataSource = _ventas;
                dgvVentas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                DibujarGraficoVentas(_ventas);

                // 2) Ranking
                // Ranking
                _ranking = _bll.ObtenerRanking(desde, hasta);

                dgvRanking.AutoGenerateColumns = true;
                dgvRanking.DataSource = _ranking;
                dgvRanking.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                DibujarGraficoRanking(_ranking);

            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error al cargar dashboard:\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error
                );
            }
        }

        private void DibujarGraficoVentas(List<DashboardVentaDto> ventas)
        {
            var chart = chartVentas;
            var serie = chart.Series["Series1"];
            serie.Points.Clear();
            serie.ChartType = SeriesChartType.Column;
            serie.IsValueShownAsLabel = true;
            chart.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = false;
            chart.ChartAreas["ChartArea1"].AxisY.MajorGrid.Enabled = false;

            // Agrupo por día
            var porDia = ventas
                .GroupBy(v => v.Fecha)
                .Select(g => new { Fecha = g.Key, Total = g.Sum(x => x.Total) })
                .OrderBy(x => x.Fecha);

            foreach (var item in porDia)
            {
                int idx = serie.Points.AddXY(item.Fecha, item.Total);
                serie.Points[idx].ToolTip = $"{item.Fecha:dd/MM/yyyy}: {item.Total:C2}";
            }

            // Formato eje X como fechas
            var ax = chart.ChartAreas["ChartArea1"].AxisX;
            ax.LabelStyle.Format = "dd/MM";
            ax.Interval = 1;
            chart.Legends["Legend1"].Enabled = false;
        }

        private void DibujarGraficoRanking(List<DashboardRankingDto> ranking)
        {
            chartRanking.Series.Clear();
            chartRanking.ChartAreas[0].AxisY.CustomLabels.Clear(); // Limpio primero
            chartRanking.Legends.Clear();

            var serie = new Series("Ranking")
            {
                ChartType = SeriesChartType.Bar,
                IsValueShownAsLabel = true
            };

            // IMPORTANTE: Usar i como posición en el eje Y (1,2,3...)
            for (int i = 0; i < ranking.Count; i++)
            {
                var item = ranking[i];
                serie.Points.AddXY(i + 1, item.Total);
                serie.Points[i].ToolTip = $"{item.Vendedor}: {item.Total:C2}";

                // Etiqueta personalizada para el eje Y con el nombre del vendedor
                chartRanking.ChartAreas[0].AxisY.CustomLabels.Add(
                    new CustomLabel(i + 0.5, i + 1.5, item.Vendedor, 0, LabelMarkStyle.None)
                );
            }

            chartRanking.Series.Add(serie);
            chartRanking.ChartAreas[0].AxisY.Interval = 1;
            chartRanking.Legends.Clear();
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
