using BLL;
using DTOs;
using System;
using System.Linq;
using System.Windows.Forms;

namespace AutoGestion.UI
{
    public partial class RegistrarAsistencia : UserControl
    {
        private readonly BLLTurno _bllTurno = new BLLTurno();
        private readonly BLLCliente _bllCliente = new BLLCliente();
        private readonly BLLVehiculo _bllVehiculo = new BLLVehiculo();

        public RegistrarAsistencia()
        {
            InitializeComponent();
            cmbEstado.Items.AddRange(new[] { "Asistió", "No asistió", "Pendiente" });
            btnGuardar.Click += BtnGuardar_Click;
            CargarTurnos();
        }

        private void CargarTurnos()
        {
            try
            {
                // 1) Traer BE.Turno pendientes
                var listaBE = _bllTurno.ObtenerTurnosParaAsistencia();

                // 2) Mapear a DTO, recuperando Cliente y Vehículo completos
                var listaDto = listaBE.Select(t =>
                {
                    var cli = _bllCliente.ObtenerPorId(t.Cliente.ID)
                              ?? throw new ApplicationException($"Cliente {t.Cliente.ID} no encontrado.");
                    var veh = _bllVehiculo.BuscarPorId(t.Vehiculo.ID)
                              ?? throw new ApplicationException($"Vehículo {t.Vehiculo.ID} no encontrado.");

                    return new TurnoDto
                    {
                        ID = t.ID,
                        Cliente = $"{cli.Nombre} {cli.Apellido}",
                        Vehiculo = $"{veh.Marca} {veh.Modelo} ({veh.Dominio})",
                        Fecha = t.Fecha,
                        Hora = t.Hora,
                        Asistencia = t.Asistencia,
                        Observaciones = t.Observaciones
                    };
                }).ToList();

                dgvTurnos.AutoGenerateColumns = true;
                dgvTurnos.DataSource = listaDto;

                // 4) Ocultar columnas no deseadas
                foreach (DataGridViewColumn col in dgvTurnos.Columns)
                {
                    if (col.Name is not nameof(TurnoDto.ID)
                                 and not nameof(TurnoDto.Cliente)
                                 and not nameof(TurnoDto.Vehiculo)
                                 and not nameof(TurnoDto.Fecha)
                                 and not nameof(TurnoDto.Hora)
                                 and not nameof(TurnoDto.Asistencia)
                                 and not nameof(TurnoDto.Observaciones))
                        col.Visible = false;
                }

                dgvTurnos.Columns[nameof(TurnoDto.Fecha)].DefaultCellStyle.Format = "d";
                dgvTurnos.Columns[nameof(TurnoDto.Hora)].DefaultCellStyle.Format = @"hh\:mm";
                dgvTurnos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvTurnos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvTurnos.ReadOnly = true;
                dgvTurnos.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error al cargar turnos:\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error
                );
            }
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            // 1. Validaciones de selección
            if (dgvTurnos.CurrentRow?.DataBoundItem is not TurnoDto turnoDto)
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

            int turnoId = turnoDto.ID;
            string estado = cmbEstado.SelectedItem.ToString();
            string observaciones = txtObservaciones.Text.Trim();

            try
            {
                string error;
                if (!_bllTurno.RegistrarAsistencia(turnoId, estado, observaciones, out error))
                {
                    MessageBox.Show(error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                MessageBox.Show("Asistencia registrada correctamente.", "Éxito",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Limpiar campos y refrescar
                cmbEstado.SelectedIndex = -1;
                txtObservaciones.Clear();
                dgvTurnos.ClearSelection();
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
