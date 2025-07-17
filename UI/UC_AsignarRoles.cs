using BLL;
using DTOs;
using Servicios;

namespace AutoGestion.UI
{
    public partial class UC_AsignarRoles : UserControl
    {
        private readonly BLLComponente _bllComponente = new BLLComponente();
        private readonly BLLUsuario _bllUsuario = new BLLUsuario();

        public UC_AsignarRoles()
        {
            InitializeComponent();

            Load += (_, __) =>
            {
                CargarUsuarios();
                CargarRoles();
                CargarPermisosGlobales();
                CargarMenusEnComboBox();
            };

            treeViewUsuarios.AfterSelect += TreeViewUsuarios_AfterSelect;
            trvPermisosRoles.AfterSelect += TreeViewRoles_AfterSelect;
            trvPermisos.AfterSelect += TreeViewPermisos_AfterSelect;
            comboBoxMenu.SelectedIndexChanged += (s, e) => CargarItemsDelMenu();
            btnAltaPermiso.Click += BtnAltaPermiso_Click;
            btnEliminarPermiso.Click += BtnEliminarPermiso_Click;
            btnAltaRol.Click += BtnAltaRol_Click;
            btnModificarRol.Click += BtnModificarRol_Click;
            btnEliminarRol.Click += BtnEliminarRol_Click;
            btnAsociarPermisoARol.Click += BtnAsignarPermisoARol_Click;
            btnQuitarPermisoARol.Click += BtnQuitarPermisoARol_Click;
            btnAsociarRolUsuario.Click += BtnAsignarRolUsuario_Click;
            btnDesasociarRolUsuario.Click += BtnQuitarRolUsuario_Click;
            btnAsociarPermisoAUsuario.Click += BtnAsignarPermisoUsuario_Click;
            btnQuitarPermisoUsuario.Click += BtnQuitarPermisoUsuario_Click;
            checkBoxPassword.CheckedChanged += ChkMostrarPassword_CheckedChanged;
        }

        private void CargarUsuarios()
        {
            treeViewUsuarios.Nodes.Clear();
            var root = new TreeNode("Usuarios");
            treeViewUsuarios.Nodes.Add(root);
            foreach (var u in _bllUsuario.ListarUsuariosDto())
                root.Nodes.Add(new TreeNode(u.Username) { Tag = u });
            root.Expand();
        }

        private void CargarRoles()
        {
            trvPermisosRoles.Nodes.Clear();
            var root = new TreeNode("Roles");
            trvPermisosRoles.Nodes.Add(root);
            foreach (var r in _bllComponente.ObtenerRoles())
                root.Nodes.Add(new TreeNode(r.Nombre) { Tag = r });
            root.Expand();
        }

        private void CargarPermisosGlobales()
        {
            trvPermisos.Nodes.Clear();
            var root = new TreeNode("Permisos");
            trvPermisos.Nodes.Add(root);
            foreach (var p in _bllComponente.ObtenerPermisos())
                root.Nodes.Add(new TreeNode(p.Nombre) { Tag = p });
            root.Expand();
        }

        private void CargarMenusEnComboBox()
        {
            comboBoxMenu.Items.Clear();
            var main = Application.OpenForms
                                  .OfType<Form1>()
                                  .FirstOrDefault();
            if (main == null) return;

            foreach (ToolStripMenuItem menu in main.MenuItems)
                comboBoxMenu.Items.Add(menu);
            comboBoxMenu.DisplayMember = "Text";
        }

        private void CargarItemsDelMenu()
        {
            comboBoxItem.Items.Clear();
            if (comboBoxMenu.SelectedItem is ToolStripMenuItem menu)
            {
                foreach (ToolStripItem item in menu.DropDownItems.OfType<ToolStripItem>())
                    comboBoxItem.Items.Add(item);
                comboBoxItem.DisplayMember = "Text";
            }
        }


