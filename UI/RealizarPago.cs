using AutoGestion.CTRL_Vista;
using AutoGestion.CTRL_Vista.Modelos;
using AutoGestion.Servicios;

namespace AutoGestion.Vista
{
    public partial class RealizarPago : UserControl
    {
        private readonly PagoController _ctrlPago = new PagoController();
        private ClienteDto _clienteSeleccionado;
        private VehiculoDto _vehiculoSeleccionado;

        public RealizarPago()
        {
            InitializeComponent();
            CargarVehiculosDisponibles();
            CargarTiposDePago();
        }

        private void CargarVehiculosDisponibles()
        {
            try
            {
                var lista = _ctrlPago.ObtenerVehiculosDisponibles();
                dgvVehiculos.DataSource = lista;
                dgvVehiculos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvVehiculos.ReadOnly = true;
                dgvVehiculos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
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
            try
            {
                _clienteSeleccionado = _ctrlPago.BuscarCliente(dni);
                if (_clienteSeleccionado == null)
                {
                    MessageBox.Show("Cliente no encontrado.", "Info",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            _vehiculoSeleccionado = dgvVehiculos.CurrentRow?.DataBoundItem as VehiculoDto;
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
            if (!decimal.TryParse(txtMonto.Text.Trim(), out var monto))
            {
                MessageBox.Show("Monto inválido.", "Validación",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int.TryParse(txtCuotas.Text.Trim(), out var cuotas);

            // Llamada al Controller
            bool ok = _ctrlPago.RegistrarPagoYVenta(
                clienteDni: _clienteSeleccionado.Dni,
                vehiculoDominio: _vehiculoSeleccionado.Dominio,
                tipoPago: cmbTipoPago.SelectedItem.ToString(),
                monto: monto,
                cuotas: cuotas,
                detalles: txtOtrosDatos.Text.Trim(),
                vendedorId: Sesion.UsuarioActual.ID,
                vendedorNombre: Sesion.UsuarioActual.Nombre,
                out string error);

            if (!ok)
            {
                MessageBox.Show($"Error al registrar pago/venta:\n{error}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("✅ Pago y venta registrados correctamente.", "Éxito",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Limpiar y recargar
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
        }
    }
}
