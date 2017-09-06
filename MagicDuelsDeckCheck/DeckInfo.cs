using System.Collections.Generic;

namespace MagicDuelsDeckCheck
{
    internal class DeckInfo
    {
        public string DeckName { get; set; }
        public List<DeckEntry> Cards { get; set; } = new List<DeckEntry>();

        public DeckInfo(string deckName)
        {
            DeckName = deckName;
        }
    }
}
