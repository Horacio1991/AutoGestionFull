using AutoGestion.CTRL_Vista;
using AutoGestion.DTOs;

namespace AutoGestion.Vista
{
    public partial class RegistrarTurno : UserControl
    {
        private readonly RegistrarTurnoController _ctrl = new();
        private TurnoInputDto _inputDto;

        public RegistrarTurno()
        {
            InitializeComponent();
            dtpFecha.Format = DateTimePickerFormat.Short;
            dtpHora.Format = DateTimePickerFormat.Time;
            dtpHora.ShowUpDown = true;

            Load += RegistrarTurno_Load_1;
        }

        private void RegistrarTurno_Load_1(object sender, EventArgs e)
        {
            CargarVehiculos();
        }

        // Pide al controller la lista de vehículos y la muestra en el grid.
        private void CargarVehiculos()
        {
            try
            {
                var lista = _ctrl.ObtenerVehiculosParaTurno();
                dgvVehiculos.DataSource = lista
                    .Select(v => new { v.ID, v.Dominio, v.Marca, v.Modelo })
                    .ToList();

                dgvVehiculos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvVehiculos.ReadOnly = true;
                dgvVehiculos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvVehiculos.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error al cargar vehículos:\n{ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // cuando se selecciona una fila muestra el dominio al textbox.
        private void dgvVehiculos_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            txtDominio.Text = dgvVehiculos.Rows[e.RowIndex].Cells["Dominio"].Value?.ToString();
        }

        // Asegura que la hora sea en bloques de 30 minutos.
        private void dtpHora_ValueChanged_1(object sender, EventArgs e)
        {
            var hora = dtpHora.Value;
            int minutos = hora.Minute >= 30 ? 30 : 0; //Si es mayor o igual a 30, redondea a 30 minutos, sino a 0.
            dtpHora.Value = new DateTime(
                hora.Year, hora.Month, hora.Day,
                hora.Hour, minutos, 0
            );
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            string dni = txtDniCliente.Text.Trim();
            string dominio = txtDominio.Text.Trim();
            if (string.IsNullOrEmpty(dni) || string.IsNullOrEmpty(dominio))
            {
                MessageBox.Show(
                    "Ingresa DNI del cliente y dominio del vehículo.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            // Construir DTO de entrada
            _inputDto = new TurnoInputDto
            {
                DniCliente = dni,
                DominioVehiculo = dominio,
                Fecha = dtpFecha.Value.Date,
                Hora = dtpHora.Value.TimeOfDay
            };

            try
            {
                _ctrl.RegistrarTurno(_inputDto);

                MessageBox.Show(
                    "Turno registrado correctamente.",
                    "Éxito",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                txtDniCliente.Clear();
                txtDominio.Clear();
                CargarVehiculos();
            }
            catch (ApplicationException aex)
            {
                // Errores de validación en BLL/Controller
                MessageBox.Show(
                    aex.Message,
                    "No se pudo registrar turno",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }
            catch (Exception ex)
            {
                // Otros errores
                MessageBox.Show(
                    $"Error inesperado:\n{ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
    }
}
