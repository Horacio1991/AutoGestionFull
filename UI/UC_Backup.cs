using BE;
using BLL;

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

            // Tomamos al usuario de la sesión
            var usr = UsuarioSesion.UsuarioActual;
            if (usr != null)
            {
                _usuarioId = usr.Id;
                _usuarioNombre = usr.Username;
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
                    $"Backup \"{dto.Nombre}\" realizado con éxito.",
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
                    DataPropertyName = "Nombre",
                    HeaderText = "Backup",
                    Name = "colNombre"
                });
                dgvBackup.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "UsernameUsuario",
                    HeaderText = "Usuario",
                    Name = "colUsuario"
                });
                dgvBackup.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "Fecha",
                    HeaderText = "Fecha",
                    Name = "colFecha",
                    DefaultCellStyle = { Format = "g" }
                });

                dgvBackup.DataSource = lista;
                dgvBackup.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvBackup.ReadOnly = true;
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
