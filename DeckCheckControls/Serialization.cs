using System.IO;
using System.Runtime.Serialization;
using System.Xml;

namespace DeckCheckControls
{
    public class Serialization
    {
        public static T Read<T>(Stream stream)
        {
            DataContractSerializer serializer = new DataContractSerializer(typeof(T));
            using (XmlReader reader = XmlReader.Create(stream))
                return (T)serializer.ReadObject(reader);
        }

        public static T Read<T>(string filePath) where T : new()
        {
            if (!File.Exists(filePath))
                return new T();
            using (Stream stream = File.OpenRead(filePath))
                return Read<T>(stream);
        }

        public static void Save<T>(Stream stream, T data)
        {
            DataContractSerializer serializer = new DataContractSerializer(typeof(T));
            using (XmlWriter writer = XmlWriter.Create(stream))
                serializer.WriteObject(writer, data);
        }

        public static void Save<T>(string filePath, T data)
        {
            using (Stream stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
                Save(stream, data);
        }

    }
}
