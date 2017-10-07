using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System;
using System.Text.RegularExpressions;

namespace MagicDuelsDeckCheck
{
    internal class PageGenerator
    {
        private const string possesedItemTemplateName = "PossesedItemTemplate.html";

        private PageTemplate _pageTemplate;
        private SectionTemplate _possessedSectionTemplate;
        private SectionTemplate _deckLinkSectionTemplate;
        private SectionTemplate _unknownSectionTemplate;

        private ItemTemplate _itemTemplate;
        private ItemTemplate _possessedItemTemplate;
        private ItemTemplate _UnknownItemTemplate;

        public void Initialize(bool showPosessedCards)
        {
            _pageTemplate = new PageTemplate("Page.html");
            //_possessedSectionTemplate = new SectionTemplate("PossesedCards.html");
            //_deckLinkSectionTemplate = new SectionTemplate("DeckLink.html");
            //_unknownSectionTemplate = new SectionTemplate("UnknownCards.html");

            _itemTemplate = new ItemTemplate("ItemTemplate.html");
            _possessedItemTemplate = _itemTemplate;
            _UnknownItemTemplate = _itemTemplate;
            //_possessedItemTemplate = new ItemTemplate("PossessedItems.html");
            //_UnknownItemTemplate = new ItemTemplate("UnknownItems.html");


            //_possessedItemTemplate = new ItemTemplate("PossessedItems.html");
            //_UnknownItemTemplate = new ItemTemplate("UnknownItems.html");
        }

        public bool ShowPossessedCards { get; set; }

        public string MakePage(DeckInfo deck, string deckPath)
        {
            int totalNeeded = deck.Cards.Sum(x => !x.Unknown && x.Shortfall > 0 ? x.Shortfall : 0);
            IEnumerable<DeckEntry> neededCards = deck.Cards.Where(x => !x.Unknown && x.Shortfall > 0);
            IEnumerable<DeckEntry> unknownCards = deck.Cards.Where(x => x.Unknown);
            IEnumerable<DeckEntry> possessedCards = deck.Cards.Where(x => !x.Unknown && x.Shortfall <= 0);

            string neededMarkup = MakeCardItemsMarkup(neededCards, _itemTemplate);
            string unknownMarkup = MakeCardItemsMarkup(unknownCards, _UnknownItemTemplate);
            string possessedMarkup = MakeCardItemsMarkup(possessedCards, _possessedItemTemplate);

            StringBuilder page = _pageTemplate.GetSectionMarkup(deck, neededMarkup, totalNeeded, unknownMarkup, possessedMarkup, deckPath);

            //page.Replace(SectionTemplateFields.DeckName, deck.DeckName);
            //page.Replace(SectionTemplateFields.CardTotal, totalNeeded.ToString());
            //page.Replace(SectionTemplateFields.Cards, MakeCardItemsMarkup(neededCards, _itemTemplate));

            //if (unknownCards.Any())
            //    page.Replace(SectionTemplateFields.UnknownCards, MakeUnkownCards(unknownCards));
            //else
            //    page.Replace(SectionTemplateFields.UnknownCards, "");

            string fileName = GetFilename();
            File.WriteAllText(fileName, page.ToString());
            return fileName;
        }

        private string MakeCardItemsMarkup(IEnumerable<DeckEntry> cards, ItemTemplate template)
        {
            StringBuilder cardItems = new StringBuilder();
            foreach (var card in cards)
                cardItems.Append(template.GetItemHtml(card));
            return cardItems.ToString();
        }

        private string GetFilename()
        {
            return Path.GetTempPath() + "DeckCheckMissingCards.html";
        }



        private string MakeUnkownCards(IEnumerable<DeckEntry> unkownCards)
        {
            StringBuilder sb = new StringBuilder("<div class=\"row\"><h3>Cards not in Magic Duels</h3>");
            foreach (var card in unkownCards)
            {
                sb.Append(card.CardName);
                sb.Append("<br />");
            }
            sb.Append("</div>");
            return sb.ToString();
        }
    }
}