        private void TreeViewUsuarios_AfterSelect(object sender, TreeViewEventArgs e)
        {
            textBoxUserId.Text = "";
            textBoxUsename.Text = "";
            textBoxUserPassword.Text = "";
            trvPermisosDelUsuario.Nodes.Clear();

            if (e.Node.Tag is UsuarioDto u)
            {
                textBoxUserId.Text = u.ID.ToString();
                textBoxUsename.Text = u.Username;
                checkBoxPassword.Checked = false;

                // Recupero siempre el Base64 cifrado desde la BLLUsuario:
                textBoxUserPassword.UseSystemPasswordChar = false;
                textBoxUserPassword.Text =
                    _bllUsuario.ObtenerPasswordEncrypted(u.ID);

                var perms = _bllComponente.ObtenerPermisosUsuario(u.ID);
                var root = new TreeNode($"Permisos de {u.Username}");
                trvPermisosDelUsuario.Nodes.Add(root);
                foreach (var p in perms)
                    root.Nodes.Add(new TreeNode(p.Nombre) { Tag = p });
                root.ExpandAll();
            }
        }

        private void TreeViewRoles_AfterSelect(object sender, TreeViewEventArgs e)
        {
            textBoxRolId.Text = "";
            textBoxRolNombre.Text = "";
            trvPermisosPorRol.Nodes.Clear();

            if (e.Node.Tag is RolDto r)
            {
                textBoxRolId.Text = r.Id.ToString();
                textBoxRolNombre.Text = r.Nombre;
                var root = new TreeNode($"Permisos de {r.Nombre}");
                trvPermisosPorRol.Nodes.Add(root);
                foreach (var p in r.Permisos)
                    root.Nodes.Add(new TreeNode(p.Nombre) { Tag = p });
                root.ExpandAll();
            }
        }

        private void TreeViewPermisos_AfterSelect(object sender, TreeViewEventArgs e)
        {
            textBoxPermisoId.Text = "";
            textBoxPermisoNombre.Text = "";

            if (e.Node.Tag is PermisoDto p)
            {
                textBoxPermisoId.Text = p.Id.ToString();
                textBoxPermisoNombre.Text = p.Nombre;
            }
        }

        private void BtnAltaPermiso_Click(object sender, EventArgs e)
        {
            if (comboBoxMenu.SelectedItem is ToolStripMenuItem m &&
                comboBoxItem.SelectedItem is ToolStripItem it)
            {
                var nombre = $"{m.Text} - {it.Text}";
                if (_bllComponente.CrearPermiso(nombre))
                    CargarPermisosGlobales();
                else
                    MessageBox.Show("No se pudo crear permiso (duplicado?).");
            }
        }

