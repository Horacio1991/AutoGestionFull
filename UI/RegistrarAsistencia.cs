using BLL;
using DTOs;

namespace AutoGestion.UI
{
    public partial class RegistrarAsistencia : UserControl
    {
        private readonly BLLTurno _bllTurno = new BLLTurno();
        private List<TurnoDto> _turnos;

        public RegistrarAsistencia()
        {
            InitializeComponent();
            cmbEstado.Items.AddRange(new[] { "Asistió", "No asistió", "Pendiente" });
            CargarTurnos();
        }

        private void CargarTurnos()
        {
            try
            {
                // Obtener turnos pendientes de la BLL
                var listaBE = _bllTurno.ObtenerTurnosParaAsistencia();

                // Mapear BE.Turno a DTO
                _turnos = listaBE.Select(t => new TurnoDto
                {
                    ID = t.ID,
                    Cliente = $"{t.Cliente.Nombre} {t.Cliente.Apellido}",
                    Vehiculo = $"{t.Vehiculo.Marca} {t.Vehiculo.Modelo} ({t.Vehiculo.Dominio})",
                    Fecha = t.Fecha,
                    Hora = t.Hora,
                    Asistencia = t.Asistencia,
                    Observaciones = t.Observaciones
                }).ToList();

                dgvTurnos.DataSource = _turnos;
                dgvTurnos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvTurnos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvTurnos.ReadOnly = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar turnos:\n{ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // Validaciones básicas
            var turnoDto = dgvTurnos.CurrentRow?.DataBoundItem as TurnoDto;
            if (turnoDto == null)
            {
                MessageBox.Show("Seleccione un turno.", "Validación",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cmbEstado.SelectedIndex < 0)
            {
                MessageBox.Show("Seleccione un estado de asistencia.", "Validación",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var nuevoEstado = cmbEstado.SelectedItem.ToString();
            var nuevasObs = txtObservaciones.Text.Trim();

            try
            {
                // Llamada a la BLL
                _bllTurno.RegistrarAsistencia(
                    turnoId: turnoDto.ID,
                    estado: nuevoEstado,
                    observaciones: nuevasObs
                );

                MessageBox.Show("Asistencia registrada correctamente.", "Éxito",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Limpiar y recargar
                cmbEstado.SelectedIndex = -1;
                txtObservaciones.Clear();
                CargarTurnos();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar asistencia:\n{ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
