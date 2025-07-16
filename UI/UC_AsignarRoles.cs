using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BE.BEComposite;
using BLL;

namespace AutoGestion.UI
{
    public partial class UC_AsignarRoles : UserControl
    {
        private readonly BLLComponente _gestorComponente;
        private readonly BLLUsuario _gestorUsuario;

        public UC_AsignarRoles()
        {
            InitializeComponent();

            _gestorComponente = new BLLComponente();
            _gestorUsuario = new BLLUsuario();

            // Wire eventos
            treeViewUsuarios.AfterSelect += TreeViewUsuarios_AfterSelect;
            trvPermisosRoles.AfterSelect += TrvPermisosRoles_AfterSelect;
            trvPermisos.AfterSelect += TrvPermisos_AfterSelect;
            comboBoxMenu.SelectedIndexChanged += ComboBoxMenu_SelectedIndexChanged;

            // Botones
            btnAltaPermiso.Click += BtnAltaPermiso_Click;
            btnEliminarPermiso.Click += BtnEliminarPermiso_Click;
            btnAltaRol.Click += BtnAltaRol_Click;
            btnModificarRol.Click += BtnModificarRol_Click;
            btnEliminarRol.Click += BtnEliminarRol_Click;
            btnAsociarPermisoARol.Click += BtnAsociarPermisoARol_Click;
            btnQuitarPermisoARol.Click += BtnQuitarPermisoARol_Click;
            btnAsociarRolUsuario.Click += BtnAsociarRolUsuario_Click;
            btnDesasociarRolUsuario.Click += BtnDesasociarRolUsuario_Click;
            btnAsociarPermisoAUsuario.Click += BtnAsociarPermisoAUsuario_Click;
            btnQuitarPermisoUsuario.Click += BtnQuitarPermisoUsuario_Click;
            checkBoxPassword.CheckedChanged += CheckBoxPassword_CheckedChanged;
            btnExit.Click += BtnExit_Click;

            // Inicialización
            CargarMenusEnComboBox();
            CargarPermisos();
            DeshabilitarCamposDeUsuario();
            RefreshTreeViewUsuarios();
            RefreshTreeViewRoles();
        }

        private void DeshabilitarCamposDeUsuario()
        {
            textBoxUserId.ReadOnly = true;
            textBoxUsename.ReadOnly = true;
            textBoxUserPassword.ReadOnly = true;
            textBoxRolId.ReadOnly = true;
            textBoxPermisoId.ReadOnly = true;
        }

        private void RefreshTreeViewUsuarios()
        {
            treeViewUsuarios.Nodes.Clear();
            var root = new TreeNode("Usuarios del Sistema");
            treeViewUsuarios.Nodes.Add(root);

            foreach (var u in _gestorUsuario.ListarUsuariosDto()) // ahora devuelven DTOs
            {
                var node = new TreeNode(u.Username) { Tag = u };
                root.Nodes.Add(node);
            }
            treeViewUsuarios.ExpandAll();
        }

        private void RefreshTreeViewRoles()
        {
            trvPermisosRoles.Nodes.Clear();
            var root = new TreeNode("Roles") { Tag = "Roles" };
            trvPermisosRoles.Nodes.Add(root);

            foreach (var r in _gestorComponente.ListarComponente().OfType<BERol>())
                root.Nodes.Add(new TreeNode(r.Nombre) { Tag = r });

            trvPermisosRoles.ExpandAll();
        }

        private void CargarPermisos()
        {
            trvPermisos.Nodes.Clear();
            var root = new TreeNode("Permisos");
            trvPermisos.Nodes.Add(root);

            foreach (var p in _gestorComponente.ListarComponente().OfType<BEPermiso>())
                root.Nodes.Add(new TreeNode(p.Nombre) { Tag = p });

            trvPermisos.ExpandAll();
        }

        private void CargarMenusEnComboBox()
        {
            comboBoxMenu.Items.Clear();
            // Asumimos que tu MainForm está abierto
            var main = Application.OpenForms
                                  .OfType<Form1>()
                                  .FirstOrDefault();
            if (main != null)
            {
                foreach (ToolStripMenuItem item in main.MenuItems)
                    comboBoxMenu.Items.Add(item);
                comboBoxMenu.DisplayMember = "Text";
            }
        }

