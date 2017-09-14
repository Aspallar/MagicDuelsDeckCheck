using AngleSharp.Dom.Html;
using AngleSharp.Parser.Html;
using MagicDuels;

namespace MagicDuelsDeckCheck
{
    internal class MagicDuelsHelperDeckReader : IDeckReader
    {
        public DeckInfo ReadDeck(string deckDefinition)
        {
            HtmlParser parser = new HtmlParser();
            IHtmlDocument doc = parser.Parse(deckDefinition);

            var deckTitle = doc.QuerySelector("h1").TextContent;
            var deckList = doc.QuerySelectorAll("a[href^='/cards?cardName=']");

            const string titlePrefix = "Magic Duels Deck: ";
            if (deckTitle.StartsWith(titlePrefix))
                deckTitle = deckTitle.Substring(titlePrefix.Length);

            DeckInfo deckInfo = new DeckInfo(deckTitle);

            foreach (var entry in deckList)
            {
                string cardName = entry.TextContent;
                if (!MagicDuelsHelper.IsBasicLand(cardName))
                {
                    string numberOf = entry.PreviousSibling.TextContent.Trim();
                    int number = int.Parse(numberOf.Substring(0, numberOf.Length - 1));
                    deckInfo.Cards.Add(new DeckEntry
                    {
                        Required = number,
                        CardName = cardName,
                    });
                }
            }
            return deckInfo;
        }
    }
}
