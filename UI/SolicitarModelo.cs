using AutoGestion.CTRL_Vista.Modelos;
using AutoGestion.CTRL_Vista;

namespace AutoGestion.Vista
{
    public partial class SolicitarModelo : UserControl
    {
        private readonly VehiculoController _ctrl = new();
        private List<VehiculoDto> _vehiculosDisponibles;

        public SolicitarModelo()
        {
            InitializeComponent();
            CargarTodosLosVehiculos();
        }

        // Trae todos los vehículos con estado "Disponible"
        private void CargarTodosLosVehiculos()
        {
            try
            {
                _vehiculosDisponibles = _ctrl.ObtenerDisponibles();
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

        // filtrar por modelo o marca.
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
                var exactos = _ctrl.BuscarPorModelo(texto);
                if (exactos.Any())
                {
                    dgvResultados.DataSource = exactos;
                }
                else
                {
                    var porMarca = _ctrl.BuscarPorMarca(texto);
                    if (porMarca.Any())
                    {
                        MessageBox.Show(
                            "Se muestran vehiculo de la Marca solicitada",
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

        // Botón para volver a mostrar todos los vehículos disponibles.
        private void btnMostrarTodos_Click(object sender, EventArgs e)
        {
            txtModelo.Clear();
            CargarTodosLosVehiculos();
        }

        // Ajustes visuales del DataGridView.
        private void FormatearGrid()
        {
            dgvResultados.AutoGenerateColumns = true; 
            dgvResultados.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvResultados.ReadOnly = true;
            dgvResultados.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }
    }
}
