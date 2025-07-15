using AutoGestion.Servicios.Encriptacion;
using BE;
using BLL;
using Servicios;
using System;
using System.Linq;
using System.Windows.Forms;

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
                // Ahora ListarTodo() devuelve List<Usuario>
                var usuarios = _bllUsuario.ListarTodo();
                dgvUsuarios.DataSource = usuarios
                    .Select(u => new
                    {
                        u.Id,
                        u.Username,
                        Clave = Encriptacion.DesencriptarPassword(u.Password)
                    })
                    .ToList();

                dgvUsuarios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvUsuarios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvUsuarios.ReadOnly = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar usuarios: {ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                var username = txtNombre.Text.Trim();
                var clave = txtClave.Text.Trim();

                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(clave))
                {
                    MessageBox.Show("Complete todos los campos.",
                                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (_bllUsuario.ExisteUsuario(username))
                {
                    MessageBox.Show("El usuario ya existe.",
                                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Ahora la BLL se encarga de encriptar internamente
                _bllUsuario.RegistrarUsuario(username, clave);

                MessageBox.Show("Usuario agregado correctamente.",
                                "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LimpiarCampos();
                CargarUsuarios();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al agregar usuario: {ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(txtID.Text, out int id))
                {
                    MessageBox.Show("Seleccione un usuario de la lista.",
                                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var usuario = _bllUsuario.ListarTodo().FirstOrDefault(u => u.Id == id);
                if (usuario == null) return;

                if (MessageBox.Show($"¿Eliminar al usuario '{usuario.Username}'?", "Confirmar",
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    return;

                // Ahora eliminamos por Id
                _bllUsuario.EliminarUsuario(id);

                MessageBox.Show("Usuario eliminado.",
                                "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LimpiarCampos();
                CargarUsuarios();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar usuario: {ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void chkVerClave_CheckedChanged(object sender, EventArgs e)
        {
            txtClave.UseSystemPasswordChar = !chkVerClave.Checked;
        }

        private void dgvUsuarios_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvUsuarios.CurrentRow == null) return;

            txtID.Text = dgvUsuarios.CurrentRow.Cells["Id"].Value?.ToString();
            txtNombre.Text = dgvUsuarios.CurrentRow.Cells["Username"].Value?.ToString();
            txtClave.Text = dgvUsuarios.CurrentRow.Cells["Clave"].Value?.ToString();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(txtID.Text, out int id))
                {
                    MessageBox.Show("Seleccione un usuario válido.",
                                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var username = txtNombre.Text.Trim();
                var clave = txtClave.Text.Trim();
                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(clave))
                {
                    MessageBox.Show("Nombre y contraseña no pueden estar vacíos.",
                                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Ahora la firma es (int userId, string newUsername, string newPasswordPlain)
                _bllUsuario.ModificarUsuario(id, username, clave);

                MessageBox.Show("Usuario modificado correctamente.",
                                "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LimpiarCampos();
                CargarUsuarios();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al modificar usuario: {ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimpiarCampos()
        {
            txtID.Clear();
            txtNombre.Clear();
            txtClave.Clear();
            chkVerClave.Checked = false;
        }
    }
}
