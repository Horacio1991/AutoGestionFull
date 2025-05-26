using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AutoGestion.BE
{
    [Serializable]
    public class Cliente
    {
        public string Dni { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Contacto { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}
