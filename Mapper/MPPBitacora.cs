using System.Xml.Linq;
using BE;
using Servicios.Utilidades;

namespace Mapper
{
    public class MPPBitacora
    {
        private readonly string rutaXML = XmlPaths.Bitacoras;

        public MPPBitacora()
        {
            AsegurarArchivo();
        }

        private void AsegurarArchivo()
        {
            try
            {
                var dir = Path.GetDirectoryName(rutaXML);
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);

                if (!File.Exists(rutaXML))
                {
                    new XDocument(new XElement("Bitacoras")).Save(rutaXML);
                }
            }
            catch (Exception ex)
            {
            }
        }

        // Lista todos los registros de la bitácora
        public List<Bitacora> ListarTodo()
        {
            var lista = new List<Bitacora>();
            try
            {
                var doc = XDocument.Load(rutaXML);

                foreach (var nodo in doc.Root.Elements("Bitacora"))
                {
                    lista.Add(new Bitacora
                    {
                        ID = (int)nodo.Attribute("Id"),
                        FechaRegistro = DateTime.Parse(nodo.Attribute("Fecha")?.Value ?? DateTime.Now.ToString("s")),
                        UsuarioID = int.Parse(nodo.Attribute("UsuarioId")?.Value ?? "0"),
                        UsuarioNombre = nodo.Attribute("Usuario")?.Value,
                        Detalle = nodo.Element("Mensaje")?.Value
                    });
                }
            }
            catch (Exception ex)
            {
            }
            return lista;
        }

        public void Alta(Bitacora registro)
        {
            try
            {
                var doc = XDocument.Load(rutaXML);
                var root = doc.Root;

                int siguienteId = root.Elements("Bitacora")
                                      .Select(x => (int)x.Attribute("Id"))
                                      .DefaultIfEmpty(0)
                                      .Max() + 1;

                registro.ID = siguienteId;
                registro.FechaRegistro = registro.FechaRegistro == default
                                         ? DateTime.Now
                                         : registro.FechaRegistro;

                var elem = new XElement("Bitacora",
                    new XAttribute("Id", registro.ID),
                    new XAttribute("Fecha", registro.FechaRegistro.ToString("s")),
                    new XAttribute("UsuarioId", registro.UsuarioID),
                    new XAttribute("Usuario", registro.UsuarioNombre ?? ""),
                    new XElement("Mensaje", registro.Detalle ?? "")
                );

                root.Add(elem);
                doc.Save(rutaXML);
            }
            catch (Exception ex)
            {
            }
        }
    }
}
