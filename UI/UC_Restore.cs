using BE;
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
            _usuarioId = UsuarioSesion.UsuarioActual?.Id ?? 0;
            _usuarioNombre = UsuarioSesion.UsuarioActual?.Username ?? "Desconocido";

            cargarBackups();
            btnRestaurarSeleccionado.Click += BtnRestaurarSeleccionado_Click;
            btnRestaurarSeleccionado.Click += (s, e) => cargarBackups();
        }

        private void cargarBackups()
        {
            try
            {
                var lista = _bllBackup.ObtenerBackupsDto();
                lstBackups.DataSource = lista;
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
                    "Restore realizado con éxito.",
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information
                );
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
