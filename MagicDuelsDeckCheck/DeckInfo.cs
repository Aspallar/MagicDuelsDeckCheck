using System;
using System.Linq;
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
            MergeDuplicateCards();
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
                    entry.WikiImageUrl = card.WikiImageUrl;
                }
            }
        }

        private void MergeDuplicateCards()
        {
            // preserves card order

            var occursMoreThanOnce = Cards
                .GroupBy(x => x.CardName)
                .Where(g => g.Count() > 1)
                .Select(g => new { CardName = g.Key, RequiredSum = g.Sum(x => x.Required) });

            foreach (var duplicateCard in occursMoreThanOnce)
            {
                var theseCards = Cards.Where(x => x.CardName == duplicateCard.CardName).ToArray();
                theseCards[0].Required = duplicateCard.RequiredSum;
                for (int k = 1; k < theseCards.Length; k++)
                    Cards.Remove(theseCards[k]);
            }
        }
    }
}
