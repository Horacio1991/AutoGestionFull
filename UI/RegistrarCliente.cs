using BLL;
using DTOs;
using System;
using System.Windows.Forms;

namespace AutoGestion.Vista
{
    public partial class RegistrarCliente : UserControl
    {
        private readonly BLLCliente _bllCliente = new BLLCliente();

        public RegistrarCliente()
        {
            InitializeComponent();
            btnRegistrar.Enabled = false;
            txtDni.TextChanged += (_, __) => btnRegistrar.Enabled = false;
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

            // Validación: campos obligatorios
            if (string.IsNullOrEmpty(input.Dni) || string.IsNullOrEmpty(input.Nombre)
                || string.IsNullOrEmpty(input.Apellido) || string.IsNullOrEmpty(input.Contacto))
            {
                MessageBox.Show("Complete todos los campos antes de registrar.",
                                "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validación: que no exista un cliente con ese DNI
            var yaExiste = _bllCliente.ObtenerPorDni(input.Dni);
            if (yaExiste != null)
            {
                MessageBox.Show("Ya existe un cliente registrado con ese DNI.",
                                "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var dto = _bllCliente.RegistrarCliente(input);
                MessageBox.Show($"Cliente '{dto.Nombre} {dto.Apellido}' registrado con éxito.",
                                "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiarFormulario();
                btnRegistrar.Enabled = false;
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
            // El botón registrar solo se habilita si no existe el cliente
        }
    }
}
