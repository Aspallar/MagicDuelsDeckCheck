using MagicDuels;
using System;
using System.Text.RegularExpressions;

namespace MagicDuelsDeckCheck
{
    internal class TextDeckReader : IDeckReader
    {
        public DeckInfo ReadDeck(string deckDefinition)
        {
            deckDefinition = StripSideboard(deckDefinition);
            MatchCollection cardLines = Regex.Matches(deckDefinition, @"^(\d+)\s*[x|X]?\s+(.*)$", RegexOptions.Multiline);
            DeckInfo deckInfo = new DeckInfo("Magic Duels Deck");
            foreach (Match match in cardLines)
            {
                string cardName = AdjustTwoFacedCardNames(match.Groups[2].Value.TrimEnd());
                if (!MagicDuelsHelper.IsBasicLand(cardName))
                {
                    deckInfo.Cards.Add(new DeckEntry {
                        Required = int.Parse(match.Groups[1].Value),
                        CardName = cardName,
                    });
                }
            }
            return deckInfo;
        }

        private string AdjustTwoFacedCardNames(string name)
        {
            int pos = name.IndexOf("/");
            return pos == -1 ? name : name.Substring(0, pos - 1);
        }

        private string StripSideboard(string deckDefinition)
        {
            const string sideboardText = "SIDEBOARD";
            int pos = deckDefinition.IndexOf(sideboardText, StringComparison.InvariantCultureIgnoreCase);
            if (pos == 0) return "";
            return pos == -1 ? deckDefinition : deckDefinition.Substring(0, pos - 1);
        }

    }

}

