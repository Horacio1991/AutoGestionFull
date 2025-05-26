using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoGestion.BE;
using AutoGestion.DAO;


namespace AutoGestion.BLL
{
    public class VehiculoBLL
    {
        private readonly XmlRepository<Vehiculo> _repo = new("vehiculos.xml");

        public List<Vehiculo> BuscarVehiculosPorModelo(string modelo)
        {
            return _repo.ObtenerTodos()
                        .Where(v => v.Modelo.ToLower() == modelo.ToLower() && v.Estado == "Disponible")
                        .ToList();
        }

        public List<Vehiculo> BuscarVehiculosSimilares(string modelo)
        {
            var todos = _repo.ObtenerTodos();
            var palabra = modelo.Split(' ').FirstOrDefault()?.ToLower() ?? "";

            return todos
                .Where(v => v.Modelo.ToLower().Contains(palabra) && v.Estado == "Disponible")
                .Take(5)
                .ToList();
        }
    }
}
