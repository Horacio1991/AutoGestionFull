using BE;
using BLL;
using DTOs;

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
            btnFiltrar.Click += (_, __) => CargarComisiones();
            btnDetalle.Click += (_, __) => MostrarDetalle();
        }

        private void InicializarFiltros()
        {
            // UsuarioSesion se mantiene igual, o encapsúlalo en un helper si lo deseas
            txtVendedor.Text = UsuarioSesion.UsuarioActual?.Username ?? "";

            cmbEstado.Items.Clear();
            cmbEstado.Items.AddRange(new[] { "Aprobada", "Rechazada" });
            cmbEstado.SelectedIndex = 0;

            dtpDesde.Value = DateTime.Today.AddMonths(-1);
            dtpHasta.Value = DateTime.Today;
        }

        private void CargarComisiones()
        {
            try
            {
                int vendedorId = UsuarioSesion.UsuarioActual?.Id ?? 0;
                string estado = cmbEstado.SelectedItem?.ToString() ?? "Aprobada";
                DateTime desde = dtpDesde.Value.Date;
                DateTime hasta = dtpHasta.Value.Date;

                // Devuelve DTOs directamente
                _comisiones = _bll.ObtenerComisiones(vendedorId, estado, desde, hasta);

                dgvComisiones.DataSource = null;
                dgvComisiones.AutoGenerateColumns = false;
                dgvComisiones.Columns.Clear();

                dgvComisiones.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = nameof(ComisionListDto.ID),
                    HeaderText = "ID",
                    Width = 50
                });
                dgvComisiones.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = nameof(ComisionListDto.Fecha),
                    HeaderText = "Fecha",
                    DefaultCellStyle = { Format = "g" }
                });
                dgvComisiones.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = nameof(ComisionListDto.Cliente),
                    HeaderText = "Cliente",
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                });
                dgvComisiones.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = nameof(ComisionListDto.Vehiculo),
                    HeaderText = "Vehículo",
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                });
                dgvComisiones.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = nameof(ComisionListDto.Monto),
                    HeaderText = "Monto",
                    DefaultCellStyle = { Format = "C2" }
                });
                dgvComisiones.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = nameof(ComisionListDto.Estado),
                    HeaderText = "Estado"
                });
                // Ocultamos motivo por defecto, solo lo mostramos en detalle
                dgvComisiones.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = nameof(ComisionListDto.MotivoRechazo),
                    HeaderText = "MotivoRechazo",
                    Visible = false
                });

                dgvComisiones.DataSource = _comisiones;
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

        private void MostrarDetalle()
        {
            if (dgvComisiones.CurrentRow?.DataBoundItem is not ComisionListDto com)
            {
                MessageBox.Show(
                    "Seleccione una comisión del listado.",
                    "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning
                );
                return;
            }

            if (com.Estado.Equals("Aprobada", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show(
                    "✅ Comisión aprobada.",
                    "Detalle", MessageBoxButtons.OK, MessageBoxIcon.Information
                );
            }
            else
            {
                MessageBox.Show(
                    $"❌ Comisión rechazada.\nMotivo: {com.MotivoRechazo}",
                    "Detalle", MessageBoxButtons.OK, MessageBoxIcon.Warning
                );
            }
        }
    }
}
