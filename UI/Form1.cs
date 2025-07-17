using AutoGestion.UI;
using AutoGestion.Vista;
using BLL;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Vista.UserControls.Backup;
using Vista.UserControls.Dashboard;

namespace AutoGestion
{
    public partial class Form1 : Form
    {
        private readonly UsuarioDto _usuario;
        private readonly BLLComponente _bllComponente = new BLLComponente();

        public Form1(UsuarioDto usuario)
        {
            InitializeComponent();
            _usuario = usuario
                ?? throw new ApplicationException("Sesión inválida. Debes loguearte primero.");
            Load += Form1_Load;
        }

        /// <summary>
        /// Permite acceder directamente a los ítems del menú desde fuera.
        /// </summary>
        public ToolStripItemCollection MenuItems => menuPrincipal.Items;

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                // 1) Obtengo lista de permisos completos del usuario (DTOs con Nombre = "Categoría - Acción")
                var permisosDto = _bllComponente.ObtenerPermisosUsuario(_usuario.ID);

                // 2) Si es "admin", dejo todo visible y salgo
                if (_usuario.Username.Equals("admin", StringComparison.OrdinalIgnoreCase))
                    return;

                // 3) Oculto todo por defecto
                foreach (ToolStripMenuItem menu in menuPrincipal.Items)
                {
                    menu.Visible = false;
                    foreach (ToolStripMenuItem sub in menu.DropDownItems.OfType<ToolStripMenuItem>())
                        sub.Visible = false;
                }

                // 4) Reaplico visibilidad según permisos
                AplicarPermisos(permisosDto);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error al iniciar la aplicación:\n{ex.Message}",
                    "Error crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        private void AplicarPermisos(IList<PermisoDto> permisos)
        {
            // Helper: ¿existe un permiso cuya categoría coincide con menuText?
            bool TieneCategoria(string menuText)
            {
                return permisos.Any(p => p.Nombre.StartsWith(menuText + " -", StringComparison.OrdinalIgnoreCase));
            }
            // Helper: ¿existe un permiso cuya acción coincide con subText?
            bool TieneAccion(string subText)
            {
                return permisos.Any(p =>
                {
                    var partes = p.Nombre.Split(new[] { " - " }, StringSplitOptions.None);
                    return partes.Length == 2 && partes[1].Equals(subText, StringComparison.OrdinalIgnoreCase);
                });
            }

            // Recorro cada menú y submenú
            foreach (ToolStripMenuItem menu in menuPrincipal.Items)
            {
                // muestro el menú si alguna de sus acciones está permitida
                menu.Visible = TieneCategoria(menu.Text);

                foreach (ToolStripMenuItem sub in menu.DropDownItems.OfType<ToolStripMenuItem>())
                {
                    sub.Visible = TieneAccion(sub.Text);
                    // Si alguno de sus subitems está visible, que también se vea el padre
                    if (sub.Visible)
                        menu.Visible = true;
                }
            }

            // Siempre dejo visible "Cerrar sesión"
            var cerrar = menuPrincipal.Items
                .OfType<ToolStripMenuItem>()
                .SelectMany(m => m.DropDownItems.OfType<ToolStripMenuItem>())
                .FirstOrDefault(mi => mi.Name == "mnuCerrarSesion");
            if (cerrar != null) cerrar.Visible = true;
        }

        /// <summary>
        /// Carga un UserControl dentro del panel principal.
        /// </summary>
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
