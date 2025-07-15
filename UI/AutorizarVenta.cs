using System;
using System.Collections.Generic;
using System.Windows.Forms;
//using BE;
using BLL;

namespace AutoGestion.UI
{
    public partial class AutorizarVenta : UserControl
    {
        private readonly BLLVenta _bll = new BLLVenta();
        private List<Venta> _ventas;

        public AutorizarVenta()
        {
            InitializeComponent();
            CargarVentas();
        }

        private void CargarVentas()
        {
            try
            {
                _ventas = _bll.ListarPendientes();  // trae List<BE.Venta>
                dgvVentas.DataSource = null;
                dgvVentas.DataSource = _ventas;
                dgvVentas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvVentas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvVentas.ReadOnly = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar ventas pendientes:\n{ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAutorizar_Click(object sender, EventArgs e)
        {
            if (dgvVentas.CurrentRow?.DataBoundItem is not Venta venta)
            {
                MessageBox.Show("Seleccione una venta para autorizar.",
                                "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                bool ok = _bll.AutorizarVenta(venta.ID);
                MessageBox.Show(ok ? "✅ Venta autorizada." : "❌ No se pudo autorizar.",
                                "Autorizar venta",
                                MessageBoxButtons.OK,
                                ok ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
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
            if (dgvVentas.CurrentRow?.DataBoundItem is not Venta venta)
            {
                MessageBox.Show("Seleccione una venta para rechazar.",
                                "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string motivo = txtMotivoRechazo.Text.Trim();
            if (string.IsNullOrEmpty(motivo))
            {
                MessageBox.Show("Ingrese un motivo de rechazo.",
                                "Validación", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            try
            {
                bool ok = _bll.RechazarVenta(venta.ID, motivo);
                MessageBox.Show(ok ? "✅ Venta rechazada." : "❌ No se pudo rechazar.",
                                "Rechazar venta",
                                MessageBoxButtons.OK,
                                ok ? MessageBoxIcon.Information : MessageBoxIcon.Error);
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
