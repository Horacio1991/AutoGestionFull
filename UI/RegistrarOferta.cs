using AutoGestion.CTRL_Vista;
using AutoGestion.CTRL_Vista.Modelos;
using AutoGestion.DTOs;

namespace AutoGestion.Vista
{
    public partial class RegistrarOferta : UserControl
    {
        private readonly OfertaController _ctrl = new();
        private OferenteDto _oferenteDto;

        public RegistrarOferta()
        {
            InitializeComponent();
        }

        private void btnBuscarOferente_Click_1(object sender, EventArgs e)
        {
            string dni = txtDni.Text.Trim();
            if (string.IsNullOrEmpty(dni))
            {
                MessageBox.Show(
                    "Por favor ingresa un DNI válido.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            try
            {
                // 1) Consultar al controller
                _oferenteDto = _ctrl.BuscarOferente(dni);

                if (_oferenteDto == null)
                {
                    // 2) No existe; limpio campos para nuevo ingreso
                    MessageBox.Show(
                        "Oferente no encontrado. Completa sus datos para registrarlo.",
                        "Información",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    txtNombre.Clear();
                    txtApellido.Clear();
                    txtContacto.Clear();
                }
                else
                {
                    // 3) Mostrar datos existentes
                    txtNombre.Text = _oferenteDto.Nombre;
                    txtApellido.Text = _oferenteDto.Apellido;
                    txtContacto.Text = _oferenteDto.Contacto;
                    MessageBox.Show(
                        "Oferente encontrado.",
                        "Información",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error al buscar oferente:\n{ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // arma DTO y se lo envia al controller para registrar oferta.
        private void btnGuardarOferta_Click(object sender, EventArgs e)
        {
            // 1) Validar DNI
            if (string.IsNullOrWhiteSpace(txtDni.Text))
            {
                MessageBox.Show("DNI es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // 2) Datos del oferente
            if (string.IsNullOrWhiteSpace(txtNombre.Text)
             || string.IsNullOrWhiteSpace(txtApellido.Text)
             || string.IsNullOrWhiteSpace(txtContacto.Text))
            {
                MessageBox.Show("Completa los datos del oferente.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // 3) Datos del vehículo
            if (string.IsNullOrWhiteSpace(txtMarca.Text)
             || string.IsNullOrWhiteSpace(txtModelo.Text)
             || string.IsNullOrWhiteSpace(txtColor.Text)
             || string.IsNullOrWhiteSpace(txtDominio.Text))
            {
                MessageBox.Show("Completa todos los datos del vehículo.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Armar el DTO de entrada
            var dto = new OfertaInputDto
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
                    Km = (int)numKm.Value
                },
                FechaInspeccion = dtpFechaInspeccion.Value.Date
            };

            try
            {
                // 4) Llamar al controller
                bool ok = _ctrl.RegistrarOferta(dto);

                // 5) Mostrar resultado
                if (ok)
                {
                    MessageBox.Show(
                        "Oferta registrada correctamente.",
                        "Éxito",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    LimpiarFormulario();
                }
                else
                {
                    MessageBox.Show(
                        "No se pudo registrar la oferta.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error al registrar oferta:\n{ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
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
            _oferenteDto = null;
        }
    }
}
