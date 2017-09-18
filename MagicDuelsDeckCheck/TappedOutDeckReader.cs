using AngleSharp.Dom.Html;
using AngleSharp.Parser.Html;
using MagicDuels;

namespace MagicDuelsDeckCheck
{
    internal class TappedOutDeckReader : IDeckReader
    {
        public DeckInfo ReadDeck(string deckDefinition)
        {
            HtmlParser parser = new HtmlParser();
            IHtmlDocument doc = parser.Parse(deckDefinition);

            var deckTitle = doc.QuerySelector("title").TextContent.Trim();
            var deckList = doc.QuerySelectorAll("ul.boardlist li.member > a:first-child");

            const string titlePostfix = " (Magic Duels MTG Deck)";
            if (deckTitle.EndsWith(titlePostfix))
                deckTitle = deckTitle.Substring(0, deckTitle.Length - titlePostfix.Length);

            DeckInfo deckInfo = new DeckInfo(deckTitle);

            foreach (var entry in deckList)
            {
                string cardName = entry.GetAttribute("data-name");
                if (!MagicDuelsHelper.IsBasicLand(cardName))
                {
                    string numberOf = entry.TextContent.Trim();
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
