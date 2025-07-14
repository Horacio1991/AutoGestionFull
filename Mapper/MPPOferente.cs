using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using BE;
using Servicios.Utilidades;

namespace Mapper
{
    public class MPPOferente
    {
        private readonly string rutaXML = XmlPaths.BaseDatosLocal;

        public List<Oferente> ListarTodo()
        {
            if (!File.Exists(rutaXML))
                return new List<Oferente>();

            var doc = XDocument.Load(rutaXML);
            var root = doc.Root.Element("Oferentes");
            if (root == null)
                return new List<Oferente>();

            return root
                .Elements("Oferente")
                .Where(x => (string)x.Attribute("Active") == "true")
                .Select(x => new Oferente
                {
                    ID = (int)x.Attribute("Id"),
                    Dni = x.Element("Dni").Value.Trim(),
                    Nombre = x.Element("Nombre").Value.Trim(),
                    Apellido = x.Element("Apellido").Value.Trim(),
                    Contacto = x.Element("Contacto").Value.Trim()
                })
                .ToList();
        }

        public Oferente BuscarPorDni(string dni)
        {
            if (!File.Exists(rutaXML))
                return null;

            var doc = XDocument.Load(rutaXML);
            var elem = doc.Root
                .Element("Oferentes")?
                .Elements("Oferente")
                .FirstOrDefault(x => x.Element("Dni").Value.Trim() == dni && (string)x.Attribute("Active") == "true");
            if (elem == null) return null;

            return new Oferente
            {
                ID = (int)elem.Attribute("Id"),
                Dni = elem.Element("Dni").Value.Trim(),
                Nombre = elem.Element("Nombre").Value.Trim(),
                Apellido = elem.Element("Apellido").Value.Trim(),
                Contacto = elem.Element("Contacto").Value.Trim()
            };
        }

        public void AltaOferente(Oferente oferente)
        {
            XDocument doc;
            if (File.Exists(rutaXML))
                doc = XDocument.Load(rutaXML);
            else
            {
                Directory.CreateDirectory(Path.GetDirectoryName(rutaXML));
                doc = new XDocument(new XElement("BaseDeDatosLocal", new XElement("Oferentes")));
            }

            var root = doc.Root.Element("Oferentes");
            int nuevoId = 1;
            if (root.Elements("Oferente").Any())
                nuevoId = root.Elements("Oferente").Max(x => (int)x.Attribute("Id")) + 1;

            oferente.ID = nuevoId;

            var nuevo = new XElement("Oferente",
                new XAttribute("Id", oferente.ID),
                new XAttribute("Active", "true"),
                new XElement("Dni", oferente.Dni),
                new XElement("Nombre", oferente.Nombre),
                new XElement("Apellido", oferente.Apellido),
                new XElement("Contacto", oferente.Contacto)
            );

            root.Add(nuevo);
            doc.Save(rutaXML);
        }
    }
}
