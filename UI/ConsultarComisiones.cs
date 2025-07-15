using AutoGestion.CTRL_Vista;
using AutoGestion.DTOs;
using AutoGestion.Servicios;

namespace AutoGestion.Vista
{
    public partial class ConsultarComisiones : UserControl
    {
        private readonly ComisionController _ctrl = new ComisionController();

        private List<ComisionListDto> _comisiones;

        public ConsultarComisiones()
        {
            InitializeComponent();
            InicializarFiltros();
            CargarComisiones();
        }

        // Configura valores por defecto en los controles de filtro.
        private void InicializarFiltros()
        {
            try
            {
                txtVendedor.Text = Sesion.UsuarioActual?.Nombre ?? "";
                cmbEstado.Items.Clear();
                cmbEstado.Items.AddRange(new[] { "Aprobada", "Rechazada" });
                cmbEstado.SelectedIndex = 0;

                dtpDesde.Value = DateTime.Today.AddMonths(-1);
                dtpHasta.Value = DateTime.Today;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al inicializar filtros:\n{ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Lee todas las comisiones para el usuario y estado actuales y las muestra.
        private void CargarComisiones()
        {
            try
            {
                int vendedorId = Sesion.UsuarioActual?.ID ?? 0;
                string estado = cmbEstado.SelectedItem?.ToString() ?? "Aprobada";
                DateTime desde = dtpDesde.Value.Date;
                DateTime hasta = dtpHasta.Value.Date;

                
                _comisiones = _ctrl.ObtenerComisiones(vendedorId, estado, desde, hasta);
                dgvComisiones.DataSource = null;
                dgvComisiones.DataSource = _comisiones
                    .Select(c => new
                    {
                        c.ID,
                        c.Fecha,
                        c.Cliente,
                        c.Vehiculo,
                        c.Monto,
                        c.Estado
                    })
                    .ToList(); 

                dgvComisiones.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvComisiones.ReadOnly = true;
                dgvComisiones.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar comisiones:\n{ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Botón FILTRAR: vuelve a cargar la grilla con nuevos parámetros.
        private void btnFiltrar_Click_1(object sender, EventArgs e)
        {
            CargarComisiones();
        }

        // Botón DETALLE: muestra ventana con información de la comisión seleccionada.
        private void btnDetalle_Click(object sender, EventArgs e)
        {
            if (dgvComisiones.CurrentRow?.DataBoundItem == null)
            {
                MessageBox.Show("Seleccione una comisión del listado.",
                                "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int id = Convert.ToInt32(dgvComisiones.CurrentRow.Cells["ID"].Value);
                var com = _comisiones.FirstOrDefault(c => c.ID == id);
                if (com == null)
                {
                    MessageBox.Show("No se encontró la comisión seleccionada.",
                                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (com.Estado.Equals("Aprobada", StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show("✅ Comisión aprobada.", "Detalle",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show($"❌ Comisión rechazada.\nMotivo: {com.MotivoRechazo}",
                                    "Detalle", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al mostrar detalle:\n{ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
