using BLL;
using DTOs;

namespace AutoGestion.UI
{
    public partial class RegistrarOferta : UserControl
    {
        private readonly BLLRegistrarOferta _bllReg = new BLLRegistrarOferta();

        public RegistrarOferta()
        {
            InitializeComponent();
        }

        private void btnBuscarOferente_Click_1(object sender, EventArgs e)
        {
            string dni = txtDni.Text.Trim();
            if (string.IsNullOrEmpty(dni))
            {
                MessageBox.Show("Ingresa un DNI válido.", "Validación",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var dto = new BLLOferente().ObtenerPorDni(dni);
                if (dto == null)
                {
                    MessageBox.Show("Oferente no existe. Completa sus datos.", "Info",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtNombre.Clear();
                    txtApellido.Clear();
                    txtContacto.Clear();
                }
                else
                {
                    txtNombre.Text = dto.Nombre;
                    txtApellido.Text = dto.Apellido;
                    txtContacto.Text = dto.Contacto;
                    MessageBox.Show("Oferente encontrado.", "Info",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al buscar oferente:\n{ex.Message}", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGuardarOferta_Click(object sender, EventArgs e)
        {
            // Validaciones...
            var input = new OfertaInputDto
            {
                Oferente = new OferenteDto
                {
                    Dni = txtDni.Text.Trim(),
                    Nombre = txtNombre.Text.Trim(),
                    Apellido = txtApellido.Text.Trim(),
                    Contacto = txtContacto.Text.Trim()
                },
                Vehiculo = new VehiculoDto
                {
                    Marca = txtMarca.Text.Trim(),
                    Modelo = txtModelo.Text.Trim(),
                    Año = (int)numAnio.Value,
                    Color = txtColor.Text.Trim(),
                    Dominio = txtDominio.Text.Trim(),
                    Km = (int)numKm.Value,
                    Estado = "En Proceso" // o el estado que corresponda
                },
                FechaInspeccion = dtpFechaInspeccion.Value.Date
            };

            try
            {
                _bllReg.RegistrarOferta(input);
                MessageBox.Show("Oferta registrada correctamente.", "Éxito",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiarFormulario();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al registrar oferta:\n{ex.Message}", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimpiarFormulario()
        {
            txtDni.Clear();
            txtNombre.Clear();
            txtApellido.Clear();
            txtContacto.Clear();
            txtMarca.Clear();
            txtModelo.Clear();
            txtColor.Clear();
            txtDominio.Clear();
            numAnio.Value = numAnio.Minimum;
            numKm.Value = 0;
        }

    }
}
