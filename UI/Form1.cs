using AutoGestion.UI;
using BE;
using BE.BEComposite;
using BLL;

namespace AutoGestion
{
    public partial class Form1 : Form
    {
        private readonly Usuario _usuario;
        private readonly BLLComponente _bllComponente = new BLLComponente();

        public Form1()
        {
            InitializeComponent();

            // Recuperar usuario de la sesión
            _usuario = UsuarioSesion.UsuarioActual;
            if (_usuario == null)
                throw new ApplicationException("Sesión inválida. Debes loguearte primero.");

            Load += Form1_Load;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                // Cargo todos los componentes (roles y permisos) asignados al usuario
                var permisos = _bllComponente.ObtenerPermisosUsuario(_usuario.Id);
                _usuario.Rol = permisos;

                // Si es "admin", dejo todo visible
                if (_usuario.Username.Equals("admin", StringComparison.OrdinalIgnoreCase))
                    return;

                // Oculto todos los menús
                foreach (ToolStripMenuItem menu in menuPrincipal.Items)
                    menu.Visible = false;

                // Aplico permisos
                AplicarPermisos(_usuario.Rol);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error al iniciar la aplicación: {ex.Message}",
                    "Error crítico",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        private void AplicarPermisos(System.Collections.Generic.IEnumerable<BEComponente> comps)
        {
            foreach (ToolStripMenuItem menu in menuPrincipal.Items)
            {
                menu.Visible = TienePermiso(comps, menu.Text);
                foreach (ToolStripMenuItem sub in menu.DropDownItems.OfType<ToolStripMenuItem>())
                {
                    sub.Visible = TienePermiso(comps, sub.Text);
                }
            }

            // Aseguro que "Cerrar sesión" siempre esté visible
            var cerrar = menuPrincipal.Items
                .OfType<ToolStripMenuItem>()
                .SelectMany(m => m.DropDownItems.OfType<ToolStripMenuItem>())
                .FirstOrDefault(mi => mi.Name == "mnuCerrarSesion");
            if (cerrar != null) cerrar.Visible = true;
        }

        private bool TienePermiso(System.Collections.Generic.IEnumerable<BEComponente> comps, string texto)
        {
            foreach (var c in comps)
            {
                if (c.Nombre.Equals(texto, StringComparison.OrdinalIgnoreCase))
                    return true;
                if (c is BERol rol && TienePermiso(rol.Hijos, texto))
                    return true;
            }
            return false;
        }

        private void CargarControl(UserControl uc)
        {
            panelContenido.Controls.Clear();
            uc.Dock = DockStyle.Fill;
            panelContenido.Controls.Add(uc);
        }

        // --- Manejadores de menú ---
        private void mnuRegistrarCliente_Click(object sender, EventArgs e) => CargarControl(new RegistrarCliente());
        private void mnuSolicitarModelo_Click(object sender, EventArgs e) => CargarControl(new SolicitarModelo());
        private void mnuRealizarPago_Click(object sender, EventArgs e) => CargarControl(new RealizarPago());
        private void mnuAutorizarVenta_Click(object sender, EventArgs e) => CargarControl(new AutorizarVenta());
        private void mnuEmitirFactura_Click(object sender, EventArgs e) => CargarControl(new EmitirFactura());
        private void mnuRealizarEntrega_Click(object sender, EventArgs e) => CargarControl(new RealizarEntrega());
        private void mnuRegistrarOferta_Click(object sender, EventArgs e) => CargarControl(new RegistrarOferta());
        private void mnuEvaluarVehiculo_Click(object sender, EventArgs e) => CargarControl(new EvaluarEstado());
        private void mnuTasarVehiculo_Click(object sender, EventArgs e) => CargarControl(new TasarVehiculo());
        private void mnuRegistrarCompra_Click(object sender, EventArgs e) => CargarControl(new RegistrarDatos());
        private void mnuRegistrarComision_Click(object sender, EventArgs e) => CargarControl(new RegistrarComision());
        private void mnuConsultarComisiones_Click(object sender, EventArgs e) => CargarControl(new ConsultarComisiones());
        private void mnuRegistrarTurno_Click(object sender, EventArgs e) => CargarControl(new RegistrarTurno());
        private void mnuRegistrarAsistencia_Click(object sender, EventArgs e) => CargarControl(new RegistrarAsistencia());
        private void mnuAsignarRoles_Click(object sender, EventArgs e) => CargarControl(new AsignarRoles());
        private void mnuABMUsuarios_Click(object sender, EventArgs e) => CargarControl(new ABMUsuarios());
        private void mnuDashboard_Click(object sender, EventArgs e) => CargarControl(new Dashboard());
        private void mnuBackup_Click(object sender, EventArgs e) => CargarControl(new UC_Backup());
        private void mnuRestore_Click(object sender, EventArgs e) => CargarControl(new UC_Restore());
        private void mnuBitacora_Click(object sender, EventArgs e) => CargarControl(new UC_Bitacora());

        private void mnuCerrarSesion_Click(object sender, EventArgs e)
        {
            var resp = MessageBox.Show(
                "¿Desea cerrar sesión?",
                "Cerrar sesión",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (resp != DialogResult.Yes) return;

            UsuarioSesion.UsuarioActual = null;
            new FormLogin().Show();
            this.Close();
        }
    }
}
