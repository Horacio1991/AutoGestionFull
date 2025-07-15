using AutoGestion.CTRL_Vista;
using AutoGestion.DTOs;

namespace AutoGestion.Vista
{
    public partial class RegistrarComision : UserControl
    {
        private readonly ComisionController _ctrl = new ComisionController();
        private List<VentaComisionDto> _ventas;

        public RegistrarComision()
        {
            InitializeComponent();
            CargarVentasSinComision();
        }

        // Carga en el DataGridView todas las ventas entregadas sin comisión registrada.
        private void CargarVentasSinComision()
        {
            try
            {
                _ventas = _ctrl.ObtenerVentasSinComision();
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

        //registra la comisión aprobada.
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

            var dto = new ComisionInputDto
            {
                VentaID = venta.VentaID,
                Monto = monto,
                Estado = "Aprobada",
                MotivoRechazo = null
            };

            try
            {
                bool ok = _ctrl.RegistrarComision(dto);
                if (ok)
                {
                    MessageBox.Show("✅ Comisión aprovada correctamente.", "Éxito",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtComisionFinal.Clear();
                    CargarVentasSinComision();
                }
                else
                {
                    MessageBox.Show("No se pudo registrar la comisión.", "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al aprobar comisión:\n{ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // registra la comisión como rechazada con motivo.
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

            var dto = new ComisionInputDto
            {
                VentaID = venta.VentaID,
                Monto = 0m,
                Estado = "Rechazada",
                MotivoRechazo = motivo
            };

            try
            {
                bool ok = _ctrl.RegistrarComision(dto);
                if (ok)
                {
                    MessageBox.Show("❌ Comisión rechazada correctamente.", "Éxito",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtMotivoRechazo.Clear();
                    CargarVentasSinComision();
                }
                else
                {
                    MessageBox.Show("No se pudo rechazar la comisión.", "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al rechazar comisión:\n{ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
