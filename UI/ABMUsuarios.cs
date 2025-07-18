using BLL;
using DTOs;

namespace AutoGestion.UI
{
    public partial class ABMUsuarios : UserControl
    {
        private readonly BLLUsuario _bllUsuario = new BLLUsuario();

        public ABMUsuarios()
        {
            InitializeComponent();
            txtID.ReadOnly = true;
            txtClave.UseSystemPasswordChar = true;
            CargarUsuarios();
        }

        private void CargarUsuarios()
        {
            try
            {
                var usuarios = _bllUsuario.ListarUsuariosDto();
                var gridData = usuarios
                    .Select(u => new
                    {
                        u.ID,
                        u.Username,
                        Clave = _bllUsuario.ObtenerPasswordPlain(u.ID)
                    })
                    .ToList();

                dgvUsuarios.DataSource = gridData;
                dgvUsuarios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvUsuarios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvUsuarios.ReadOnly = true;
                dgvUsuarios.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar usuarios:\n{ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvUsuarios_SelectionChanged_1(object sender, EventArgs e)
        {
            if (dgvUsuarios.CurrentRow == null) return;

            txtID.Text = dgvUsuarios.CurrentRow.Cells["ID"].Value?.ToString();
            txtNombre.Text = dgvUsuarios.CurrentRow.Cells["Username"].Value?.ToString();
            txtClave.Text = dgvUsuarios.CurrentRow.Cells["Clave"].Value?.ToString();

            btnAgregar.Enabled = true;
            btnModificar.Enabled = true;
            btnEliminar.Enabled = true;
        }

        private void btnAgregar_Click_1(object sender, EventArgs e)
        {
            var username = txtNombre.Text.Trim();
            var clave = txtClave.Text.Trim();
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(clave))
            {
                MessageBox.Show("Complete todos los campos.", "Validación",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string error;
            if (!_bllUsuario.RegistrarUsuario(username, clave, out error))
            {
                MessageBox.Show($"No se pudo agregar el usuario:\n{error}", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("Usuario agregado correctamente.", "Éxito",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
            LimpiarCampos();
            CargarUsuarios();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtID.Text, out var id))
            {
                MessageBox.Show("Seleccione un usuario válido.", "Validación",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var username = txtNombre.Text.Trim();
            var clave = txtClave.Text.Trim();
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(clave))
            {
                MessageBox.Show("Complete todos los campos.", "Validación",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string error;
            if (!_bllUsuario.ModificarUsuario(id, username, clave, out error))
            {
                MessageBox.Show($"No se pudo modificar el usuario:\n{error}", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("Usuario modificado correctamente.", "Éxito",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
            LimpiarCampos();
            CargarUsuarios();
        }

        private void btnEliminar_Click_1(object sender, EventArgs e)
        {
            if (!int.TryParse(txtID.Text, out var id))
            {
                MessageBox.Show("Seleccione un usuario válido.", "Validación",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var usuario = _bllUsuario.ListarUsuariosDto().FirstOrDefault(u => u.ID == id);
            if (usuario == null) return;

            if (MessageBox.Show($"¿Eliminar al usuario '{usuario.Username}'?",
                                "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                != DialogResult.Yes)
                return;

            string error;
            if (!_bllUsuario.EliminarUsuario(id, out error))
            {
                MessageBox.Show($"No se pudo eliminar el usuario:\n{error}", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("Usuario eliminado correctamente.", "Éxito",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
            LimpiarCampos();
            CargarUsuarios();
        }

        private void chkVerClave_CheckedChanged_1(object sender, EventArgs e)
        {
            txtClave.UseSystemPasswordChar = !chkVerClave.Checked;
        }

        private void LimpiarCampos()
        {
            txtID.Clear();
            txtNombre.Clear();
            txtClave.Clear();
            chkVerClave.Checked = false;
            btnAgregar.Enabled = true;
            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;
        }
    }
}
