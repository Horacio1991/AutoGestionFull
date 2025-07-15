using BLL;
using DTOs;
using BE; // para UsuarioSesion

namespace AutoGestion.UI
{
    public partial class ConsultarComisiones : UserControl
    {
        private readonly BLLComision _bll = new BLLComision();
        private List<ComisionListDto> _comisiones;

        public ConsultarComisiones()
        {
            InitializeComponent();
            InicializarFiltros();
            CargarComisiones();
        }

        private void InicializarFiltros()
        {
            try
            {
                // UsuarioSesion fue definido en BE.UsuarioSesion
                txtVendedor.Text = UsuarioSesion.UsuarioActual?.Username ?? "";

                cmbEstado.Items.Clear();
                cmbEstado.Items.AddRange(new[] { "Aprobada", "Rechazada" });
                cmbEstado.SelectedIndex = 0;

                dtpDesde.Value = DateTime.Today.AddMonths(-1);
                dtpHasta.Value = DateTime.Today;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error al inicializar filtros:\n{ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void CargarComisiones()
        {
            try
            {
                int vendedorId = UsuarioSesion.UsuarioActual?.Id ?? 0;
                string estado = cmbEstado.SelectedItem?.ToString() ?? "Aprobada";
                DateTime desde = dtpDesde.Value.Date;
                DateTime hasta = dtpHasta.Value.Date;

                // Llamada directa a la BLL, que devuelve DTOs
                _comisiones = _bll.ObtenerComisiones(vendedorId, estado, desde, hasta);

                dgvComisiones.DataSource = _comisiones;
                dgvComisiones.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvComisiones.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvComisiones.ReadOnly = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error al cargar comisiones:\n{ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            CargarComisiones();
        }

        private void btnDetalle_Click(object sender, EventArgs e)
        {
            if (dgvComisiones.CurrentRow?.DataBoundItem is not ComisionListDto com)
            {
                MessageBox.Show(
                    "Seleccione una comisión del listado.",
                    "Atención",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            if (string.Equals(com.Estado, "Aprobada", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show(
                    "✅ Comisión aprobada.",
                    "Detalle",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(
                    $"❌ Comisión rechazada.\nMotivo: {com.MotivoRechazo}",
                    "Detalle",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }
    }
}
