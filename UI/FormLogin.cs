using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BE;
using BLL;
using Servicios.Utilidades;

namespace AutoGestion.UI
{
    public partial class FormLogin : Form
    {
        private readonly BLLUsuario _bllUsuario = new BLLUsuario();
        private List<Usuario> _usuarios = new();

        public FormLogin()
        {
            InitializeComponent();
            Load += FormLogin_Load;
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            try
            {
                // Cargamos todos los usuarios activos
                _usuarios = _bllUsuario.ListarUsuarios();
                if (_usuarios.Count == 0)
                {
                    MessageBox.Show(
                        "No hay usuarios cargados en el sistema.",
                        "Advertencia",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error al cargar usuarios: {ex.Message}",
                    "Error crítico",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                Close();
            }
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            try
            {
                string username = txtUsuario.Text.Trim();
                string password = txtClave.Text.Trim();

                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                {
                    MessageBox.Show(
                        "Debe ingresar usuario y contraseña.",
                        "Validación",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                    return;
                }

                // Preparamos un objeto Usuario con la clave en claro
                var candidato = new Usuario
                {
                    Username = username,
                    Password = password
                };

                // Validamos credenciales
                if (!_bllUsuario.ValidarLogin(candidato))
                {
                    MessageBox.Show(
                        "Usuario o contraseña incorrectos.",
                        "Acceso denegado",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                // Recuperamos todos los datos del usuario (incluyendo roles)
                var usuarioValido = _bllUsuario.BuscarPorUsername(username);
                if (usuarioValido == null)
                {
                    MessageBox.Show(
                        "No se pudo recuperar la información del usuario.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }

                // Guardamos en sesión y abrimos la ventana principal
                UsuarioSesion.UsuarioActual = usuarioValido;
                var formMain = new Form1(usuarioValido);
                formMain.Show();
                Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error al autenticar: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
