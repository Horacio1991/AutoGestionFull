using BLL;
using DTOs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace AutoGestion.UI
{
    public partial class EvaluarEstado : UserControl
    {
        private readonly BLLEvaluacionTecnica _bll = new BLLEvaluacionTecnica();
        private List<OfertaListDto> _ofertas;

        public EvaluarEstado()
        {
            InitializeComponent();
            CargarOfertas();
        }

        private void CargarOfertas()
        {
            try
            {
                _ofertas = _bll.ObtenerOfertasParaEvaluar();
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
            // Cada vez que cambie la fecha, reaplico el filtro
            var fecha = dtpFiltroFecha.Value.Date;
            var filtradas = _ofertas
                .Where(o => o.FechaInspeccion.Date == fecha)
                .ToList();

            if (!filtradas.Any())
            {
                MessageBox.Show("No hay ofertas en esa fecha.", "Información",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            cmbOfertas.DataSource = filtradas;
            cmbOfertas.SelectedIndex = -1;
        }

        private void btnFiltrarFecha_Click(object sender, EventArgs e)
        {
            // Si prefieres un botón en lugar de filtrar al cambiar la fecha:
            dtpFiltroFecha_ValueChanged(sender, e);
        }

        private void btnGuardar_Click_1(object sender, EventArgs e)
        {
            if (!(cmbOfertas.SelectedValue is int ofertaId))
            {
                MessageBox.Show("Seleccione una oferta.", "Validación",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
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

        private void LimpiarFormulario()
        {
            txtMotor.Clear();
            txtCarroceria.Clear();
            txtInterior.Clear();
            txtDocumentacion.Clear();
            txtObservaciones.Clear();
            cmbOfertas.SelectedIndex = -1;
            dtpFiltroFecha.Value = DateTime.Today;
        }

      
    }

}
