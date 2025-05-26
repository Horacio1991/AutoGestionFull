using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AutoGestion.DAO
{
    public class XmlRepository<T>
    {
        private readonly string _archivo;

        public XmlRepository(string nombreArchivo)
        {
            var folder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DatosXML");
            Directory.CreateDirectory(folder);
            _archivo = Path.Combine(folder, nombreArchivo);

            if (!File.Exists(_archivo))
                GuardarLista(new List<T>());
        }

        public List<T> ObtenerTodos()
        {
            using var fs = new FileStream(_archivo, FileMode.Open);
            var serializer = new XmlSerializer(typeof(List<T>));
            return (List<T>)serializer.Deserialize(fs);
        }

        public void Agregar(T item)
        {
            var lista = ObtenerTodos();
            lista.Add(item);
            GuardarLista(lista);
        }

        public void GuardarLista(List<T> lista)
        {
            using var fs = new FileStream(_archivo, FileMode.Create);
            var serializer = new XmlSerializer(typeof(List<T>));
            serializer.Serialize(fs, lista);
        }
    }
}
