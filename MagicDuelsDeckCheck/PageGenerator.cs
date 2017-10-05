using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MagicDuelsDeckCheck
{
    internal class PageGenerator
    {
        string _pageTemplate;
        string _itemTemplate;

        public void Initialize()
        {
            _pageTemplate = File.ReadAllText("PageTemplate.html");
            _itemTemplate = File.ReadAllText("ItemTemplate.html");
        }

        public string MakePage(DeckInfo deck, string deckPath)
        {
            StringBuilder items = new StringBuilder();
            List<string> unkownCards = new List<string>();
            int total = 0;

            foreach (var card in deck.Cards)
            {
                if (!card.Unknown)
                {
                    if (card.Shortfall > 0)
                    {
                        StringBuilder item = new StringBuilder(_itemTemplate);
                        item.Replace("[shortfall]", card.Shortfall.ToString());
                        item.Replace("[cardname]", card.CardName);
                        item.Replace("[urlcardname]", card.CardName.Replace(" ", "%20"));
                        item.Replace("[wikicardname]", GetWikiCardName(card.CardName));
                        item.Replace("[set]", card.Set);
                        if (!string.IsNullOrEmpty(card.CorrectName))
                            item.Replace("[correctname]", "Correct name: " + card.CorrectName);
                        else
                            item.Replace("[correctname]", "");
                        items.Append(item);
                        total += card.Shortfall;
                    }
                }
                else
                {
                    unkownCards.Add(card.CardName);
                }
            }

            StringBuilder page = new StringBuilder(_pageTemplate);
            page.Replace("[deckname]", deck.DeckName);

            if (Utils.IsHttpPath(deckPath))
                page.Replace("[decklink]", $"<a href=\"{deckPath}\" target=\"_blank\">View deck definition</a>");
            else
                page.Replace("[decklink]", "");

            page.Replace("[cardtotal]", total.ToString());
            page.Replace("[cardlist]", items.ToString());

            if (unkownCards.Count > 0)
                page.Replace("[unknowncards]", MakeUnkownCards(unkownCards));
            else
                page.Replace("[unknowncards]", "");

            string fileName = GetFilename();
            File.WriteAllText(fileName, page.ToString());
            return fileName;
        }

        private string GetFilename()
        {
            return Path.GetTempPath() + "DeckCheckMissingCards.html";
        }

        private static string GetWikiCardName(string displayName)
        {
            string name = displayName.Replace(' ', '_');
            name = name.Replace("'", "%27");
            return name;
        }

        private string MakeUnkownCards(List<string> unkownCards)
        {
            StringBuilder sb = new StringBuilder("<div class=\"row\"><h3>Cards not in Magic Duels</h3>");
            foreach (var cardName in unkownCards)
            {
                sb.Append(cardName);
                sb.Append("<br />");
            }
            sb.Append("</div>");
            return sb.ToString();
        }
    }
}
