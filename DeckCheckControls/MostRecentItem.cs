using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeckCheckControls
{
    public class MostRecentItem
    {
        public string DisplayName { get; private set; }
        public string Path { get; private set; }

        public MostRecentItem(string displayName, string path)
        {
            DisplayName = displayName;
            Path = path;
        }
    }
}
