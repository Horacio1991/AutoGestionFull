using BLL;
using DTOs;

namespace AutoGestion.UI
{
    public partial class RealizarPago : UserControl
    {
        private readonly BLLPago _bllPago = new BLLPago();
        private ClienteDto _clienteSeleccionado;
        private VehiculoDto _vehiculoSeleccionado;

        public RealizarPago()
        {
            InitializeComponent();
            dgvVehiculos.SelectionChanged += dgvVehiculos_SelectionChanged;
            CargarVehiculosDisponibles();
            CargarTiposDePago();
        }

        private void CargarVehiculosDisponibles()
        {
            try
            {
                var lista = _bllPago.ObtenerVehiculosDisponibles();
                dgvVehiculos.DataSource = null;
                dgvVehiculos.AutoGenerateColumns = true;
                dgvVehiculos.DataSource = lista;
                dgvVehiculos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvVehiculos.ReadOnly = true;
                dgvVehiculos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvVehiculos.ClearSelection();
                _vehiculoSeleccionado = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar vehículos:\n{ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarTiposDePago()
        {
            cmbTipoPago.Items.Clear();
            cmbTipoPago.Items.AddRange(new object[]
            {
                "Efectivo",
                "Transferencia",
                "Tarjeta de Crédito",
                "Financiación"
            });
            cmbTipoPago.SelectedIndex = 0;
        }

        private void btnBuscarCliente_Click_1(object sender, EventArgs e)
        {
            var dni = txtDni.Text.Trim();
            if (string.IsNullOrEmpty(dni))
            {
                MessageBox.Show("Ingrese el DNI del cliente.", "Validación",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                _clienteSeleccionado = _bllPago.BuscarCliente(dni);
                if (_clienteSeleccionado == null)
                {
                    MessageBox.Show("Cliente no encontrado.", "Info",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtNombre.Clear(); txtApellido.Clear(); txtContacto.Clear();
                    return;
                }

                txtNombre.Text = _clienteSeleccionado.Nombre;
                txtApellido.Text = _clienteSeleccionado.Apellido;
                txtContacto.Text = _clienteSeleccionado.Contacto;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al buscar cliente:\n{ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvVehiculos_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvVehiculos.CurrentRow?.DataBoundItem is VehiculoDto vehiculo)
                _vehiculoSeleccionado = vehiculo;
            else
                _vehiculoSeleccionado = null;
        }

        private void btnRegistrarPago_Click_1(object sender, EventArgs e)
        {
            if (_clienteSeleccionado == null)
            {
                MessageBox.Show("Busque primero un cliente.", "Validación",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (_vehiculoSeleccionado == null)
            {
                MessageBox.Show("Seleccione un vehículo.", "Validación",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!decimal.TryParse(txtMonto.Text.Trim(), out var monto) || monto <= 0)
            {
                MessageBox.Show("Monto inválido.", "Validación",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int.TryParse(txtCuotas.Text.Trim(), out var cuotas);

            // Verificar sesión
            var vendedorActual = SessionManager.CurrentUser;
            if (vendedorActual == null)
            {
                MessageBox.Show("Sesión de usuario no válida.", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Llamada a la BLL
            bool ok = _bllPago.RegistrarPagoYVenta(
                clienteDni: _clienteSeleccionado.Dni,
                vehiculoDominio: _vehiculoSeleccionado.Dominio,
                tipoPago: cmbTipoPago.SelectedItem?.ToString() ?? "",
                monto: monto,
                cuotas: cuotas,
                detalles: txtOtrosDatos.Text.Trim(),
                vendedorId: vendedorActual.ID,
                vendedorNombre: vendedorActual.Username,
                out string error
            );

            if (!ok)
            {
                MessageBox.Show($"Error al registrar pago/venta:\n{error}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("✅ Pago y venta registrados correctamente.", "Éxito",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

            LimpiarFormulario();
            CargarVehiculosDisponibles();
        }

        private void LimpiarFormulario()
        {
            txtDni.Clear();
            txtNombre.Clear();
            txtApellido.Clear();
            txtContacto.Clear();
            txtMonto.Clear();
            txtCuotas.Clear();
            txtOtrosDatos.Clear();
            _clienteSeleccionado = null;
            _vehiculoSeleccionado = null;
            cmbTipoPago.SelectedIndex = 0;
            dgvVehiculos.ClearSelection();
        }
    }
}