        private void BtnEliminarPermiso_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBoxPermisoId.Text, out var id) &&
                _bllComponente.EliminarPermiso(id))
                CargarPermisosGlobales();
        }

        private void BtnAltaRol_Click(object sender, EventArgs e)
        {
            var nombre = textBoxRolNombre.Text.Trim();
            var permisosIds = trvPermisos.Nodes[0].Nodes
                .Cast<TreeNode>()
                .Where(n => n.Checked && n.Tag is PermisoDto)
                .Select(n => ((PermisoDto)n.Tag).Id)
                .ToList();

            if (_bllComponente.CrearRol(nombre, permisosIds))
                CargarRoles();
        }

        private void BtnModificarRol_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(textBoxRolId.Text, out var id)) return;
            var nombre = textBoxRolNombre.Text.Trim();
            var permisosIds = trvPermisos.Nodes[0].Nodes
                .Cast<TreeNode>()
                .Where(n => n.Checked && n.Tag is PermisoDto)
                .Select(n => ((PermisoDto)n.Tag).Id)
                .ToList();

            if (_bllComponente.ModificarRol(id, nombre, permisosIds))
                CargarRoles();
        }

        private void BtnEliminarRol_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBoxRolId.Text, out var id) &&
                _bllComponente.EliminarRol(id))
                CargarRoles();
        }

        private void BtnAsignarPermisoARol_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBoxRolId.Text, out var rolId) &&
                int.TryParse(textBoxPermisoId.Text, out var permId) &&
                _bllComponente.AsignarPermisoARol(rolId, permId))
            {
                TreeViewRoles_AfterSelect(this, new TreeViewEventArgs(trvPermisosRoles.SelectedNode));
            }

            MessageBox.Show("✅ Permiso asociado correctamente.", "Éxito",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            // recargo la lista de roles y re-selecciono el mismo nodo
            CargarRoles();
            var node = trvPermisosRoles.Nodes[0].Nodes
                             .Cast<TreeNode>()
                             .FirstOrDefault(n => ((RolDto)n.Tag).Id == rolId);
            if (node != null)
                trvPermisosRoles.SelectedNode = node;
        }

        private void BtnQuitarPermisoARol_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBoxRolId.Text, out var rolId) &&
                int.TryParse(textBoxPermisoId.Text, out var permId) &&
                _bllComponente.EliminarPermisoDeRol(rolId, permId))
            {
                TreeViewRoles_AfterSelect(this, new TreeViewEventArgs(trvPermisosRoles.SelectedNode));
                bool ok = _bllComponente.EliminarPermisoDeRol(rolId, permId);
                if (!ok)
                {
                    MessageBox.Show("No se pudo quitar el permiso.", "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                MessageBox.Show("✅ Permiso removido correctamente.", "Éxito",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);

                CargarRoles();
                var node = trvPermisosRoles.Nodes[0].Nodes
                              .Cast<TreeNode>()
                              .FirstOrDefault(n => ((RolDto)n.Tag).Id == rolId);
                if (node != null)
                    trvPermisosRoles.SelectedNode = node;

            }

        }

        private void BtnAsignarRolUsuario_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBoxUserId.Text, out var userId) &&
                int.TryParse(textBoxRolId.Text, out var rolId) &&
                _bllComponente.AsignarRolAUsuario(userId, rolId))
            {
                TreeViewUsuarios_AfterSelect(this, new TreeViewEventArgs(treeViewUsuarios.SelectedNode));
            }
        }

        private void BtnQuitarRolUsuario_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBoxUserId.Text, out var userId) &&
                int.TryParse(textBoxRolId.Text, out var rolId) &&
                _bllComponente.EliminarRolDeUsuario(userId, rolId))
            {
                TreeViewUsuarios_AfterSelect(this, new TreeViewEventArgs(treeViewUsuarios.SelectedNode));
            }
        }

        private void BtnAsignarPermisoUsuario_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBoxUserId.Text, out var userId) &&
                int.TryParse(textBoxPermisoId.Text, out var permId) &&
                _bllComponente.AsignarPermisoAUsuario(userId, permId))
            {
                TreeViewUsuarios_AfterSelect(this, new TreeViewEventArgs(treeViewUsuarios.SelectedNode));
            }
        }

        private void BtnQuitarPermisoUsuario_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBoxUserId.Text, out var userId) &&
                int.TryParse(textBoxPermisoId.Text, out var permId) &&
                _bllComponente.EliminarPermisoDeUsuario(userId, permId))
            {
                TreeViewUsuarios_AfterSelect(this, new TreeViewEventArgs(treeViewUsuarios.SelectedNode));
            }
        }

        private void ChkMostrarPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(textBoxUserId.Text, out var userId))
                return;

            if (checkBoxPassword.Checked)
            {
                // Si tildado, mostramos desencriptado
                textBoxUserPassword.UseSystemPasswordChar = false;
                textBoxUserPassword.Text =
                    Encriptacion.DesencriptarPassword(textBoxUserPassword.Text);
            }
            else
            {
                // Si destildado, volvemos al cifrado original (Base64)
                textBoxUserPassword.UseSystemPasswordChar = false;
                // Lo recuperamos del BE a través de BLLUsuario
                textBoxUserPassword.Text =
                    _bllUsuario.ObtenerPasswordEncrypted(userId);
            }
        }

        private void btnAsignarPermisosSeleccionados_Click_1(object sender, EventArgs e)
        {
            if (!int.TryParse(textBoxRolId.Text, out var rolId))
            {
                MessageBox.Show("Seleccioná primero un rol válido.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Recojo todos los permisos chequeados
            var permisosSeleccionados = new List<int>();
            foreach (TreeNode permisoNode in trvPermisos.Nodes[0].Nodes)
            {
                if (permisoNode.Checked && permisoNode.Tag is PermisoDto pd)
                    permisosSeleccionados.Add(pd.Id);
            }

            if (permisosSeleccionados.Count == 0)
            {
                MessageBox.Show("Marcá al menos un permiso para asignar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Asigno uno a uno
            var errores = new List<int>();
            foreach (var pid in permisosSeleccionados)
            {
                bool ok = _bllComponente.AsignarPermisoARol(rolId, pid);
                if (!ok) errores.Add(pid);
            }

            // Refresco la vista del rol actual
            //TreeViewRoles_AfterSelect(this, new TreeViewEventArgs(trvPermisosRoles.SelectedNode));
            // 1. Refresco la vista del rol actual
            RefrescarPermisosPorRol(rolId);

            if (errores.Count == 0)
                MessageBox.Show("✅ Todos los permisos fueron asignados correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show($"❌ No se pudieron asignar los permisos: {string.Join(", ", errores)}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void trvPermisos_AfterCheck(object sender, TreeViewEventArgs e)
        {
            // Evitamos reentradas infinitas por cambiar Checked en código:
            if (e.Action != TreeViewAction.Unknown)
                CheckAllChildNodes(e.Node, e.Node.Checked);
        }

        private void CheckAllChildNodes(TreeNode treeNode, bool isChecked)
        {
            foreach (TreeNode child in treeNode.Nodes)
            {
                child.Checked = isChecked;
                // y recursivamente
                CheckAllChildNodes(child, isChecked);
            }
        }

        private void btnQuitarPermisosSeleccionados_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(textBoxRolId.Text, out var rolId))
            {
                MessageBox.Show("Seleccioná primero un rol válido.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Recojo todos los permisos chequeados en trvPermisosPorRol
            var permisosARemover = trvPermisosPorRol.Nodes[0].Nodes
                .Cast<TreeNode>()
                .Where(n => n.Checked && n.Tag is PermisoDto)
                .Select(n => ((PermisoDto)n.Tag).Id)
                .ToList();

            if (permisosARemover.Count == 0)
            {
                MessageBox.Show("Marcá al menos un permiso para quitar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var errores = new List<int>();
            foreach (var pid in permisosARemover)
            {
                if (!_bllComponente.EliminarPermisoDeRol(rolId, pid))
                    errores.Add(pid);
            }

            // Refresco la vista
            RefrescarPermisosPorRol(rolId);

            if (errores.Count == 0)
                MessageBox.Show("✅ Todos los permisos fueron removidos correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show($"❌ No se pudieron remover los permisos: {string.Join(", ", errores)}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void RefrescarPermisosPorRol(int rolId)
        {
            // 1) recargo la lista de roles para tener el DTO actualizado
            CargarRoles();

            // 2) vuelvo a seleccionar el nodo del rol en trvPermisosRoles
            var nodoRol = trvPermisosRoles.Nodes[0].Nodes
                .Cast<TreeNode>()
                .FirstOrDefault(n => ((RolDto)n.Tag).Id == rolId);

            if (nodoRol != null)
            {
                trvPermisosRoles.SelectedNode = nodoRol;
                // disparamos a mano el AfterSelect para que rellene trvPermisosPorRol
                TreeViewRoles_AfterSelect(this, new TreeViewEventArgs(nodoRol));
            }
        }

        private void trvPermisosPorRol_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Action != TreeViewAction.Unknown)
                CheckAllChildNodes(e.Node, e.Node.Checked);
        }
    }
}
