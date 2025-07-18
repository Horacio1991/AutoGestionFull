using System.Xml.Linq;
using BE;
using Servicios.Utilidades;

namespace Mapper
{
    public class MPPTasacion
    {
        private readonly string rutaXML = XmlPaths.BaseDatosLocal;

        public MPPTasacion()
        {
            AsegurarEstructura();
        }

        private void AsegurarEstructura()
        {
            try
            {
                var dir = Path.GetDirectoryName(rutaXML);
                if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);

                if (!File.Exists(rutaXML))
                {
                    new XDocument(
                        new XElement("BaseDeDatosLocal",
                            new XElement("Tasaciones")
                        )
                    ).Save(rutaXML);
                }
                else
                {
                    var doc = XDocument.Load(rutaXML);
                    if (doc.Root.Element("Tasaciones") == null)
                    {
                        doc.Root.Add(new XElement("Tasaciones"));
                        doc.Save(rutaXML);
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        public List<Tasacion> ListarTodo()
        {
            try
            {
                var doc = XDocument.Load(rutaXML);
                var root = doc.Root.Element("Tasaciones");
                if (root == null) return new List<Tasacion>();

                var ofertaMapper = new MPPOfertaCompra();
                var vehiculoMapper = new MPPVehiculo();

                return root.Elements("Tasacion")
                           .Where(x => (string)x.Attribute("Active") == "true")
                           .Select(x =>
                           {
                               var ofertaId = (int)x.Element("OfertaId");
                               // cargo oferta completa y luego vehículo
                               var oferta = ofertaMapper.BuscarPorId(ofertaId);
                               if (oferta != null)
                               {
                                   var veh = vehiculoMapper.BuscarPorId(oferta.Vehiculo.ID);
                                   oferta.Vehiculo = veh ?? oferta.Vehiculo;
                               }

                               return new Tasacion
                               {
                                   ID = (int)x.Attribute("Id"),
                                   Oferta = oferta,
                                   ValorFinal = (decimal)x.Element("ValorFinal"),
                                   EstadoStock = (string)x.Element("EstadoStock"),
                                   Fecha = DateTime.Parse(x.Element("Fecha")?.Value ?? DateTime.Now.ToString("s"))
                               };
                           })
                           .ToList();
            }
            catch (Exception)
            {
                return new List<Tasacion>();
            }
        }

        public void AltaTasacion(Tasacion t)
        {
            try
            {
                var doc = XDocument.Load(rutaXML);
                var root = doc.Root.Element("Tasaciones");

                int nextId = root.Elements("Tasacion")
                                 .Select(x => (int)x.Attribute("Id"))
                                 .DefaultIfEmpty(0)
                                 .Max() + 1;

                t.ID = nextId;
                t.Fecha = DateTime.Now;

                var elem = new XElement("Tasacion",
                    new XAttribute("Id", t.ID),
                    new XAttribute("Active", "true"),
                    new XElement("OfertaId", t.Oferta.ID),
                    new XElement("ValorFinal", t.ValorFinal),
                    new XElement("EstadoStock", t.EstadoStock),
                    new XElement("Fecha", t.Fecha.ToString("s"))
                );

                root.Add(elem);
                doc.Save(rutaXML);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("No se pudo registrar la tasación. " + ex.Message, ex);
            }
        }
    }
}
