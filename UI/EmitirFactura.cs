using AutoGestion.Servicios.Pdf;
using BLL;
using DTOs;

namespace AutoGestion.UI
{
    public partial class EmitirFactura : UserControl
    {
        private readonly BLLVenta _bllVenta = new BLLVenta();
        private readonly BLLFactura _bllFactura = new BLLFactura();
        private List<VentaDto> _ventasParaFacturar;

        public EmitirFactura()
        {
            InitializeComponent();
            CargarVentas();
        }

        private void CargarVentas()
        {
            _ventasParaFacturar = _bllVenta.ObtenerVentasParaFacturar();
            dgvVentas.DataSource = _ventasParaFacturar;
            dgvVentas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvVentas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvVentas.ReadOnly = true;
        }

        private void btnEmitir_Click(object sender, EventArgs e)
        {
            if (dgvVentas.CurrentRow?.DataBoundItem is not VentaDto dto)
            {
                MessageBox.Show("Seleccione una venta para emitir la factura.",
                                "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var resumen =
                $"Cliente: {dto.Cliente}\n" +
                $"Vehículo: {dto.Vehiculo}\n" +
                $"Forma de Pago: {dto.TipoPago}\n" +
                $"Precio: {dto.Monto:C2}\n" +
                $"Fecha: {dto.Fecha:dd/MM/yyyy}";
            if (MessageBox.Show(resumen + "\n\n¿Confirmar emisión de factura?",
                                "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                != DialogResult.Yes) return;

            try
            {
                var factura = _bllFactura.EmitirFactura(dto.ID);

                using var dlg = new SaveFileDialog
                {
                    Filter = "PDF (*.pdf)|*.pdf",
                    FileName = $"Factura_{factura.ID}.pdf"
                };
                if (dlg.ShowDialog() == DialogResult.OK)
                    GeneradorFacturaPDF.Generar(factura, dlg.FileName);

                MessageBox.Show("Factura emitida y guardada correctamente.",
                                "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al emitir la factura:\n{ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CargarVentas();
            }
        }
    }
}



