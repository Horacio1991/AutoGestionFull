using System;
using System.Linq;
using System.Windows.Forms;
using BLL;
using DTOs;

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
            };

            treeViewUsuarios.AfterSelect += TreeViewUsuarios_AfterSelect;
            trvPermisosRoles.AfterSelect += TreeViewRoles_AfterSelect;
            trvPermisos.AfterSelect += TreeViewPermisos_AfterSelect;

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
                textBoxUserPassword.UseSystemPasswordChar = !checkBoxPassword.Checked;
                textBoxUserPassword.Text = checkBoxPassword.Checked
                    ? _bllUsuario.ObtenerPasswordPlain(u.ID)
                    : u.PasswordEncrypted;

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
        }

        private void BtnQuitarPermisoARol_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBoxRolId.Text, out var rolId) &&
                int.TryParse(textBoxPermisoId.Text, out var permId) &&
                _bllComponente.EliminarPermisoDeRol(rolId, permId))
            {
                TreeViewRoles_AfterSelect(this, new TreeViewEventArgs(trvPermisosRoles.SelectedNode));
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
            if (int.TryParse(textBoxUserId.Text, out var userId))
            {
                textBoxUserPassword.UseSystemPasswordChar = !checkBoxPassword.Checked;
                textBoxUserPassword.Text = checkBoxPassword.Checked
                    ? _bllUsuario.ObtenerPasswordPlain(userId)
                    : _bllUsuario.ListarUsuariosDto()
                                  .First(u => u.ID == userId)
                                  .PasswordEncrypted;
            }
        }
    }
}
