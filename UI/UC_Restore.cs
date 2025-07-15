using AutoGestion.CTRL_Vista;
using AutoGestion.Servicios;


namespace Vista.UserControls.Backup
{
    public partial class UC_Restore : UserControl
    {
        private readonly RestoreController _ctrl = new();
        private readonly int _usuarioId;
        private readonly string _usuarioNombre;

        public UC_Restore()
        {
            InitializeComponent();

            // Obtenemos datos de sesión
            _usuarioId = Sesion.UsuarioActual?.ID ?? 0;
            _usuarioNombre = Sesion.UsuarioActual?.Nombre ?? "Desconocido";

            CargarBackups();
        }

        private void CargarBackups()
        {
            try
            {
                lstBackups.Items.Clear();
                var backups = _ctrl.ObtenerBackups();
                lstBackups.Items.AddRange(backups.ToArray());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar backups:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRestaurarSeleccionado_Click(object sender, EventArgs e)
        {
            if (lstBackups.SelectedItem == null)
            {
                MessageBox.Show("Seleccioná un backup para restaurar.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var seleccionado = lstBackups.SelectedItem.ToString();
            try
            {
                _ctrl.Restaurar(seleccionado, _usuarioId, _usuarioNombre);
                MessageBox.Show("Restore realizado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al restaurar:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Si quieres añadir un botón de recarga:
        private void btnRecargarBackups_Click(object sender, EventArgs e)
        {
            CargarBackups();
        }
    }
}
