using AutoGestion.CTRL_Vista;

namespace AutoGestion.Vista
{
    public partial class RegistrarCliente : UserControl
    {
        private readonly ClienteController _ctrl = new();

        public RegistrarCliente()
        {
            InitializeComponent();
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            string dni = txtDni.Text.Trim();
            string nombre = txtNombre.Text.Trim();
            string apellido = txtApellido.Text.Trim();
            string contacto = txtContacto.Text.Trim();

            if (string.IsNullOrEmpty(dni) ||
                string.IsNullOrEmpty(nombre) ||
                string.IsNullOrEmpty(apellido) ||
                string.IsNullOrEmpty(contacto))
            {
                MessageBox.Show("Complete todos los campos.", "Validación",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var dto = _ctrl.RegistrarCliente(dni, nombre, apellido, contacto);
                MessageBox.Show($"Cliente '{dto.Nombre} {dto.Apellido}' registrado con éxito.",
                                "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiarFormulario();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al registrar cliente:\n{ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //busca el cliente y muestra datos si existe.
        private void btnBuscarDNI_Click(object sender, EventArgs e)
        {
            string dni = txtDni.Text.Trim();
            if (string.IsNullOrEmpty(dni))
            {
                MessageBox.Show("Ingrese un DNI válido.", "Validación",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var existente = _ctrl.BuscarCliente(dni);
                if (existente != null)
                {
                    MessageBox.Show("Cliente encontrado.", "Info",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtNombre.Text = existente.Nombre;
                    txtApellido.Text = existente.Apellido;
                    txtContacto.Text = existente.Contacto;
                    btnRegistrar.Enabled = false;
                }
                else
                {
                    MessageBox.Show("Cliente no encontrado. Puede registrarlo.", "Info",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarFormulario(keepDni: true);
                    btnRegistrar.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al buscar cliente:\n{ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimpiarFormulario(bool keepDni = false)
        {
            if (!keepDni) txtDni.Clear();
            txtNombre.Clear();
            txtApellido.Clear();
            txtContacto.Clear();
            btnRegistrar.Enabled = true;
        }
    }
}
