using AutoGestion.UI;              // para SessionManager
using BLL;
using DTOs;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Vista.UserControls.Backup
{
    public partial class UC_Backup : UserControl
    {
        private readonly BLLBackup _bllBackup = new BLLBackup();
        private int _usuarioId;
        private string _usuarioNombre;

        public UC_Backup()
        {
            InitializeComponent();

            // Tomamos al usuario de la sesión (SessionManager, no la BE)
            var usr = SessionManager.CurrentUser;
            if (usr != null)
            {
                _usuarioId = usr.ID;
                _usuarioNombre = usr.Username;
            }
            else
            {
                _usuarioId = 0;
                _usuarioNombre = "(desconocido)";
            }

            btnBackup.Click += BtnBackup_Click;
            CargarHistorial();
        }

        private void BtnBackup_Click(object sender, EventArgs e)
        {
            try
            {
                var dto = _bllBackup.RealizarBackup(_usuarioId, _usuarioNombre);
                MessageBox.Show(
                    $"Backup \"{dto.Nombre}\" realizado con éxito por {_usuarioNombre}.",
                    "Backup", MessageBoxButtons.OK, MessageBoxIcon.Information
                );
                CargarHistorial();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error al hacer backup:\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error
                );
            }
        }

        private void CargarHistorial()
        {
            try
            {
                var lista = _bllBackup.ObtenerHistorialDto();

                dgvBackup.DataSource = null;
                dgvBackup.AutoGenerateColumns = false;
                dgvBackup.Columns.Clear();

                dgvBackup.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = nameof(BackupDto.Nombre),
                    HeaderText = "Backup",
                    Name = "colNombre"
                });
                dgvBackup.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = nameof(BackupDto.UsernameUsuario),
                    HeaderText = "Usuario",
                    Name = "colUsuario"
                });
                dgvBackup.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = nameof(BackupDto.Fecha),
                    HeaderText = "Fecha",
                    Name = "colFecha",
                    DefaultCellStyle = { Format = "g" }
                });

                dgvBackup.DataSource = lista;
                dgvBackup.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvBackup.ReadOnly = true;
                dgvBackup.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error al cargar historial:\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error
                );
            }
        }
    }
}
