using BLL;
using DTOs;

namespace AutoGestion.UI
{
    public partial class ConsultarComisiones : UserControl
    {
        private readonly BLLComision _bll = new BLLComision();
        private List<ComisionListDto> _coms;

        public ConsultarComisiones()
        {
            InitializeComponent();
            InicializarFiltros();
            CargarComisiones();
        }

        private void InicializarFiltros()
        {
            // Ahora usando el DTO de SessionManager
            txtVendedor.Text = SessionManager.CurrentUser?.Username ?? "";
            txtVendedor.ReadOnly = true;

            cmbEstado.Items.Clear();
            cmbEstado.Items.AddRange(new[] { "Aprobada", "Rechazada" });
            cmbEstado.SelectedIndex = 0;

            dtpDesde.Value = DateTime.Today.AddMonths(-1);
            dtpHasta.Value = DateTime.Today;

            btnFiltrar.Click += (_, __) => CargarComisiones();
        }

        private void CargarComisiones()
        {
            try
            {
                // Tomamos el ID del DTO de sesión
                int vendedorId = SessionManager.CurrentUser?.ID ?? 0;
                string estado = cmbEstado.SelectedItem.ToString()!;
                DateTime desde = dtpDesde.Value.Date;
                DateTime hasta = dtpHasta.Value.Date;

                // Obtenemos los DTOs directamente
                _coms = _bll.ObtenerComisiones(vendedorId, estado, desde, hasta);

                dgvComisiones.AutoGenerateColumns = true;
                dgvComisiones.DataSource = _coms;

                // Sólo dejamos visibles estas columnas
                var visibles = new[]
                {
                    nameof(ComisionListDto.ID),
                    nameof(ComisionListDto.Fecha),
                    nameof(ComisionListDto.Cliente),
                    nameof(ComisionListDto.Vehiculo),
                    nameof(ComisionListDto.Monto),
                    nameof(ComisionListDto.Estado)
                };

                foreach (DataGridViewColumn col in dgvComisiones.Columns)
                    col.Visible = visibles.Contains(col.DataPropertyName);

                dgvComisiones.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvComisiones.ReadOnly = true;
                dgvComisiones.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvComisiones.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error al cargar comisiones:\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error
                );
            }
        }


    }
}
