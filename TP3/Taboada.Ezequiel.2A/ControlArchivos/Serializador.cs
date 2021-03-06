using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Xml;
using System.Xml.Serialization;
using Excepciones;

namespace ControlArchivos
{
    public class Serializador<T> : IArchivos<T>
        where T : class
    {
        string ubicacion = string.Empty;
        private ETipoExtenxion extenxion;

        public Serializador(ETipoExtenxion extenxion)
        {
            this.extenxion = extenxion;
        }

        public bool Escribir(string nombre, T dato)
        {
            bool ok = false;
            try
            {
                if (Directory.Exists(Environment.SpecialFolder.Desktop + nombre))
                {
                    ubicacion = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\" + nombre;
                }
                else
                {
                    ubicacion = AppDomain.CurrentDomain.BaseDirectory + nombre;
                }
                if (this.extenxion == ETipoExtenxion.Json)
                {
                    if (Path.GetExtension(nombre) == ".json")
                    {
                        Archivo archivo = new Archivo();
                        JsonSerializerOptions opciones = new JsonSerializerOptions();
                        opciones.WriteIndented = true;
                        archivo.Escribir(ubicacion,JsonSerializer.Serialize(dato, typeof(T), opciones));
                        ok = true;
                    }
                    else
                    {
                        throw new ExtensionInvalidaException("La extension no es Json");
                    }
                }
                else
                {
                    if (Path.GetExtension(nombre) == ".xml")
                    {
                        using (XmlTextWriter xmlTextWriter = new XmlTextWriter(ubicacion, Encoding.UTF8))
                        {
                            xmlTextWriter.Formatting = Formatting.Indented;
                            XmlSerializer xmlSerializar = new XmlSerializer(typeof(T));
                            xmlSerializar.Serialize(xmlTextWriter, dato);
                            ok = true;
                        }
                    }
                    else
                    {
                        throw new ExtensionInvalidaException("La extension no es Xml");
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return ok;
        }

        public T Leer(string nombre)
        {
            try
            {
                if (Directory.Exists(Environment.SpecialFolder.Desktop + nombre))
                {
                    ubicacion = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\" + nombre;
                }
                else
                {
                    ubicacion = AppDomain.CurrentDomain.BaseDirectory + nombre;
                }
                if (this.extenxion == ETipoExtenxion.Json)
                {
                    if (Path.GetExtension(nombre) == ".json")
                    {
                        Archivo archivo = new Archivo();
                        T objeto = JsonSerializer.Deserialize<T>(archivo.Leer(ubicacion));
                        return objeto;
                    }
                    else
                    {
                        throw new ExtensionInvalidaException("La extension no es Json");
                    }
                }
                else
                {

                    if (Path.GetExtension(nombre) == ".xml")
                    {
                        using (XmlTextReader xmliTextReader = new XmlTextReader(ubicacion))
                        {
                            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                            T objeto = xmlSerializer.Deserialize(xmliTextReader) as T;
                            return objeto;
                        }
                    }
                    else
                    {
                        throw new ExtensionInvalidaException("La extension no es Xml");
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }  
        
    }
}
