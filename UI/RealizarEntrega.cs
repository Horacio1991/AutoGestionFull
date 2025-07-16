using BLL;
using DTOs;

namespace AutoGestion.UI
{
    public partial class RealizarEntrega : UserControl
    {
        private readonly BLLVenta _bll = new BLLVenta();
        private readonly BLLComprobanteEntrega _bllComp = new BLLComprobanteEntrega();
        private List<VentaDto> _ventas;

        public RealizarEntrega()
        {
            InitializeComponent();
            CargarVentas();
        }

        private void CargarVentas()
        {
            try
            {
                _ventas = _bll.ObtenerVentasParaEntrega();
                dgvVentas.DataSource = _ventas;
                dgvVentas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvVentas.ReadOnly = true;
                dgvVentas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar ventas para entrega:\n{ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnConfirmarEntrega_Click_1(object sender, EventArgs e)
        {
            if (dgvVentas.CurrentRow?.DataBoundItem is not VentaDto dto)
            {
                MessageBox.Show("Seleccione una venta para entregar.",
                                "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using var dlg = new SaveFileDialog
            {
                Filter = "PDF|*.pdf",
                FileName = $"Comprobante_{dto.ID}.pdf"
            };
            if (dlg.ShowDialog() != DialogResult.OK) return;

            try
            {
                _bllComp.EmitirComprobantePdf(dto.ID, dlg.FileName);
                MessageBox.Show("✅ Entrega registrada y comprobante generado correctamente.",
                                "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // recargar la grilla para quitar las entregadas
                CargarVentas();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al generar comprobante:\n{ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CargarVentas();
            }
        }


    }
}
