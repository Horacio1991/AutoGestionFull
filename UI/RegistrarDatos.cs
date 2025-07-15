using AutoGestion.CTRL_Vista;
using AutoGestion.DTOs;

namespace AutoGestion.Vista
{
    public partial class RegistrarDatos : UserControl
    {
        private readonly RegistrarDatosController _ctrl = new();
        private OfertaRegistroDto _dto;

        public RegistrarDatos()
        {
            InitializeComponent();
            cmbEstadoStock.Items.AddRange(new[] { "Disponible", "Requiere reacondicionamiento" });
        }

        // Maneja la búsqueda de la oferta por dominio.
        private void btnBuscarOferta_Click(object sender, EventArgs e)
        {
            string dominio = txtDominio.Text.Trim();
            if (string.IsNullOrEmpty(dominio))
            {
                MessageBox.Show(
                    "Por favor, ingresa un dominio de vehículo.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            try
            {
                // 1) Consultar la oferta + evaluación
                _dto = _ctrl.ObtenerOfertaPorDominio(dominio);

                if (_dto == null)
                {
                    // 2) No existe la oferta o no tiene evaluación
                    MessageBox.Show(
                        "No se encontró oferta o evaluación para ese dominio.",
                        "Información",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    LimpiarCampos();
                    return;
                }

                // 3) Mostrar la cadena de evaluación en el textbox
                txtEvaluacion.Text = _dto.EvaluacionTexto;
            }
            catch (Exception ex)
            {
                // 4) Cualquier error inesperado
                MessageBox.Show(
                    $"Error al buscar la oferta:\n{ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // Maneja el registro final del estado de stock.
        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            // Validar que haya cargado previamente una oferta válida
            if (_dto == null)
            {
                MessageBox.Show(
                    "Primero debes buscar y seleccionar una oferta válida.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            // Validar que el usuario haya elegido un nuevo estado
            if (cmbEstadoStock.SelectedIndex < 0)
            {
                MessageBox.Show(
                    "Selecciona el estado de stock deseado.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            var input = new RegistrarDatosInputDto
            {
                OfertaID = _dto.OfertaID,
                EstadoStock = cmbEstadoStock.SelectedItem.ToString()
            };

            try
            {
                // 1) Llamar al controlador para persistir cambios
                _ctrl.RegistrarDatos(input);

                // 2) Confirmación al usuario
                MessageBox.Show(
                    "Datos registrados correctamente.",
                    "Éxito",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                // 3) Limpiar vista para nueva operación
                LimpiarCampos();
            }
            catch (ApplicationException aex)
            {
                // Errores esperados lanzados desde el controller
                MessageBox.Show(
                    $"No se pudo guardar la oferta:\n{aex.Message}",
                    "Error de aplicación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }
            catch (Exception ex)
            {
                // Cualquier otra excepción
                MessageBox.Show(
                    $"Error inesperado:\n{ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // Limpia todos los campos y resetea el estado interno.
        private void LimpiarCampos()
        {
            txtDominio.Clear();
            txtEvaluacion.Clear();
            cmbEstadoStock.SelectedIndex = -1;
            _dto = null;
        }
    }
}
