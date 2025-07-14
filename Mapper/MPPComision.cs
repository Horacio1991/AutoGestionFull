using BE;
using Servicios.Utilidades;
using System.Xml.Linq;

namespace Mapper
{
    public class MPPComision
    {
        private readonly string rutaXML = XmlPaths.BaseDatosLocal;

        public List<Comision> ListarTodo()
        {
            if (!File.Exists(rutaXML))
                return new List<Comision>();

            var doc = XDocument.Load(rutaXML);
            var coms = doc.Root.Element("Comisiones")?.Elements("Comision")
                        .Where(x => x.Attribute("Active")?.Value == "true")
                        ?? Enumerable.Empty<XElement>();

            var lista = new List<Comision>();
            foreach (var x in coms)
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

                lista.Add(c);
            }

            return lista;
        }

        public Comision BuscarPorId(int id) =>
            ListarTodo().FirstOrDefault(c => c.ID == id);

        public void AltaComision(Comision comision)
        {
            XDocument doc;
            if (File.Exists(rutaXML))
                doc = XDocument.Load(rutaXML);
            else
                doc = new XDocument(new XElement("BaseDeDatosLocal", new XElement("Comisiones")));

            var root = doc.Root.Element("Comisiones") ?? new XElement("Comisiones");
            if (doc.Root.Element("Comisiones") == null)
                doc.Root.Add(root);

            var elem = new XElement("Comision",
                new XAttribute("Id", comision.ID),
                new XAttribute("Active", "true"),
                new XElement("Fecha", comision.Fecha),
                new XElement("Monto", comision.Monto),
                new XElement("Estado", comision.Estado),
                new XElement("MotivoRechazo", comision.MotivoRechazo ?? string.Empty),
                new XElement("Venta", new XAttribute("Id", comision.Venta.ID))
            );

            root.Add(elem);
            doc.Save(rutaXML);
        }

        public void GuardarLista(List<Comision> lista)
        {
            var doc = XDocument.Load(rutaXML);
            var root = doc.Root.Element("Comisiones");
            root.RemoveAll();

            foreach (var com in lista)
            {
                var elem = new XElement("Comision",
                    new XAttribute("Id", com.ID),
                    new XAttribute("Active", "true"),
                    new XElement("Fecha", com.Fecha),
                    new XElement("Monto", com.Monto),
                    new XElement("Estado", com.Estado),
                    new XElement("MotivoRechazo", com.MotivoRechazo ?? string.Empty),
                    new XElement("Venta", new XAttribute("Id", com.Venta.ID))
                );
                root.Add(elem);
            }

            doc.Save(rutaXML);
        }
    }
}
