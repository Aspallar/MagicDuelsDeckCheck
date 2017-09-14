using AngleSharp.Dom.Html;
using AngleSharp.Parser.Html;
using MagicDuels;

namespace MagicDuelsDeckCheck
{
    internal class MagicDuelsWikiDeckReader : IDeckReader
    {
        public DeckInfo ReadDeck(string deckDefinition)
        {
            HtmlParser parser = new HtmlParser();
            IHtmlDocument doc = parser.Parse(deckDefinition);

            var deckTitle = doc.QuerySelector("h1.page-header__title").TextContent;
            var deckList = doc.QuerySelectorAll("div.div-col.columns.column-count.column-count-2 span.card-image-tooltip");

            const string titlePrefix = "Decks/";
            if (deckTitle.StartsWith(titlePrefix))
                deckTitle = deckTitle.Substring(titlePrefix.Length);

            DeckInfo deckInfo = new DeckInfo(deckTitle);

            foreach (var entry in deckList)
            {
                string cardName = entry.TextContent;
                if (!MagicDuelsHelper.IsBasicLand(cardName))
                {
                    string numberOf = entry.PreviousSibling.TextContent.Trim();
                    int number = int.Parse(numberOf);
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
