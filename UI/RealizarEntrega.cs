using AutoGestion.CTRL_Vista.Modelos;
using AutoGestion.CTRL_Vista;
using AutoGestion.Servicios.Pdf;

namespace AutoGestion.Vista
{
    public partial class RealizarEntrega : UserControl
    {
        private readonly EntregaController _ctrl = new();
        private List<VentaDto> _ventas;

        public RealizarEntrega()
        {
            InitializeComponent();
            CargarVentas(); 
        }

        // Lee las ventas facturadas y muestra los DTOs en el DataGridView.
        private void CargarVentas()
        {
            try
            {
                _ventas = _ctrl.ObtenerVentasParaEntrega(); //Que son las ventas facturadas
                dgvVentas.DataSource = null;
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

        // marca la venta como entregada y genera el PDF de comprobante.
        private void btnConfirmarEntrega_Click_1(object sender, EventArgs e)
        {
            if (dgvVentas.CurrentRow?.DataBoundItem is not VentaDto dto)
            {
                MessageBox.Show("Seleccione una venta para entregar.",
                                "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // 1) Marcar como entregada en BLL
                _ctrl.ConfirmarEntrega(dto.ID);

                // 2) Recuperar entidad completa para el PDF
                var ventaEntity = _ctrl.ObtenerEntidad(dto.ID);

                // 3) Pedir ruta y generar PDF
                using var dlg = new SaveFileDialog
                {
                    Filter = "PDF (*.pdf)|*.pdf",
                    FileName = $"Comprobante_Entrega_{dto.ID}.pdf"
                };
                if (dlg.ShowDialog() != DialogResult.OK)
                    return;

                GeneradorComprobantePDF.Generar(ventaEntity, dlg.FileName);

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
                // 4) Refrescar la lista para eliminar la venta ya entregada
                CargarVentas();
            }
        }
    }
}
