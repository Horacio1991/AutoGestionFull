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
            // Setea nombre de vendedor o mensaje de sesión inválida
            if (SessionManager.CurrentUser == null)
            {
                txtVendedor.Text = "[Sesión inválida]";
                txtVendedor.ReadOnly = true;
                btnFiltrar.Enabled = false;
                dgvComisiones.Enabled = false;
                MessageBox.Show("Debe iniciar sesión para consultar comisiones.", "Acceso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            txtVendedor.Text = SessionManager.CurrentUser.Username;
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
                btnFiltrar.Enabled = false;

                // Si el usuario no está logueado, no seguimos
                if (SessionManager.CurrentUser == null)
                {
                    MessageBox.Show("Debe iniciar sesión para consultar comisiones.", "Acceso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dgvComisiones.DataSource = null;
                    return;
                }

                int vendedorId = SessionManager.CurrentUser.ID;
                string estado = (cmbEstado.SelectedItem ?? "Aprobada").ToString();
                DateTime desde = dtpDesde.Value.Date;
                DateTime hasta = dtpHasta.Value.Date;

                _coms = _bll.ObtenerComisiones(vendedorId, estado, desde, hasta);

                dgvComisiones.AutoGenerateColumns = true;
                dgvComisiones.DataSource = _coms;

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
            finally
            {
                btnFiltrar.Enabled = true;
            }
        }

        private void btnDetalle_Click(object sender, EventArgs e)
        {
            if (dgvComisiones.CurrentRow?.DataBoundItem is not ComisionListDto dto)
            {
                MessageBox.Show("Seleccione una comisión de la lista.", "Validación",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Mostramos el detalle según el estado
            if (dto.Estado.Equals("Rechazada", StringComparison.OrdinalIgnoreCase))
            {
                var motivo = string.IsNullOrWhiteSpace(dto.MotivoRechazo)
                    ? "Motivo de rechazo no especificado."
                    : dto.MotivoRechazo;
                MessageBox.Show(
                    $"Comisión rechazada.\n\nMotivo de rechazo:\n{motivo}",
                    "Detalle de Comisión", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(
                    "Comisión aprobada.",
                    "Detalle de Comisión", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

    }
}
