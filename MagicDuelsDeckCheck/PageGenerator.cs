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
        private const string possesedItemTemplateName = "PossesedItem.html";

        private PageTemplate _pageTemplate;
        private SectionTemplate _possessedSectionTemplate;
        private SectionTemplate _deckLinkSectionTemplate;
        private SectionTemplate _unknownSectionTemplate;

        private ItemTemplate _itemTemplate;
        private ItemTemplate _possessedItemTemplate;
        private ItemTemplate _UnknownItemTemplate;

        public void Initialize(string templateFolder)
        {
            if (!string.IsNullOrEmpty(templateFolder) && !templateFolder.EndsWith(@"\"))
                templateFolder += @"\";

            _pageTemplate = new PageTemplate(templateFolder + "Page.html");
            _possessedSectionTemplate = new SectionTemplate(templateFolder + "PossessedCards.html");
            _deckLinkSectionTemplate = new SectionTemplate(templateFolder + "DeckLink.html");
            _unknownSectionTemplate = new SectionTemplate(templateFolder + "UnknownCards.html");

            _itemTemplate = new ItemTemplate(templateFolder + "ItemTemplate.html");
            _possessedItemTemplate = _itemTemplate;
            string possessedPath = templateFolder + possesedItemTemplateName;
            if (File.Exists(possessedPath))
                _possessedItemTemplate = new ItemTemplate(possessedPath);
            _UnknownItemTemplate = new ItemTemplate(templateFolder + "UnknownItem.html");
        }

        public bool ShowPossessedCards { get; set; }

        public string MakePage(DeckInfo deck, string deckPath)
        {
            int totalNeeded = deck.Cards.Sum(x => !x.Unknown && x.Shortfall > 0 ? x.Shortfall : 0);
            int totalPossessed = deck.Cards.Sum(x => x.Possessed);
            IEnumerable<DeckEntry> neededCards = deck.Cards.Where(x => !x.Unknown && x.Shortfall > 0);
            IEnumerable<DeckEntry> unknownCards = deck.Cards.Where(x => x.Unknown);
            IEnumerable<DeckEntry> possessedCards = deck.Cards.Where(x => !x.Unknown && x.Owned > 0);

            string neededMarkup = MakeCardItemsMarkup(neededCards, _itemTemplate);
            string unknownMarkup = MakeCardItemsMarkup(unknownCards, _UnknownItemTemplate);
            string possessedMarkup = MakeCardItemsMarkup(possessedCards, _possessedItemTemplate);

            StringBuilder page = _pageTemplate.GetSectionMarkup(deck, neededMarkup, totalNeeded, unknownMarkup, possessedMarkup, totalPossessed, deckPath);

            if (_pageTemplate.ContainsDeckLinkSection)
            {
                if (Utils.IsHttpPath(deckPath))
                {
                    string deckLinkSection = _deckLinkSectionTemplate.GetSectionMarkup(deck, neededMarkup, totalNeeded, unknownMarkup, possessedMarkup, totalPossessed, deckPath).ToString();
                    page.Replace(PageTemplateFields.DeckLinkSection, deckLinkSection);
                }
                else
                {
                    page.Replace(PageTemplateFields.DeckLinkSection, "");
                }
            }

            if (_pageTemplate.ContainsPossesedCardsSection)
            {
                if (possessedCards.Any())
                {
                    string possessedCardsSection = _possessedSectionTemplate.GetSectionMarkup(deck, neededMarkup, totalNeeded, unknownMarkup, possessedMarkup, totalPossessed, deckPath).ToString();
                    page.Replace(PageTemplateFields.PossesedCardsSection, possessedCardsSection);
                }
                else
                {
                    page.Replace(PageTemplateFields.PossesedCardsSection, "");
                }
            }

            if (_pageTemplate.ContainsUnknownCardsSection)
            {
                if (unknownCards.Any())
                {
                    string unknownCardsSection = _unknownSectionTemplate.GetSectionMarkup(deck, neededMarkup, totalNeeded, unknownMarkup, possessedMarkup, totalPossessed, deckPath).ToString();
                    page.Replace(PageTemplateFields.UnknownCardsSection, unknownCardsSection);
                }
                else
                {
                    page.Replace(PageTemplateFields.UnknownCardsSection, "");
                }
            }

            string fileName = GetFilename();
            File.WriteAllText(fileName, page.ToString());
            return fileName;
        }

        private string MakeCardItemsMarkup(IEnumerable<DeckEntry> cards, ItemTemplate template)
        {
            StringBuilder cardItems = new StringBuilder();
            foreach (var card in cards)
                cardItems.Append(template.GetItemMarkup(card));
            return cardItems.ToString();
        }

        private string GetFilename()
        {
            return Path.GetTempPath() + "DeckCheckMissingCards.html";
        }

    }
}
