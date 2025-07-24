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
                dgvVentas.DataSource = null;
                dgvVentas.AutoGenerateColumns = false;
                dgvVentas.Columns.Clear();

                // Configura columnas si está vacía
                if (dgvVentas.Columns.Count == 0)
                {
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
                        DataPropertyName = nameof(VentaDto.Fecha),
                        HeaderText = "Fecha",
                        DefaultCellStyle = { Format = "g" }
                    });
                    dgvVentas.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        DataPropertyName = nameof(VentaDto.Estado),
                        HeaderText = "Estado"
                    });
                    dgvVentas.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        DataPropertyName = nameof(VentaDto.TipoPago),
                        HeaderText = "Forma de Pago"
                    });
                    dgvVentas.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        DataPropertyName = nameof(VentaDto.Monto),
                        HeaderText = "Monto",
                        DefaultCellStyle = { Format = "C2" }
                    });
                }

                dgvVentas.DataSource = _ventas;
                dgvVentas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvVentas.ReadOnly = true;
                dgvVentas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvVentas.ClearSelection();
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

            // 1. Ruta automática para comprobantes
            string basePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Documentos", "Comprobantes");
            if (!Directory.Exists(basePath))
                Directory.CreateDirectory(basePath);

            string archivoDestino = Path.Combine(basePath, $"Comprobante_{dto.ID}.pdf");

            try
            {
                btnConfirmarEntrega.Enabled = false;

                _bllComp.EmitirComprobantePdf(dto.ID, archivoDestino);

                MessageBox.Show(
                    $"✅ Entrega registrada y comprobante guardado automáticamente en:\n{archivoDestino}",
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Recargar la grilla para quitar las entregadas
                CargarVentas();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al generar comprobante:\n{ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnConfirmarEntrega.Enabled = true;
                CargarVentas();
            }
        }

    }
}
