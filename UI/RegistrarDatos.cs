using DTOs;
using BLL;
using System;
using System.Windows.Forms;

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
            btnRegistrar.Enabled = false;
        }

        private void btnBuscarOferta_Click(object sender, EventArgs e)
        {
            string dominio = txtDominio.Text.Trim();
            if (string.IsNullOrEmpty(dominio))
            {
                MessageBox.Show("Ingrese un dominio.", "Validación",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDominio.Focus();
                return;
            }

            try
            {
                _dto = _bllDatos.ObtenerOfertaPorDominio(dominio);
                if (_dto == null)
                {
                    MessageBox.Show("No se encontró una oferta con evaluación técnica para ese dominio.", "Información",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarCampos();
                    txtDominio.Focus();
                    return;
                }
                txtEvaluacion.Text = _dto.EvaluacionTexto;
                btnRegistrar.Enabled = true;
                cmbEstadoStock.SelectedIndex = -1;
                cmbEstadoStock.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al buscar oferta:\n{ex.Message}", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                LimpiarCampos();
            }
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (_dto == null)
            {
                MessageBox.Show("Busque primero una oferta válida.", "Validación",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDominio.Focus();
                return;
            }
            if (cmbEstadoStock.SelectedIndex < 0)
            {
                MessageBox.Show("Seleccione un estado de stock.", "Validación",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbEstadoStock.Focus();
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
                txtDominio.Focus();
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
            btnRegistrar.Enabled = false;
        }
    }
}
