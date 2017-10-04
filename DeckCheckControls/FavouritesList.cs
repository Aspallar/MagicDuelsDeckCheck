using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DeckCheckControls
{
    [CollectionDataContract]
    public class FavouritesList : List<MostRecentItem>
    {
        public static FavouritesList Read(string filePath)
        {
            return Serialization.Read<FavouritesList>(filePath);
        }

        public static void Save(string filePath, FavouritesList favouritesList)
        {
            Serialization.Save(filePath, favouritesList);
        }
    }
}
