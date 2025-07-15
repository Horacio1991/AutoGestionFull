using AutoGestion.CTRL_Vista;
using System.Data;


namespace Vista.UserControls.Backup
{
    public partial class UC_Bitacora : UserControl
    {
        private readonly BitacoraController _ctrl = new();
        public UC_Bitacora()
        {
            InitializeComponent();
            rbTodos.Checked = true;
            CargarBitacora();
        }

        private void CargarBitacora()
        {
            try
            {
                var lista = _ctrl.ObtenerRegistros();
                if (rbSoloBackups.Checked)
                    lista = lista.Where(b => b.Detalle.Equals("backup", StringComparison.OrdinalIgnoreCase)).ToList();
                else if (rbSoloRestores.Checked)
                    lista = lista.Where(b => b.Detalle.Equals("restore", StringComparison.OrdinalIgnoreCase)).ToList();

                dgvBitacora.Rows.Clear();
                dgvBitacora.Columns.Clear();

                dgvBitacora.Columns.Add("Fecha", "Fecha");
                dgvBitacora.Columns.Add("Detalle", "Acción");
                dgvBitacora.Columns.Add("Usuario", "Usuario");

                foreach (var b in lista)
                    dgvBitacora.Rows.Add(
                        b.FechaRegistro.ToString("g"),
                        b.Detalle,
                        b.UsuarioNombre
                    );

                dgvBitacora.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvBitacora.ReadOnly = true;
                dgvBitacora.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar bitácora:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Cada vez que se cambie el filtro, se recargará la bitácora
        private void rbTodos_CheckedChanged_1(object sender, EventArgs e) => CargarBitacora();
        private void rbSoloBackups_CheckedChanged_1(object sender, EventArgs e) => CargarBitacora();
        private void rbSoloRestores_CheckedChanged_1(object sender, EventArgs e) => CargarBitacora();
        private void btnRecargarBitacora_Click(object sender, EventArgs e) => CargarBitacora();
    }

}
