using AutoGestion.CTRL_Vista;
using AutoGestion.DTOs;

namespace AutoGestion.Vista
{
    public partial class RegistrarAsistencia : UserControl
    {
        private readonly RegistrarAsistenciaController _ctrl = new();
        private List<TurnoAsistenciaListDto> _turnos;

        public RegistrarAsistencia()
        {
            InitializeComponent();

            cmbEstado.Items.AddRange(new[] { "Asistió", "No asistió", "Pendiente" });

            CargarTurnos();
        }

        // Carga los turnos cumplidos pendientes de registrar asistencia.
        private void CargarTurnos()
        {
            try
            {
                _turnos = _ctrl.ObtenerTurnosParaAsistencia();
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

        //registra la asistencia seleccionada.
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (dgvTurnos.CurrentRow?.DataBoundItem is not TurnoAsistenciaListDto fila)
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

            var dto = new RegistrarAsistenciaInputDto
            {
                TurnoID = fila.ID,
                Estado = cmbEstado.SelectedItem.ToString(),
                Observaciones = txtObservaciones.Text.Trim()
            };

            try
            {
                _ctrl.RegistrarAsistencia(dto);
                MessageBox.Show("Asistencia registrada correctamente.", "Éxito",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Limpiar selección y recargar
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
