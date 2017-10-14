using AngleSharp.Dom.Html;
using AngleSharp.Parser.Html;
using MagicDuels;
using System.Net;

namespace MagicDuelsDeckCheck
{
    internal class MagicDuelsHelperDeckReader : IDeckReader
    {
        public DeckInfo ReadDeck(string deckDefinition)
        {
            HtmlParser parser = new HtmlParser();
            IHtmlDocument doc = parser.Parse(deckDefinition);

            var deckTitle = doc.QuerySelector("h1").TextContent;
            var deckList = doc.QuerySelector("#deckList");
            var cards = deckList.QuerySelectorAll("img[data-cardName]");
            var amounts = deckList.QuerySelectorAll("label[data-cardCount=count]");

            const string titlePrefix = "Magic Duels Deck: ";
            if (deckTitle.StartsWith(titlePrefix))
                deckTitle = deckTitle.Substring(titlePrefix.Length);

            DeckInfo deckInfo = new DeckInfo(deckTitle);

            for (int k = 0; k < cards.Length; k++)
            {
                string cardName = WebUtility.HtmlDecode(cards[k].GetAttribute("data-cardName"));
                if (!MagicDuelsHelper.IsBasicLand(cardName))
                {
                    int number = int.Parse(amounts[k].TextContent);
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
