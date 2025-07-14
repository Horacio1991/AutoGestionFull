using BE;
using Servicios.Utilidades;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Mapper
{
    public class MPPBitacora
    {
        private readonly string rutaXML = XmlPaths.BaseDatosLocal;

        public List<Bitacora> ListarTodo()
        {
            if (!File.Exists(rutaXML))
                return new List<Bitacora>();

            var doc = XDocument.Load(rutaXML);
            var nodoBitacoras = doc.Root.Element("Bitacoras");
            if (nodoBitacoras == null)
                return new List<Bitacora>();

            return nodoBitacoras
                .Elements("Bitacora")
                .Select(x => new Bitacora
                {
                    ID = (int)x.Attribute("Id"),
                    FechaRegistro = DateTime.Parse((string)x.Attribute("Fecha")),
                    UsuarioID = (int)x.Attribute("UsuarioId"),
                    UsuarioNombre = (string)x.Attribute("Usuario"),
                    Detalle = (string)x.Element("Mensaje")
                })
                .ToList();
        }

        public void Agregar(Bitacora bit)
        {
            XDocument doc;
            if (File.Exists(rutaXML))
            {
                doc = XDocument.Load(rutaXML);
                if (doc.Root.Element("Bitacoras") == null)
                    doc.Root.Add(new XElement("Bitacoras"));
            }
            else
            {
                Directory.CreateDirectory(Path.GetDirectoryName(rutaXML));
                doc = new XDocument(
                    new XElement("BaseDeDatosLocal",
                        new XElement("Bitacoras")
                    )
                );
            }

            var bitacoras = doc.Root.Element("Bitacoras");
            int nuevoId = bitacoras.Elements("Bitacora")
                                   .Select(x => (int)x.Attribute("Id"))
                                   .DefaultIfEmpty(0)
                                   .Max() + 1;

            var nuevo = new XElement("Bitacora",
                new XAttribute("Id", nuevoId),
                new XAttribute("Fecha", bit.FechaRegistro.ToString("s")),
                new XAttribute("UsuarioId", bit.UsuarioID),
                new XAttribute("Usuario", bit.UsuarioNombre),
                new XElement("Mensaje", bit.Detalle)
            );

            bitacoras.Add(nuevo);
            doc.Save(rutaXML);
        }

        public bool GuardarRegistro(Bitacora bit)
        {
            try
            {
                var doc = File.Exists(rutaXML)
                          ? XDocument.Load(rutaXML)
                          : new XDocument(new XElement("Bitacoras"));
                var root = doc.Root;
                int nuevoId = root.Elements("Bitacora")
                                  .Max(x => (int?)x.Attribute("Id") ?? 0) + 1;
                var elem = new XElement("Bitacora",
                    new XAttribute("Id", nuevoId),
                    new XAttribute("Fecha", bit.FechaRegistro.ToString("yyyy-MM-dd HH:mm:ss")),
                    new XAttribute("Usuario", bit.UsuarioNombre),
                    new XElement("Mensaje", bit.Detalle)
                );
                root.Add(elem);
                doc.Save(rutaXML);
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
