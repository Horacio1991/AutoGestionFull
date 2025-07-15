using System;
using System.Collections.Generic;
using System.Linq;
using BE;
using DTOs;
using Mapper;

namespace BLL
{
    public class BLLComision
    {
        private readonly MPPComision _mapper = new MPPComision();

        /// <summary>
        /// Obtiene comisiones de un vendedor, filtradas por estado y rango de fechas,
        /// y las mapea a DTOs para la capa de presentación.
        /// </summary>
        public List<ComisionListDto> ObtenerComisiones(int vendedorId, string estado, DateTime desde, DateTime hasta)
        {
            var todas = _mapper.ListarTodo();

            var filtradas = todas
                .Where(c =>
                    c.Venta?.Vendedor?.ID == vendedorId
                    && string.Equals(c.Estado, estado, StringComparison.OrdinalIgnoreCase)
                    && c.Fecha.Date >= desde.Date
                    && c.Fecha.Date <= hasta.Date
                );

            // Mapeo a DTO
            return filtradas.Select(c => new ComisionListDto
            {
                ID = c.ID,
                Fecha = c.Fecha,
                Cliente = $"{c.Venta.Cliente.Nombre} {c.Venta.Cliente.Apellido}",
                Vehiculo = $"{c.Venta.Vehiculo.Marca} {c.Venta.Vehiculo.Modelo} ({c.Venta.Vehiculo.Dominio})",
                Monto = c.Monto,
                Estado = c.Estado,
                MotivoRechazo = c.MotivoRechazo
            }).ToList();
        }

        /// <summary>
        /// Registra una nueva comisión (aprobar/rechazar) usando la entidad BE.
        /// </summary>
        public void RegistrarComision(Comision comision)
        {
            _mapper.AltaComision(comision);
        }
    }
}
