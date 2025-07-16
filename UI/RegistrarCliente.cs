using BLL;
using DTOs;

namespace AutoGestion.Vista
{
    public partial class RegistrarCliente : UserControl
    {
        private readonly BLLCliente _bllCliente = new BLLCliente();

        public RegistrarCliente()
        {
            InitializeComponent();
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            var input = new ClienteInputDto
            {
                Dni = txtDni.Text.Trim(),
                Nombre = txtNombre.Text.Trim(),
                Apellido = txtApellido.Text.Trim(),
                Contacto = txtContacto.Text.Trim()
            };         

            try
            {
                // Ahora UI NO ve BE, solo DTOs y BLL
                var dto = _bllCliente.RegistrarCliente(input);
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
                var clienteBE = _bllCliente.ObtenerPorDni(dni);
                if (clienteBE != null)
                {
                    // Mapear a DTO para mostrar
                    var dto = new ClienteDto
                    {
                        ID = clienteBE.ID,
                        Dni = clienteBE.Dni,
                        Nombre = clienteBE.Nombre,
                        Apellido = clienteBE.Apellido,
                        Contacto = clienteBE.Contacto
                    };

                    MessageBox.Show("Cliente encontrado.", "Info",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtNombre.Text = dto.Nombre;
                    txtApellido.Text = dto.Apellido;
                    txtContacto.Text = dto.Contacto;
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
            if (!keepDni)
                txtDni.Clear();
            txtNombre.Clear();
            txtApellido.Clear();
            txtContacto.Clear();
            btnRegistrar.Enabled = true;
        }
    }
}
