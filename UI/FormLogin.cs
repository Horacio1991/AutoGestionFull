using BLL;

namespace AutoGestion.UI
{
    public partial class FormLogin : Form
    {
        private readonly BLLUsuario _bllUsuario = new BLLUsuario();

        public FormLogin()
        {
            InitializeComponent();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            string username = txtUsuario.Text.Trim();
            string password = txtClave.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show(
                    "Debe ingresar usuario y contraseña.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation
                );
                return;
            }

            try
            {
                // 1) Validar credenciales
                if (!_bllUsuario.ValidarLogin(username, password))
                {
                    MessageBox.Show(
                        "Usuario o contraseña incorrectos.",
                        "Acceso denegado",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                // 2) Recuperar DTO del usuario
                var dto = _bllUsuario.ObtenerUsuarioDto(username);

                // 3) Guardar en sesión (UI)
                SessionManager.CurrentUser = dto;

                // 4) Abrir la ventana principal, pasando DTO
                var formMain = new Form1(dto);
                formMain.Show();
                Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error al autenticar:\n{ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
    }
}
