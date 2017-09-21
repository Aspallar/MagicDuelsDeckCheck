using System;
using System.Collections.Generic;
using MagicDuels;

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

        public void GetOwned(MagicDuelsCards _cards, CorrectCardNames _correctCardNames)
        {
            foreach (var entry in Cards)
            {
                CardInfo card;
                entry.Unknown = !_cards.TryGetValue(entry.CardName, out card);
                if (entry.Unknown)
                {
                    string correctName = _correctCardNames.GetCorrectName(entry.CardName);
                    if (!string.IsNullOrEmpty(correctName))
                    {
                        entry.CorrectName = correctName;
                        entry.Unknown = false;
                        card = _cards[correctName];
                    }
                }
                if (!entry.Unknown)
                {
                    entry.Owned = card.NumberOwned;
                    entry.Set = card.Set;
                }
            }
        }
    }
}
