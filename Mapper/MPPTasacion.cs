using BE;
using Servicios.Utilidades;
using System.Xml.Linq;

namespace Mapper
{
    public class MPPTasacion
    {
        private readonly string _rutaXml = XmlPaths.BaseDatosLocal;
        private const string NodoRoot = "Tasaciones";
        private const string NodoElemento = "Tasacion";

        public List<Tasacion> ListarTodo()
        {
            if (!File.Exists(_rutaXml))
                return new List<Tasacion>();

            var doc = XDocument.Load(_rutaXml);
            var elems = doc.Root.Element(NodoRoot)?.Elements(NodoElemento) ?? Enumerable.Empty<XElement>();

            return elems
                .Where(x => (string)x.Attribute("Active") == "true")
                .Select(Parse)
                .ToList();
        }

        public Tasacion BuscarPorId(int id)
        {
            if (!File.Exists(_rutaXml)) return null;
            var doc = XDocument.Load(_rutaXml);
            var x = doc.Root.Element(NodoRoot)?
                .Elements(NodoElemento)
                .FirstOrDefault(e => (int)e.Attribute("Id") == id && (string)e.Attribute("Active") == "true");
            return x == null ? null : Parse(x);
        }

        public void Alta(Tasacion t)
        {
            XDocument doc;
            if (File.Exists(_rutaXml))
                doc = XDocument.Load(_rutaXml);
            else
                doc = new XDocument(new XElement("BaseDeDatosLocal", new XElement(NodoRoot)));

            var root = doc.Root.Element(NodoRoot) ?? new XElement(NodoRoot);
            if (doc.Root.Element(NodoRoot) == null)
                doc.Root.Add(root);

            int nuevoId = root.Elements(NodoElemento).Any()
                ? root.Elements(NodoElemento).Max(e => (int)e.Attribute("Id")) + 1
                : 1;

            t.ID = nuevoId;
            var elem = new XElement(NodoElemento,
                new XAttribute("Id", t.ID),
                new XAttribute("Active", "true"),
                new XElement("OfertaId", t.Oferta.ID),
                new XElement("ValorFinal", t.ValorFinal),
                new XElement("Fecha", t.Fecha)
            );

            root.Add(elem);
            doc.Save(_rutaXml);
        }

        public void Modificar(Tasacion t)
        {
            if (!File.Exists(_rutaXml)) throw new FileNotFoundException(_rutaXml);
            var doc = XDocument.Load(_rutaXml);
            var elem = doc.Root.Element(NodoRoot)?
                .Elements(NodoElemento)
                .FirstOrDefault(e => (int)e.Attribute("Id") == t.ID);

            if (elem == null) throw new ApplicationException("Tasación no encontrada");

            elem.SetElementValue("OfertaId", t.Oferta.ID);
            elem.SetElementValue("ValorFinal", t.ValorFinal);
            elem.SetElementValue("Fecha", t.Fecha);

            doc.Save(_rutaXml);
        }

        public void Baja(int id)
        {
            if (!File.Exists(_rutaXml)) return;
            var doc = XDocument.Load(_rutaXml);
            var elem = doc.Root.Element(NodoRoot)?
                .Elements(NodoElemento)
                .FirstOrDefault(e => (int)e.Attribute("Id") == id);

            if (elem != null)
            {
                elem.SetAttributeValue("Active", "false");
                doc.Save(_rutaXml);
            }
        }

        private Tasacion Parse(XElement x) => new Tasacion
        {
            ID = (int)x.Attribute("Id"),
            Oferta = new OfertaCompra { ID = (int)x.Element("OfertaId") },
            ValorFinal = decimal.Parse(x.Element("ValorFinal").Value),
            Fecha = DateTime.Parse(x.Element("Fecha").Value)
        };
    }
}
