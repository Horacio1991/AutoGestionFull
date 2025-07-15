using AutoGestion.CTRL_Vista;
using AutoGestion.Servicios;

namespace Vista.UserControls.Backup
{
    public partial class UC_Backup : UserControl
    {
        private readonly BackupController _ctrl = new();
        private int _usuarioId;
        private string _usuarioNombre;

        public UC_Backup()
        {
            InitializeComponent();

            // Usuario de la sesion actual
            var usr = Sesion.UsuarioActual;
            if (usr != null)
            {
                _usuarioId = usr.ID;
                _usuarioNombre = usr.Nombre;
            }

            btnBackup.Click += BtnBackup_Click;
            CargarHistorial();
        }

        private void BtnBackup_Click(object sender, EventArgs e)
        {
            try
            {
                var carpeta = _ctrl.RealizarBackup(_usuarioId, _usuarioNombre);
                MessageBox.Show($"Backup \"{carpeta}\" realizado con éxito.",
                                "Backup", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarHistorial();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al hacer backup:\n{ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarHistorial()
        {
            try
            {
                var lista = _ctrl.ObtenerHistorial();

                // 1) Limpiar todo
                dgvBackup.Rows.Clear();
                dgvBackup.Columns.Clear();

                // 2) Definir columnas
                dgvBackup.Columns.Add("Carpeta", "Carpeta de Backup");
                dgvBackup.Columns.Add("Usuario", "Usuario");

                // 3) Agregar filas
                foreach (var carpeta in lista)
                {
                    dgvBackup.Rows.Add(carpeta, _usuarioNombre);
                }

                dgvBackup.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvBackup.ReadOnly = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar historial:\n{ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}
