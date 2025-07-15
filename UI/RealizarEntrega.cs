using AutoGestion.Servicios.Pdf;
using BE;
using BLL;
using DTOs;

namespace AutoGestion.UI
{
    public partial class RealizarEntrega : UserControl
    {
        private readonly BLLVenta _bll = new BLLVenta();
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

        private void btnConfirmarEntrega_Click(object sender, EventArgs e)
        {
            if (dgvVentas.CurrentRow?.DataBoundItem is not VentaDto dto)
            {
                MessageBox.Show("Seleccione una venta para entregar.",
                                "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // 1) Marcar en BLL
                _bll.ConfirmarEntrega(dto.ID);

                // 2) Traer BE.Venta completa para el PDF
                Venta entidad = _bll.ObtenerEntidad(dto.ID);

                // 3) Generar PDF
                using var dlg = new SaveFileDialog
                {
                    Filter = "PDF (*.pdf)|*.pdf",
                    FileName = $"Comprobante_Entrega_{dto.ID}.pdf"
                };
                if (dlg.ShowDialog() != DialogResult.OK)
                    return;

                GeneradorComprobantePDF.Generar(entidad, dlg.FileName);

                MessageBox.Show("Entrega registrada y comprobante guardado.",
                                "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al confirmar entrega:\n{ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CargarVentas();
            }
        }
    }
}
