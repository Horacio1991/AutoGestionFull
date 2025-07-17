using AutoGestion.UI;
using AutoGestion.Vista;
using BLL;
using DTOs;
using Vista.UserControls.Backup;
using Vista.UserControls.Dashboard;

namespace AutoGestion
{
    public partial class Form1 : Form
    {
        private readonly UsuarioDto _usuario;
        private readonly BLLComponente _bllComponente = new BLLComponente();

        public Form1() : this(SessionManager.CurrentUser) { }

        public Form1(UsuarioDto usuario)
        {
            InitializeComponent();
            _usuario = usuario
                ?? throw new ApplicationException("Sesión inválida. Debes loguearte primero.");
            Load += Form1_Load;
        }

        public ToolStripItemCollection MenuItems => menuPrincipal.Items;

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                // Ya recibimos PermisoDto directamente de la BLL
                var permisosDto = _bllComponente.ObtenerPermisosUsuario(_usuario.ID);

                // Si es admin, dejamos todo visible
                if (_usuario.Username.Equals("admin", StringComparison.OrdinalIgnoreCase))
                    return;

                // Oculto todo por defecto
                foreach (ToolStripMenuItem menu in menuPrincipal.Items)
                    menu.Visible = false;

                // Aplico visibilidad según permisos
                AplicarPermisos(permisosDto);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error al iniciar la aplicación:\n{ex.Message}",
                    "Error crítico",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        private void AplicarPermisos(List<PermisoDto> comps)
        {
            bool TienePermiso(IEnumerable<PermisoDto> lista, string text)
            {
                foreach (var p in lista)
                {
                    if (p.Nombre.Equals(text, StringComparison.OrdinalIgnoreCase))
                        return true;
                    if (TienePermiso(p.Hijos, text))
                        return true;
                }
                return false;
            }

            foreach (ToolStripMenuItem menu in menuPrincipal.Items)
            {
                menu.Visible = TienePermiso(comps, menu.Text);
                foreach (ToolStripMenuItem sub in menu.DropDownItems.OfType<ToolStripMenuItem>())
                    sub.Visible = TienePermiso(comps, sub.Text);
            }

            // "Cerrar sesión" siempre visible
            var cerrar = menuPrincipal.Items
                .OfType<ToolStripMenuItem>()
                .SelectMany(m => m.DropDownItems.OfType<ToolStripMenuItem>())
                .FirstOrDefault(mi => mi.Name == "mnuCerrarSesion");
            if (cerrar != null) cerrar.Visible = true;
        }

        private void CargarControl(UserControl uc)
        {
            panelContenido.Controls.Clear();
            uc.Dock = DockStyle.Fill;
            panelContenido.Controls.Add(uc);
        }

        // --- Manejadores de menú ---
        private void mnuRegistrarCliente_Click_1(object sender, EventArgs e) => CargarControl(new RegistrarCliente());
        private void mnuSolicitarModelo_Click_1(object sender, EventArgs e) => CargarControl(new SolicitarModelo());
        private void mnuRealizarPago_Click_1(object sender, EventArgs e) => CargarControl(new RealizarPago());
        private void mnuAutorizarVenta_Click_1(object sender, EventArgs e) => CargarControl(new AutorizarVenta());
        private void mnuEmitirFactura_Click_1(object sender, EventArgs e) => CargarControl(new EmitirFactura());
        private void mnuRealizarEntrega_Click_1(object sender, EventArgs e) => CargarControl(new RealizarEntrega());
        private void mnuRegistrarOferta_Click_1(object sender, EventArgs e) => CargarControl(new RegistrarOferta());
        private void mnuEvaluarVehiculo_Click_1(object sender, EventArgs e) => CargarControl(new EvaluarEstado());
        private void mnuTasarVehiculo_Click_1(object sender, EventArgs e) => CargarControl(new TasarVehiculo());
        private void mnuRegistrarCompra_Click_1(object sender, EventArgs e) => CargarControl(new RegistrarDatos());
        private void registrarComisiónToolStripMenuItem_Click(object sender, EventArgs e) => CargarControl(new RegistrarComision());
        private void mnuConsultarComisiones_Click_1(object sender, EventArgs e) => CargarControl(new ConsultarComisiones());
        private void mnuRegistrarTurno_Click_1(object sender, EventArgs e) => CargarControl(new RegistrarTurno());
        private void mnuRegistrarAsistencia_Click_1(object sender, EventArgs e) => CargarControl(new RegistrarAsistencia());
        private void mnuAsignarRoles_Click(object sender, EventArgs e) => CargarControl(new UC_AsignarRoles());
        private void aBMUsToolStripMenuItem_Click(object sender, EventArgs e) => CargarControl(new ABMUsuarios());
        private void dashboardToolStripMenuItem_Click(object sender, EventArgs e) => CargarControl(new Dashboard());
        private void backupToolStripMenuItem_Click(object sender, EventArgs e) => CargarControl(new UC_Backup());
        private void restoreToolStripMenuItem_Click(object sender, EventArgs e) => CargarControl(new UC_Restore());
        private void bitacoraToolStripMenuItem_Click(object sender, EventArgs e) => CargarControl(new UC_Bitacora());

        private void mnuCerrarSesion_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show(
                "¿Desea cerrar sesión?",
                "Cerrar sesión",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            SessionManager.CurrentUser = null;
            new FormLogin().Show();
            Close();
        }
    }
}
