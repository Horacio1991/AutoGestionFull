using BLL;
using DTOs;

namespace Vista.UserControls.Backup
{
    public partial class UC_Bitacora : UserControl
    {
        private readonly BLLBitacora _bllBitacora = new BLLBitacora();

        public UC_Bitacora()
        {
            InitializeComponent();
            rbTodos.Checked = true;
            CargarBitacora();
            rbTodos.CheckedChanged += (s, e) => CargarBitacora();
            rbSoloBackups.CheckedChanged += (s, e) => CargarBitacora();
            rbSoloRestores.CheckedChanged += (s, e) => CargarBitacora();
            btnRecargarBitacora.Click += (s, e) => CargarBitacora();
        }

        private void CargarBitacora()
        {
            try
            {
                var lista = _bllBitacora.ObtenerRegistrosDto();

                if (rbSoloBackups.Checked)
                    lista = lista.Where(b => b.Detalle.Equals("backup", StringComparison.OrdinalIgnoreCase)).ToList();
                else if (rbSoloRestores.Checked)
                    lista = lista.Where(b => b.Detalle.Equals("restore", StringComparison.OrdinalIgnoreCase)).ToList();

                dgvBitacora.DataSource = null;
                dgvBitacora.AutoGenerateColumns = false;
                dgvBitacora.Columns.Clear();

                dgvBitacora.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = nameof(BitacoraDto.FechaRegistro),
                    HeaderText = "Fecha",
                    Name = "colFecha",
                    DefaultCellStyle = { Format = "g" }
                });
                dgvBitacora.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = nameof(BitacoraDto.Detalle),
                    HeaderText = "Acción",
                    Name = "colDetalle"
                });
                dgvBitacora.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = nameof(BitacoraDto.UsuarioNombre),
                    HeaderText = "Usuario",
                    Name = "colUsuario"
                });

                dgvBitacora.DataSource = lista;
                dgvBitacora.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvBitacora.ReadOnly = true;
                dgvBitacora.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error al cargar bitácora:\n{ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
    }
}
