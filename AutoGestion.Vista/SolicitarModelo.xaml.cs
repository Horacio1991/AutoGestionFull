using AutoGestion.BLL;
using AutoGestion.BE;
using System.Collections.Generic;
using System.Windows;

namespace AutoGestion.Vista
{
    public partial class SolicitarModelo : Window
    {
        private VehiculoBLL _vehiculoBLL = new();

        public SolicitarModelo()
        {
            InitializeComponent();
        }

        private void Buscar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string modelo = txtModelo.Text;
                var encontrados = _vehiculoBLL.BuscarVehiculosPorModelo(modelo);

                if (encontrados.Count > 0)
                {
                    lstResultados.ItemsSource = encontrados;
                    MessageBox.Show("Vehículos encontrados.");
                }
                else
                {
                    var similares = _vehiculoBLL.BuscarVehiculosSimilares(modelo);
                    lstResultados.ItemsSource = similares;

                    if (similares.Count > 0)
                        MessageBox.Show("No hay stock exacto. Se muestran opciones similares.");
                    else
                        MessageBox.Show("No se encontraron vehículos disponibles.");
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error");
            }
        }
    }
}
