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

            // Configuro el combo de período
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

                // Asegurarnos de que el grid va a crear una columna para cada propiedad
                dgvRanking.AutoGenerateColumns = true;
                dgvRanking.DataSource = _ranking;
                dgvRanking.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                // Y luego dibujamos el chart
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
            var chart = chartRanking;
            var serie = chart.Series["Series1"];
            serie.Points.Clear();
            serie.ChartType = SeriesChartType.Bar;
            serie.IsValueShownAsLabel = true;

            // Desactivar gridlines si las tenías puestas
            chart.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = false;
            chart.ChartAreas["ChartArea1"].AxisY.MajorGrid.Enabled = false;

            // Si no hay datos, abortamos
            if (ranking == null || ranking.Count == 0)
                return;

            foreach (var item in ranking)
            {
                // Ya sabemos que item.Vendedor no es null
                int idx = serie.Points.AddXY(item.Vendedor, item.Total);
                serie.Points[idx].ToolTip = $"{item.Vendedor}: {item.Total:C2}";
            }

            chart.Legends["Legend1"].Enabled = false;
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
