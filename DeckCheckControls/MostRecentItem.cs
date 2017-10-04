using System.Runtime.Serialization;

namespace DeckCheckControls
{
    [DataContract]
    public class MostRecentItem
    {
        [DataMember]
        public string DisplayName { get; private set; }

        [DataMember]
        public string Path { get; private set; }

        public MostRecentItem(string displayName, string path)
        {
            DisplayName = displayName;
            Path = path;
        }

        public override string ToString()
        {
            return DisplayName;
        }
    }
}
