using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

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
    }
}