        // === Handlers de selección ===

        private void TreeViewUsuarios_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag is DTOs.UsuarioDto u)
            {
                textBoxUserId.Text = u.ID.ToString();
                textBoxUsename.Text = u.Username;
                textBoxUserPassword.Text = "(oculto)";
                trvPermisosDelUsuario.Nodes.Clear();

                var permisos = _gestorComponente.ObtenerPermisosUsuario(u.ID);
                var root = new TreeNode($"Permisos de: {u.Username}");
                trvPermisosDelUsuario.Nodes.Add(root);
                AñadirHijos(root, permisos);
                root.ExpandAll();
            }
            else
            {
                textBoxUserId.Clear();
                textBoxUsename.Clear();
                textBoxUserPassword.Clear();
                trvPermisosDelUsuario.Nodes.Clear();
            }
        }

        private void TrvPermisosRoles_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag is BERol rol)
            {
                textBoxRolId.Text = rol.Id.ToString();
                textBoxRolNombre.Text = rol.Nombre;

                trvPermisosPorRol.Nodes.Clear();
                var root = new TreeNode($"Permisos del Rol: {rol.Nombre}");
                trvPermisosPorRol.Nodes.Add(root);
                foreach (var p in rol.Hijos.OfType<BEPermiso>())
                    root.Nodes.Add(new TreeNode(p.Nombre) { Tag = p });
                root.ExpandAll();
            }
            else
            {
                textBoxRolId.Clear();
                textBoxRolNombre.Clear();
                trvPermisosPorRol.Nodes.Clear();
            }
        }

        private void TrvPermisos_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag is BEPermiso p)
            {
                textBoxPermisoId.Text = p.Id.ToString();
                textBoxPermisoNombre.Text = p.Nombre;
            }
            else
            {
                textBoxPermisoId.Clear();
                textBoxPermisoNombre.Clear();
            }
        }

        private void ComboBoxMenu_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxItem.Items.Clear();
            if (comboBoxMenu.SelectedItem is ToolStripMenuItem menu)
            {
                foreach (ToolStripItem sub in menu.DropDownItems)
                    comboBoxItem.Items.Add(sub);
                comboBoxItem.DisplayMember = "Text";
            }
        }

        // Añade recursivamente hijos de un BEComponente
        private void AñadirHijos(TreeNode nodo, IEnumerable<BEComponente> hijos)
        {
            foreach (var h in hijos)
            {
                var n = new TreeNode(h.Nombre) { Tag = h };
                nodo.Nodes.Add(n);
                if (h is BERol r)
                    AñadirHijos(n, r.Hijos);
            }
        }

        // === Botones de permisos/roles/usuario ===

        private void BtnAltaPermiso_Click(object sender, EventArgs e)
        {
            if (comboBoxMenu.SelectedItem is ToolStripMenuItem m &&
                comboBoxItem.SelectedItem is ToolStripItem it)
            {
                var nombre = $"{m.Text} - {it.Text}";
                if (_gestorComponente.CrearPermiso(nombre))
                {
                    CargarPermisos();
                    RefreshTreeViewRoles();
                }
                else
                    MessageBox.Show("Error o permiso duplicado.");
            }
        }

        private void BtnEliminarPermiso_Click(object sender, EventArgs e)
        {
            if (trvPermisos.SelectedNode?.Tag is BEPermiso p)
            {
                if (_gestorComponente.BajaPermiso(p.Id))
                {
                    CargarPermisos();
                    RefreshTreeViewRoles();
                }
            }
        }

        private void BtnAltaRol_Click(object sender, EventArgs e)
        {
            var nombre = textBoxRolNombre.Text.Trim();
            var permisos = trvPermisos.Nodes[0]
                                .Nodes
                                .Cast<TreeNode>()
                                .Where(n => n.Checked && n.Tag is BEPermiso)
                                .Select(n => (BEPermiso)n.Tag)
                                .ToList();

            if (_gestorComponente.CrearRol(nombre, permisos))
                RefreshTreeViewRoles();
        }

        private void BtnModificarRol_Click(object sender, EventArgs e)
        {
            if (trvPermisosRoles.SelectedNode?.Tag is BERol rol)
            {
                rol.Nombre = textBoxRolNombre.Text.Trim();
                var nuevos = trvPermisos.Nodes[0]
                                .Nodes
                                .Cast<TreeNode>()
                                .Where(n => n.Checked && n.Tag is BEPermiso)
                                .Select(n => (BEPermiso)n.Tag)
                                .ToList();

                if (_gestorComponente.ModificarRol(rol, nuevos))
                    RefreshTreeViewRoles();
            }
        }

        private void BtnEliminarRol_Click(object sender, EventArgs e)
        {
            if (trvPermisosRoles.SelectedNode?.Tag is BERol rol)
            {
                if (_gestorComponente.BajaRol(rol.Id))
                    RefreshTreeViewRoles();
            }
        }

        private void BtnAsociarPermisoARol_Click(object sender, EventArgs e)
        {
            if (trvPermisosRoles.SelectedNode?.Tag is BERol rol &&
                trvPermisos.SelectedNode?.Tag is BEPermiso p)
            {
                _gestorComponente.AsignarComponenteAUsuario(rol.Id, p); // método interno de BLLComponente
                TrvPermisosRoles_AfterSelect(this, null);
            }
        }

        private void BtnQuitarPermisoARol_Click(object sender, EventArgs e)
        {
            if (trvPermisosRoles.SelectedNode?.Tag is BERol rol &&
                trvPermisosPorRol.SelectedNode?.Tag is BEPermiso p)
            {
                _gestorComponente.EliminarComponenteDeUsuario(rol.Id, p);
                TrvPermisosRoles_AfterSelect(this, null);
            }
        }

        private void BtnAsociarRolUsuario_Click(object sender, EventArgs e)
        {
            if (treeViewUsuarios.SelectedNode?.Tag is DTOs.UsuarioDto u &&
                trvPermisosRoles.SelectedNode?.Tag is BERol rol)
            {
                _gestorComponente.AsignarComponenteAUsuario(u.ID, rol);
                TreeViewUsuarios_AfterSelect(this, null);
            }
        }

        private void BtnDesasociarRolUsuario_Click(object sender, EventArgs e)
        {
            if (treeViewUsuarios.SelectedNode?.Tag is DTOs.UsuarioDto u &&
                trvPermisosDelUsuario.SelectedNode?.Tag is BERol rol)
            {
                _gestorComponente.EliminarComponenteDeUsuario(u.ID, rol);
                TreeViewUsuarios_AfterSelect(this, null);
            }
        }

        private void BtnAsociarPermisoAUsuario_Click(object sender, EventArgs e)
        {
            if (treeViewUsuarios.SelectedNode?.Tag is DTOs.UsuarioDto u &&
                trvPermisos.SelectedNode?.Tag is BEPermiso p)
            {
                _gestorComponente.AsignarComponenteAUsuario(u.ID, p);
                TreeViewUsuarios_AfterSelect(this, null);
            }
        }

        private void BtnQuitarPermisoUsuario_Click(object sender, EventArgs e)
        {
            if (treeViewUsuarios.SelectedNode?.Tag is DTOs.UsuarioDto u &&
                trvPermisosDelUsuario.SelectedNode?.Tag is BEPermiso p)
            {
                _gestorComponente.EliminarComponenteDeUsuario(u.ID, p);
                TreeViewUsuarios_AfterSelect(this, null);
            }
        }

        private void CheckBoxPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (treeViewUsuarios.SelectedNode?.Tag is DTOs.UsuarioDto u)
            {
                textBoxUserPassword.UseSystemPasswordChar = !checkBoxPassword.Checked;
                // Si quieres mostrar la pass desencriptada necesitarías exponerla en DTO
            }
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            // Quizá solo limpiar o notificar al form padre
            this.Parent?.Controls.Remove(this);
        }
    }
}
