using AutoGestion.CTRL_Vista;
using AutoGestion.CTRL_Vista.Modelos;
using AutoGestion.Servicios.Pdf;

namespace AutoGestion.Vista
{
    public partial class EmitirFactura : UserControl
    {
        private readonly VentaController _ventaCtrl = new();
        private readonly FacturaController _facturaCtrl = new();
        private List<VentaDto> _ventasParaFacturar;

        public EmitirFactura()
        {
            InitializeComponent();
            CargarVentas(); 
        }

        // Lee de la BLL las ventas autorizadas listas para facturar y las muestra en el grid.
        private void CargarVentas()
        {
            try
            {
                _ventasParaFacturar = _ventaCtrl.ObtenerVentasParaFacturar();
                dgvVentas.DataSource = null;
                dgvVentas.DataSource = _ventasParaFacturar;
                dgvVentas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvVentas.ReadOnly = true;
                dgvVentas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar ventas para facturar:\n{ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // marca la venta como facturada y genera el PDF en la ruta seleccionada.
        private void btnEmitir_Click_1(object sender, EventArgs e)
        {
            // 1) Validar selección
            if (dgvVentas.CurrentRow?.DataBoundItem is not VentaDto dto)
            {
                MessageBox.Show("Seleccione una venta para emitir la factura.",
                                "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2) Vista previa de datos
            string resumen =
                $"Cliente: {dto.Cliente}\n" +
                $"Vehículo: {dto.Vehiculo}\n" +
                $"Forma de Pago: {dto.TipoPago}\n" +
                $"Precio: {dto.Monto:C2}\n" +
                $"Fecha: {dto.Fecha}";
            var respuesta = MessageBox.Show(
                resumen + "\n\n¿Desea emitir esta factura?",
                "Confirmar factura",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (respuesta != DialogResult.Yes) return;

            try
            {
                // 3) Emitir factura (persistir + marcar venta)
                var factura = _facturaCtrl.EmitirFactura(dto.ID);

                // 4) Guardar PDF en disco
                using var dlg = new SaveFileDialog
                {
                    Filter = "PDF (*.pdf)|*.pdf",
                    FileName = $"Factura_{factura.ID}.pdf"
                };
                if (dlg.ShowDialog() != DialogResult.OK)
                    return;

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
                // 5) Refrescar el listado para quitar la venta ya facturada
                CargarVentas();
            }
        }
    }
}
