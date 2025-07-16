using DTOs;
using BLL;

namespace AutoGestion.UI
{
    public partial class RegistrarDatos : UserControl
    {
        private readonly BLLRegistrarDatos _bllDatos = new BLLRegistrarDatos();
        private OfertaRegistroDto _dto;

        public RegistrarDatos()
        {
            InitializeComponent();
            cmbEstadoStock.Items.AddRange(new[] { "Disponible", "Requiere reacondicionamiento" });
        }

        private void btnBuscarOferta_Click(object sender, EventArgs e)
        {
            string dominio = txtDominio.Text.Trim();
            if (string.IsNullOrEmpty(dominio))
            {
                MessageBox.Show("Ingresa un dominio.", "Validación",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                _dto = _bllDatos.ObtenerOfertaPorDominio(dominio);
                if (_dto == null)
                {
                    MessageBox.Show("No se encontró oferta o evaluación.", "Info",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarCampos();
                    return;
                }
                txtEvaluacion.Text = _dto.EvaluacionTexto;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al buscar oferta:\n{ex.Message}", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (_dto == null)
            {
                MessageBox.Show("Primero busca una oferta válida.", "Validación",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cmbEstadoStock.SelectedIndex < 0)
            {
                MessageBox.Show("Selecciona estado de stock.", "Validación",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var input = new RegistrarDatosInputDto
            {
                OfertaID = _dto.OfertaID,
                EstadoStock = cmbEstadoStock.SelectedItem.ToString()
            };

            try
            {
                _bllDatos.RegistrarDatos(input);
                MessageBox.Show("Datos registrados correctamente.", "Éxito",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiarCampos();
            }
            catch (ApplicationException aex)
            {
                MessageBox.Show($"No se pudo guardar:\n{aex.Message}", "Error de aplicación",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error inesperado:\n{ex.Message}", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimpiarCampos()
        {
            txtDominio.Clear();
            txtEvaluacion.Clear();
            cmbEstadoStock.SelectedIndex = -1;
            _dto = null;
        }
    }
}
