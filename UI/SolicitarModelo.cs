using BLL;
using DTOs;

namespace AutoGestion.UI
{
    public partial class SolicitarModelo : UserControl
    {
        private readonly BLLVehiculo _bllVehiculo = new BLLVehiculo();
        private List<VehiculoDto> _vehiculosDisponibles;

        public SolicitarModelo()
        {
            InitializeComponent();
            CargarTodosLosVehiculos();
        }

        private void CargarTodosLosVehiculos()
        {
            try
            {
                _vehiculosDisponibles = _bllVehiculo.ObtenerVehiculosDisponiblesDto();
                dgvResultados.DataSource = _vehiculosDisponibles;
                FormatearGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error al cargar vehículos disponibles:\n{ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string texto = txtModelo.Text.Trim();
            if (string.IsNullOrEmpty(texto))
            {
                MessageBox.Show(
                    "Por favor ingresa un modelo o marca.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            try
            {
                var exactos = _bllVehiculo.BuscarPorModeloDto(texto);
                if (exactos.Any())
                {
                    dgvResultados.DataSource = exactos;
                }
                else
                {
                    var porMarca = _bllVehiculo.BuscarPorMarcaDto(texto);
                    if (porMarca.Any())
                    {
                        MessageBox.Show(
                            "Se muestran vehículos de la marca solicitada.",
                            "Información",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );
                        dgvResultados.DataSource = porMarca;
                    }
                    else
                    {
                        MessageBox.Show(
                            "No se encontraron vehículos disponibles.",
                            "Información",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );
                        dgvResultados.DataSource = new List<VehiculoDto>();
                    }
                }
                FormatearGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error al buscar vehículos:\n{ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void btnMostrarTodos_Click(object sender, EventArgs e)
        {
            txtModelo.Clear();
            CargarTodosLosVehiculos();
        }

        private void FormatearGrid()
        {
            dgvResultados.AutoGenerateColumns = true;
            dgvResultados.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvResultados.ReadOnly = true;
            dgvResultados.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvResultados.ClearSelection();
        }
    }
}
