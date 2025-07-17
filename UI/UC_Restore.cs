using AutoGestion.UI;    // para SessionManager
using BLL;
using DTOs;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Vista.UserControls.Backup
{
    public partial class UC_Restore : UserControl
    {
        private readonly BLLBackup _bllBackup = new BLLBackup();
        private readonly int _usuarioId;
        private readonly string _usuarioNombre;

        public UC_Restore()
        {
            InitializeComponent();

            // Obtenemos sesión
            var usr = SessionManager.CurrentUser;
            _usuarioId = usr?.ID ?? 0;
            _usuarioNombre = usr?.Username ?? "Desconocido";

            CargarBackups();
            btnRestaurarSeleccionado.Click += BtnRestaurarSeleccionado_Click;
        }

        private void CargarBackups()
        {
            try
            {
                // Traemos todos y filtramos el historial
                var todos = _bllBackup.ObtenerBackupsDto();
                var filtrados = todos
                    .Where(b => !string.Equals(b.Nombre, "HistorialBackup", StringComparison.OrdinalIgnoreCase))
                    .ToList();

                lstBackups.DataSource = filtrados;
                lstBackups.DisplayMember = nameof(BackupDto.Nombre);
                lstBackups.ValueMember = nameof(BackupDto.Nombre);
                lstBackups.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error al cargar backups:\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error
                );
            }
        }

        private void BtnRestaurarSeleccionado_Click(object sender, EventArgs e)
        {
            if (lstBackups.SelectedItem is not BackupDto dto)
            {
                MessageBox.Show(
                    "Seleccioná un backup para restaurar.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning
                );
                return;
            }

            try
            {
                _bllBackup.RestaurarBackup(dto.Nombre, _usuarioId, _usuarioNombre);
                MessageBox.Show(
                    $"Backup \"{dto.Nombre}\" restaurado con éxito.",
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information
                );
                // refrescamos en caso de que listemos de nuevo o algo cambie
                CargarBackups();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error al restaurar:\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error
                );
            }
        }
    }
}
