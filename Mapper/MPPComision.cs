using System.Xml.Linq;
using BE;
using Servicios.Utilidades;


namespace Mapper
{
    public class MPPComision
    {
        private readonly string rutaXML = XmlPaths.BaseDatosLocal;

        private XDocument LoadOrEmpty()
        {
            try
            {
                return File.Exists(rutaXML)
                    ? XDocument.Load(rutaXML)
                    : new XDocument(new XElement("BaseDeDatosLocal"));
            }
            catch (Exception)
            {
                // Error al cargar o crear el XML
                return new XDocument(new XElement("BaseDeDatosLocal"));
            }
        }

        // Asegura que el archivo y la sección Comisiones existan.
        private IEnumerable<XElement> RootComisiones(XDocument doc)
        {
            var node = doc.Root.Element("Comisiones");
            if (node == null) yield break;
            foreach (var x in node.Elements("Comision")) yield return x;
        }

        // Devuelve todas las comisiones activas.
        public List<Comision> ListarTodo()
        {
            try
            {
                var doc = LoadOrEmpty();
                return RootComisiones(doc)
                    .Where(x => (string)x.Attribute("Active") == "true")
                    .Select(ParseComision)
                    .ToList();
            }
            catch (Exception)
            {
                return new List<Comision>();
            }
        }

        private Comision ParseComision(XElement x)
        {
            var c = new Comision
            {
                ID = (int)x.Attribute("Id"),
                Fecha = DateTime.Parse(x.Element("Fecha")?.Value ?? DateTime.Now.ToString()),
                Monto = decimal.Parse(x.Element("Monto")?.Value ?? "0"),
                Estado = x.Element("Estado")?.Value,
                MotivoRechazo = x.Element("MotivoRechazo")?.Value
            };

            var ventaId = (int?)x.Element("Venta")?.Attribute("Id");
            if (ventaId.HasValue)
                c.Venta = new MPPVenta().BuscarPorId(ventaId.Value);

            return c;
        }

        // Da de alta una comisión.
        public void AltaComision(Comision comision)
        {
            try
            {
                var doc = LoadOrEmpty();
                var root = doc.Root.Element("Comisiones")
                           ?? new XElement("Comisiones");
                if (doc.Root.Element("Comisiones") == null)
                    doc.Root.Add(root);

                int nuevoId = root.Elements("Comision")
                                  .Select(e => (int)e.Attribute("Id"))
                                  .DefaultIfEmpty(0)
                                  .Max() + 1;
                comision.ID = nuevoId;
                comision.Fecha = DateTime.Now;

                var elem = new XElement("Comision",
                    new XAttribute("Id", comision.ID),
                    new XAttribute("Active", "true"),
                    new XElement("Fecha", comision.Fecha.ToString("s")),
                    new XElement("Monto", comision.Monto),
                    new XElement("Estado", comision.Estado),
                    new XElement("MotivoRechazo", comision.MotivoRechazo ?? string.Empty),
                    new XElement("Venta", new XAttribute("Id", comision.Venta.ID))
                );
                root.Add(elem);
                doc.Save(rutaXML);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("No se pudo dar de alta la comisión. " + ex.Message, ex);
            }
        }

    }
}
