using AutoGestion.CTRL_Vista;
using AutoGestion.DTOs;

namespace AutoGestion.Vista
{
    public partial class TasarVehiculo : UserControl
    {
        private readonly TasacionController _ctrl = new();
        private List<TasacionListDto> _ofertas;

        public TasarVehiculo()
        {
            InitializeComponent();

            cmbEstadoStock.Items.AddRange(new[] { "Disponible", "Requiere reacondicionamiento" });
            CargarOfertas();
            cmbOfertas.SelectedIndexChanged += CmbOfertas_SelectedIndexChanged_1;
        }

        // Trae las ofertas con evaluación y las carga en el combo.
        private void CargarOfertas()
        {
            try
            {
                _ofertas = _ctrl.ObtenerOfertasParaTasar();
                cmbOfertas.DataSource = null;
                cmbOfertas.DataSource = _ofertas;
                cmbOfertas.DisplayMember = nameof(TasacionListDto.VehiculoResumen);
                cmbOfertas.ValueMember = nameof(TasacionListDto.OfertaID);
                cmbOfertas.SelectedIndex = -1;

                // Limpiar campos
                txtEvaluacion.Clear();
                txtRango.Clear();
                txtValorFinal.Clear();
                cmbEstadoStock.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error al cargar ofertas para tasar:\n{ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // Al cambiar la oferta seleccionada, muestra evaluación y rango si existen.
        private void CmbOfertas_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (cmbOfertas.SelectedItem is not TasacionListDto dto)
            {
                txtEvaluacion.Clear();
                txtRango.Clear();
                return;
            }

            // 1) Mostrar evaluación técnica
            txtEvaluacion.Text =
                $"Motor: {dto.EstadoMotor}\r\n" +
                $"Carrocería: {dto.EstadoCarroceria}\r\n" +
                $"Interior: {dto.EstadoInterior}\r\n" +
                $"Doc.: {dto.EstadoDocumentacion}";

            // 2) Mostrar rango sugerido
            if (dto.RangoMin.HasValue && dto.RangoMax.HasValue)
                txtRango.Text = $"Entre {dto.RangoMin:C} y {dto.RangoMax:C}";
            else
                txtRango.Text = "Sin rango de referencia";
        }

        /// <summary>
        /// Al pulsar “Confirmar”, valida y llama al controller para registrar la tasación.
        /// </summary>
        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            // 1) Validar selección de oferta
            if (cmbOfertas.SelectedItem is not TasacionListDto dto)
            {
                MessageBox.Show(
                    "Selecciona primero una oferta.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            // 2) Validar valor final
            if (!decimal.TryParse(txtValorFinal.Text.Trim(), out var valorFinal))
            {
                MessageBox.Show(
                    "Ingresa un valor final válido.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            // 3) Validar estado de stock
            if (cmbEstadoStock.SelectedItem is not string estadoStock)
            {
                MessageBox.Show(
                    "Selecciona el estado de stock.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            try
            {
                // 4) Preparar DTO de entrada
                var input = new TasacionInputDto
                {
                    OfertaID = dto.OfertaID,
                    ValorFinal = valorFinal,
                    EstadoStock = estadoStock
                };

                // 5) Registrar tasación y actualizar stock
                _ctrl.RegistrarTasacion(input);

                MessageBox.Show(
                    "✅ Tasación registrada correctamente.",
                    "Éxito",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                // 6) Refrescar lista de ofertas
                CargarOfertas();
            }
            catch (ApplicationException aex)
            {
                // Errores de negocio
                MessageBox.Show(
                    aex.Message,
                    "No se pudo tasar",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }
            catch (Exception ex)
            {
                // Errores inesperados
                MessageBox.Show(
                    $"Error inesperado:\n{ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
    }
}
