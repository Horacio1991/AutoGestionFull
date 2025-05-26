using AutoGestion.CTRL_Vista;
using AutoGestion.BE;
using System;
using System.Windows;
using System.Windows.Controls;

namespace AutoGestion.Vista.Controles
{
    public partial class RegistrarCliente : UserControl
    {
        private readonly ClienteController _controller = new();

        public RegistrarCliente()
        {
            InitializeComponent();
        }

        private void Registrar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string dni = txtDni.Text;
                string nombre = txtNombre.Text;
                string apellido = txtApellido.Text;
                string contacto = txtContacto.Text;

                var existente = _controller.BuscarCliente(dni);

                if (existente != null)
                {
                    MessageBox.Show($"El cliente ya existe:\n" +
                        $"{existente.Nombre} {existente.Apellido}\n" +
                        $"Contacto: {existente.Contacto}", "Cliente Encontrado");
                    return;
                }

                var nuevo = _controller.RegistrarCliente(dni, nombre, apellido, contacto);
                MessageBox.Show($"Cliente registrado con éxito:\n{nuevo.Nombre} {nuevo.Apellido}", "Éxito");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error");
            }
        }
    }
}
