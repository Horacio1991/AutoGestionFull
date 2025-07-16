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
            btnEmitir.Click += (_, __) => EmitirFacturaSeleccionada();
        }

        private void CargarVentasParaFacturar()
        {
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

        private void EmitirFacturaSeleccionada()
        {
            if (dgvVentas.CurrentRow?.DataBoundItem is not VentaDto dto)
            {
                MessageBox.Show("Seleccione una venta.", "Validación",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Emite y obtiene el DTO de factura
                var facturaDto = _bllFactura.EmitirFacturaDto(dto.ID);

                MessageBox.Show(
                    $"✅ Factura #{facturaDto.ID} emitida con éxito.\n" +
                    $"Cliente: {facturaDto.Cliente}\n" +
                    $"Vehículo: {facturaDto.Vehiculo}\n" +
                    $"Total: {facturaDto.Precio:C2}\n" +
                    $"Fecha: {facturaDto.Fecha:g}",
                    "Factura emitida",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al emitir factura:\n{ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Refrescar la grilla: esa venta ya no está en "Aprobada"
                CargarVentasParaFacturar();
            }
        }
    }
}
