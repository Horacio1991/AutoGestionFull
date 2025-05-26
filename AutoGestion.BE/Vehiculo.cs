using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoGestion.BE
{
    [Serializable]
    public class Vehiculo
    {
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int Año { get; set; }
        public string Color { get; set; }
        public int Km { get; set; }
        public string Dominio { get; set; }
        public string Estado { get; set; } // Ej: Disponible, Vendido
    }
}
