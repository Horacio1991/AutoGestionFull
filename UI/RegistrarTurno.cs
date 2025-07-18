using BLL;
using DTOs;

namespace AutoGestion.UI
{
    public partial class RegistrarTurno : UserControl
    {
        private readonly BLLTurno _bllTurno = new BLLTurno();
        private readonly BLLVehiculo _bllVehiculo = new BLLVehiculo();

        public RegistrarTurno()
        {
            InitializeComponent();

            dtpFecha.Format = DateTimePickerFormat.Short;
            dtpHora.Format = DateTimePickerFormat.Time;
            dtpHora.ShowUpDown = true;
            this.Load += RegistrarTurno_Load;
            dgvVehiculos.CellClick += dgvVehiculos_CellClick;
        }

        private void RegistrarTurno_Load(object sender, EventArgs e)
        {
            CargarVehiculos();
        }

        private void CargarVehiculos()
        {
            try
            {
                var lista = _bllVehiculo.ObtenerVehiculosDisponiblesDto()
                    .Select(v => new { v.ID, v.Dominio, v.Marca, v.Modelo })
                    .ToList();

                dgvVehiculos.DataSource = lista;
                dgvVehiculos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvVehiculos.ReadOnly = true;
                dgvVehiculos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvVehiculos.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar vehículos:\n{ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvVehiculos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            txtDominio.Text = dgvVehiculos.Rows[e.RowIndex].Cells["Dominio"].Value?.ToString();
        }

        private void dtpHora_ValueChanged_1(object sender, EventArgs e)
        {
            var hora = dtpHora.Value;
            int minutos = hora.Minute >= 30 ? 30 : 0;
            dtpHora.Value = new DateTime(
                hora.Year, hora.Month, hora.Day,
                hora.Hour, minutos, 0
            );
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            string dni = txtDniCliente.Text.Trim();
            string dominio = txtDominio.Text.Trim();

            // --- VALIDACIONES ---
            if (string.IsNullOrEmpty(dni))
            {
                MessageBox.Show("Ingrese el DNI del cliente.", "Validación",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDniCliente.Focus();
                return;
            }
            if (string.IsNullOrEmpty(dominio))
            {
                MessageBox.Show("Seleccione un vehículo disponible.", "Validación",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDominio.Focus();
                return;
            }

            var input = new TurnoInputDto
            {
                DniCliente = dni,
                DominioVehiculo = dominio,
                Fecha = dtpFecha.Value.Date,
                Hora = dtpHora.Value.TimeOfDay
            };

            try
            {
                string error;
                if (!_bllTurno.RegistrarTurno(input, out error))
                {
                    MessageBox.Show(error, "No se pudo registrar turno",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                MessageBox.Show("Turno registrado correctamente.", "Éxito",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtDniCliente.Clear();
                txtDominio.Clear();
                CargarVehiculos();
            }
            catch (ApplicationException aex)
            {
                MessageBox.Show(aex.Message, "No se pudo registrar turno",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error inesperado:\n{ex.Message}", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
