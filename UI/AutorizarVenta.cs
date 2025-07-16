using BLL;
using DTOs;

namespace AutoGestion.UI
{
    public partial class AutorizarVenta : UserControl
    {
        private readonly BLLVenta _bll = new BLLVenta();
        private List<VentaDto> _ventas;

        public AutorizarVenta()
        {
            InitializeComponent();
            CargarVentas();
        }

        private void CargarVentas()
        {
            try
            {
                // Llamamos al nuevo método que devuelve DTOs
                _ventas = _bll.ObtenerVentasPendientesDto();

                dgvVentas.DataSource = null;
                dgvVentas.AutoGenerateColumns = false;
                dgvVentas.Columns.Clear();

                // Definimos solo las columnas que necesitamos
                dgvVentas.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = nameof(VentaDto.ID),
                    HeaderText = "ID",
                    Width = 50
                });
                dgvVentas.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = nameof(VentaDto.Cliente),
                    HeaderText = "Cliente",
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                });
                dgvVentas.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = nameof(VentaDto.Vehiculo),
                    HeaderText = "Vehículo",
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                });
                dgvVentas.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = nameof(VentaDto.Fecha),
                    HeaderText = "Fecha",
                    DefaultCellStyle = { Format = "g" }
                });
                dgvVentas.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = nameof(VentaDto.Estado),
                    HeaderText = "Estado"
                });
                dgvVentas.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = nameof(VentaDto.MotivoRechazo),
                    HeaderText = "Motivo Rechazo",
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                });

                dgvVentas.DataSource = _ventas;
                dgvVentas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvVentas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvVentas.ReadOnly = true;
                dgvVentas.ClearSelection();

                txtMotivoRechazo.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar ventas pendientes:\n{ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAutorizar_Click(object sender, EventArgs e)
        {
            if (dgvVentas.CurrentRow?.DataBoundItem is not VentaDto dto)
            {
                MessageBox.Show("Seleccione una venta para autorizar.",
                                "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                bool ok = _bll.AutorizarVenta(dto.ID);
                MessageBox.Show(
                    ok ? "✅ Venta autorizada." : "❌ No se pudo autorizar la venta.",
                    "Autorizar venta",
                    MessageBoxButtons.OK,
                    ok ? MessageBoxIcon.Information : MessageBoxIcon.Warning
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al autorizar la venta:\n{ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CargarVentas();
            }
        }

        private void btnRechazar_Click(object sender, EventArgs e)
        {
            if (dgvVentas.CurrentRow?.DataBoundItem is not VentaDto dto)
            {
                MessageBox.Show("Seleccione una venta para rechazar.",
                                "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var motivo = txtMotivoRechazo.Text.Trim();
            if (string.IsNullOrEmpty(motivo))
            {
                MessageBox.Show("Ingrese un motivo de rechazo.",
                                "Validación", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            try
            {
                bool ok = _bll.RechazarVenta(dto.ID, motivo);
                MessageBox.Show(
                    ok ? "✅ Venta rechazada." : "❌ No se pudo rechazar la venta.",
                    "Rechazar venta",
                    MessageBoxButtons.OK,
                    ok ? MessageBoxIcon.Information : MessageBoxIcon.Error
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al rechazar la venta:\n{ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CargarVentas();
            }
        }
    }
}
