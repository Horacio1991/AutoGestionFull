using BLL;
using DTOs;

namespace AutoGestion.UI
{
    public partial class EmitirFactura : UserControl
    {
        private readonly BLLVenta _bllVenta = new BLLVenta();
        private readonly BLLFactura _bllFactura = new BLLFactura();
        private List<VentaDto> _ventas;

        public EmitirFactura()
        {
            InitializeComponent();
            CargarVentasParaFacturar();
        }

        private void CargarVentasParaFacturar()
        {
            dgvVentas.DataSource = _ventas;
            try
            {
                _ventas = _bllVenta.ObtenerVentasParaFacturar();

                dgvVentas.DataSource = null;
                dgvVentas.AutoGenerateColumns = false;
                dgvVentas.Columns.Clear();

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
                    DataPropertyName = nameof(VentaDto.TipoPago),
                    HeaderText = "Forma Pago"
                });
                dgvVentas.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = nameof(VentaDto.Monto),
                    HeaderText = "Monto",
                    DefaultCellStyle = { Format = "C2" }
                });
                dgvVentas.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = nameof(VentaDto.Fecha),
                    HeaderText = "Fecha",
                    DefaultCellStyle = { Format = "g" }
                });

                dgvVentas.DataSource = _ventas;
                dgvVentas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvVentas.ReadOnly = true;
                dgvVentas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvVentas.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar ventas para facturar:\n{ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // En AutoGestion.Vista.EmitirFactura.cs, al confirmar:

        private void btnEmitir_Click_1(object sender, EventArgs e)
        {
            if (!(dgvVentas.CurrentRow?.DataBoundItem is VentaDto dto)) return;

            using (var dlg = new SaveFileDialog { Filter = "PDF|*.pdf", FileName = $"Factura_{dto.ID}.pdf" })
            {
                if (dlg.ShowDialog() != DialogResult.OK) return;

                try
                {
                    var facturaDto = _bllFactura.EmitirFactura(dto.ID, dlg.FileName);
                    MessageBox.Show(
                        $"✅ Factura emitida\n" +
                        $"Cliente:  {facturaDto.Cliente}\n" +
                        $"Vehículo: {facturaDto.Vehiculo}\n" +
                        $"Total:    {facturaDto.Precio:C2}\n" +
                        $"Fecha:    {facturaDto.Fecha:dd/MM/yyyy}",
                        "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al emitir factura:\n{ex.Message}",
                                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            CargarVentasParaFacturar();
        }

    }
}
