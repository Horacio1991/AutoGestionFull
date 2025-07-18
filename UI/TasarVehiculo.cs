using BLL;
using DTOs;

namespace AutoGestion.UI
{
    public partial class TasarVehiculo : UserControl
    {
        private readonly BLLTasacion _bllTasacion = new BLLTasacion();
        private List<OfertaParaTasacionDto> _ofertas;

        public TasarVehiculo()
        {
            InitializeComponent();
            cmbEstadoStock.Items.AddRange(new[] { "Disponible", "Requiere reacondicionamiento" });
            CargarOfertas();
            cmbOfertas.SelectedIndexChanged += CmbOfertas_SelectedIndexChanged;
        }

        private void CargarOfertas()
        {
            try
            {
                _ofertas = _bllTasacion.ObtenerOfertasParaTasacion();
                cmbOfertas.DataSource = null;
                cmbOfertas.DataSource = _ofertas;
                cmbOfertas.DisplayMember = nameof(OfertaParaTasacionDto.VehiculoResumen);
                cmbOfertas.ValueMember = nameof(OfertaParaTasacionDto.OfertaID);
                cmbOfertas.SelectedIndex = -1;

                txtEvaluacion.Clear();
                txtRango.Clear();
                txtValorFinal.Clear();
                cmbEstadoStock.SelectedIndex = -1;

                btnConfirmar.Enabled = _ofertas.Any();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar ofertas:\n{ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CmbOfertas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbOfertas.SelectedItem is not OfertaParaTasacionDto dto)
            {
                txtEvaluacion.Clear();
                txtRango.Clear();
                return;
            }

            txtEvaluacion.Text =
                $"Motor: {dto.EstadoMotor}\r\n" +
                $"Carrocería: {dto.EstadoCarroceria}\r\n" +
                $"Interior: {dto.EstadoInterior}\r\n" +
                $"Doc.: {dto.EstadoDocumentacion}";

            if (dto.RangoMin.HasValue && dto.RangoMax.HasValue)
                txtRango.Text = $"Entre {dto.RangoMin:C} y {dto.RangoMax:C}";
            else
                txtRango.Text = "Sin rango de referencia";
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (cmbOfertas.SelectedItem is not OfertaParaTasacionDto dto)
            {
                MessageBox.Show("Selecciona primero una oferta.", "Validación",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!decimal.TryParse(txtValorFinal.Text.Trim(), out var valorFinal))
            {
                MessageBox.Show("Ingresa un valor final válido.", "Validación",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cmbEstadoStock.SelectedIndex < 0)
            {
                MessageBox.Show("Selecciona el estado de stock.", "Validación",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var input = new TasacionInputDto
            {
                OfertaID = dto.OfertaID,
                ValorFinal = valorFinal,
                EstadoStock = cmbEstadoStock.SelectedItem.ToString()
            };

            try
            {
                _bllTasacion.RegistrarTasacion(input);
                MessageBox.Show("✅ Tasación registrada correctamente.", "Éxito",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarOfertas();
                // Limpiar selección y campos
                cmbOfertas.SelectedIndex = -1;
                txtEvaluacion.Clear();
                txtRango.Clear();
                txtValorFinal.Clear();
                cmbEstadoStock.SelectedIndex = -1;
            }
            catch (ApplicationException aex)
            {
                MessageBox.Show(aex.Message, "No se pudo tasar",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error inesperado:\n{ex.Message}", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
