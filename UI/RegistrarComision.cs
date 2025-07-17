using BLL;
using DTOs;

namespace AutoGestion.UI
{
    public partial class RegistrarComision : UserControl
    {
        private readonly BLLComision _bllComision = new BLLComision();
        private List<VentaComisionDto> _ventas;

        public RegistrarComision()
        {
            InitializeComponent();
            CargarVentasSinComision();
        }

        private void CargarVentasSinComision()
        {
            try
            {
                _ventas = _bllComision.ObtenerVentasSinComision();
                dgvVentas.DataSource = _ventas;
                dgvVentas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvVentas.ReadOnly = true;
                dgvVentas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar ventas sin comisión:\n{ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnConfirmar_Click_1(object sender, EventArgs e)
        {
            if (dgvVentas.CurrentRow?.DataBoundItem is not VentaComisionDto venta)
            {
                MessageBox.Show("Seleccione una venta.", "Validación",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtComisionFinal.Text.Trim(), out var monto) || monto <= 0)
            {
                MessageBox.Show("Ingrese un monto de comisión válido.", "Validación",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var input = new ComisionInputDto
            {
                VentaID = venta.VentaID,
                Monto = monto,
                Estado = "Aprobada",
                MotivoRechazo = null
            };

            try
            {
                _bllComision.RegistrarComision(input);
                MessageBox.Show("✅ Comisión aprobada correctamente.", "Éxito",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtComisionFinal.Clear();
                CargarVentasSinComision();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al aprobar comisión:\n{ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRechazar_Click(object sender, EventArgs e)
        {
            if (dgvVentas.CurrentRow?.DataBoundItem is not VentaComisionDto venta)
            {
                MessageBox.Show("Seleccione una venta.", "Validación",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var motivo = txtMotivoRechazo.Text.Trim();
            if (string.IsNullOrEmpty(motivo))
            {
                MessageBox.Show("Ingrese el motivo del rechazo.", "Validación",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var input = new ComisionInputDto
            {
                VentaID = venta.VentaID,
                Monto = 0m,
                Estado = "Rechazada",
                MotivoRechazo = motivo
            };

            try
            {
                _bllComision.RegistrarComision(input);
                MessageBox.Show("❌ Comisión rechazada correctamente.", "Éxito",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMotivoRechazo.Clear();
                CargarVentasSinComision();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al rechazar comisión:\n{ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}
