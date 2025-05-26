using AutoGestion.Vista.Controles;

using System.Windows;

namespace AutoGestion.Vista
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void AbrirRegistrarCliente(object sender, RoutedEventArgs e)
        {
            framePrincipal.Content = new RegistrarCliente();
        }

        private void AbrirBuscarVehiculo(object sender, RoutedEventArgs e)
        {
            framePrincipal.Content = new SolicitarModelo();
        }
    }
}
