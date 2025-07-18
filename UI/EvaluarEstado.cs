using BLL;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace AutoGestion.UI
{
    public partial class EvaluarEstado : UserControl
    {
        private readonly BLLEvaluacionTecnica _bll = new BLLEvaluacionTecnica();
        private List<OfertaListDto> _ofertas = new();

        public EvaluarEstado()
        {
            InitializeComponent();
            CargarOfertas();
        }

        private void CargarOfertas()
        {
            try
            {
                _ofertas = _bll.ObtenerOfertasParaEvaluar() ?? new List<OfertaListDto>();
                cmbOfertas.DataSource = null;
                cmbOfertas.DataSource = _ofertas;
                cmbOfertas.DisplayMember = nameof(OfertaListDto.DisplayTexto);
                cmbOfertas.ValueMember = nameof(OfertaListDto.ID);
                cmbOfertas.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar ofertas:\n{ex.Message}", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dtpFiltroFecha_ValueChanged(object sender, EventArgs e)
        {
            // Validación por si _ofertas está vacía
            if (_ofertas == null || _ofertas.Count == 0)
            {
                cmbOfertas.DataSource = null;
                return;
            }

            var fecha = dtpFiltroFecha.Value.Date;
            var filtradas = _ofertas
                .Where(o => o.FechaInspeccion.Date == fecha)
                .ToList();

            cmbOfertas.DataSource = null;
            cmbOfertas.DataSource = filtradas;
            cmbOfertas.DisplayMember = nameof(OfertaListDto.DisplayTexto);
            cmbOfertas.ValueMember = nameof(OfertaListDto.ID);
            cmbOfertas.SelectedIndex = -1;

            LimpiarCamposTecnicos();

            if (!filtradas.Any())
            {
                MessageBox.Show("No hay ofertas en esa fecha.", "Información",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnFiltrarFecha_Click(object sender, EventArgs e)
        {
            dtpFiltroFecha_ValueChanged(sender, e);
        }

        private void btnGuardar_Click_1(object sender, EventArgs e)
        {
            // Manejo seguro del SelectedValue (puede ser null o no int)
            int ofertaId = -1;
            if (!(cmbOfertas.SelectedValue is int id) && !int.TryParse(cmbOfertas.SelectedValue?.ToString(), out id))
            {
                MessageBox.Show("Seleccione una oferta.", "Validación",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            ofertaId = id;

            if (string.IsNullOrWhiteSpace(txtMotor.Text) ||
                string.IsNullOrWhiteSpace(txtCarroceria.Text) ||
                string.IsNullOrWhiteSpace(txtInterior.Text) ||
                string.IsNullOrWhiteSpace(txtDocumentacion.Text))
            {
                MessageBox.Show("Complete todos los campos técnicos.", "Validación",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var dto = new EvaluacionInputDto
            {
                OfertaID = ofertaId,
                EstadoMotor = txtMotor.Text.Trim(),
                EstadoCarroceria = txtCarroceria.Text.Trim(),
                EstadoInterior = txtInterior.Text.Trim(),
                EstadoDocumentacion = txtDocumentacion.Text.Trim(),
                Observaciones = txtObservaciones.Text.Trim()
            };

            try
            {
                _bll.RegistrarEvaluacion(dto);
                MessageBox.Show("Evaluación registrada correctamente.", "Éxito",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiarFormulario();
                CargarOfertas();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar evaluación:\n{ex.Message}", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimpiarCamposTecnicos()
        {
            txtMotor.Clear();
            txtCarroceria.Clear();
            txtInterior.Clear();
            txtDocumentacion.Clear();
            txtObservaciones.Clear();
        }

        private void LimpiarFormulario()
        {
            LimpiarCamposTecnicos();
            cmbOfertas.SelectedIndex = -1;
            dtpFiltroFecha.Value = DateTime.Today;
        }
    }
}
