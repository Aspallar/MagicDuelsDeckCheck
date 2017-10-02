using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DeckCheckControls
{
    [CollectionDataContract]
    public class FavouritesList : List<MostRecentItem>
    {
        public static FavouritesList Read(Stream stream)
        {
            DataContractSerializer serializer = new DataContractSerializer(typeof(FavouritesList));
            using (XmlReader reader = XmlReader.Create(stream))
                return (FavouritesList)serializer.ReadObject(reader);
        }

        public static FavouritesList Read(string filePath)
        {
            if (!File.Exists(filePath))
                return new FavouritesList();
            using (Stream stream = File.OpenRead(filePath))
                return Read(stream);
        }

        public static void Save(Stream stream, FavouritesList FavouritesList)
        {
            DataContractSerializer serializer = new DataContractSerializer(typeof(FavouritesList));
            using (XmlWriter writer = XmlWriter.Create(stream))
                serializer.WriteObject(writer, FavouritesList);
        }

        public static void Save(string filePath, FavouritesList FavouritesList)
        {
            using (Stream stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
                Save(stream, FavouritesList);
        }
    }
}
