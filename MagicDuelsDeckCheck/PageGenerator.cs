using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace MagicDuelsDeckCheck
{
    internal class PageGenerator
    {
        private const string possesedItemTemplateName = "PossesedItem.html";

        private string _templatePath;

        private PageTemplate _pageTemplate;
        private SectionTemplate _possessedSectionTemplate;
        private SectionTemplate _deckLinkSectionTemplate;
        private SectionTemplate _unknownSectionTemplate;

        private ItemTemplate _itemTemplate;
        private ItemTemplate _possessedItemTemplate;
        private ItemTemplate _unknownItemTemplate;

        public void Initialize(string templateFolder)
        {
            _templatePath = templateFolder;

            if (!string.IsNullOrEmpty(templateFolder) && !templateFolder.EndsWith(@"\"))
                templateFolder += @"\";

            _pageTemplate = new PageTemplate(templateFolder + "Page.html");
            _deckLinkSectionTemplate = new SectionTemplate(templateFolder + "DeckLink.html");
            _unknownSectionTemplate = new SectionTemplate(templateFolder + "UnknownCards.html");
            _itemTemplate = new ItemTemplate(templateFolder + "ItemTemplate.html");

            string possessedSectionPath = templateFolder + "PossessedCards.html";
            if (File.Exists(possessedSectionPath))
                _possessedSectionTemplate = new SectionTemplate(possessedSectionPath);
            else
                _possessedSectionTemplate = new SectionTemplate();

            _possessedItemTemplate = _itemTemplate;
            string possessedItemPath = templateFolder + possesedItemTemplateName;
            if (File.Exists(possessedItemPath))
                _possessedItemTemplate = new ItemTemplate(possessedItemPath);

            _unknownItemTemplate = _itemTemplate;
            var unknownTtemPath = templateFolder + "UnknownItem.html";
            if (File.Exists(unknownTtemPath))
                _unknownItemTemplate = new ItemTemplate(unknownTtemPath);
        }

        public string MakePage(DeckInfo deck, string deckPath)
        {
            string pageContent = GetPageMarkup(deck, deckPath);
            string fileName = GetFilename();
            File.WriteAllText(fileName, pageContent);
            return fileName;
        }

        private string GetPageMarkup(DeckInfo deck, string deckPath)
        {
            IEnumerable<DeckEntry> neededCards = deck.Cards.Where(x => !x.Unknown && x.Shortfall > 0);
            IEnumerable<DeckEntry> unknownCards = deck.Cards.Where(x => x.Unknown);
            IEnumerable<DeckEntry> possessedCards = deck.Cards.Where(x => x.Owned > 0);

            SectionData sectionData = new SectionData
            {
                TotalNeeded = neededCards.Sum(x => x.Shortfall),
                TotalPossessed = possessedCards.Sum(x => x.Possessed),
                NeededMarkup = _itemTemplate.GetAllItemsMarkup(neededCards),
                UnknownMarkup = _unknownItemTemplate.GetAllItemsMarkup(unknownCards),
                PossessedMarkup = _possessedItemTemplate.GetAllItemsMarkup(possessedCards),
                TemplatePath = _templatePath,
                Deck = deck,
                DeckPath = deckPath,
            };

            StringBuilder page = _pageTemplate.GetSectionMarkup(sectionData);

            if (_pageTemplate.ContainsDeckLinkSection)
            {
                if (Utils.IsHttpPath(deckPath))
                {
                    string deckLinkSection = _deckLinkSectionTemplate.GetSectionMarkup(sectionData).ToString();
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
                    string possessedCardsSection = _possessedSectionTemplate.GetSectionMarkup(sectionData).ToString();
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
                    string unknownCardsSection = _unknownSectionTemplate.GetSectionMarkup(sectionData).ToString();
                    page.Replace(PageTemplateFields.UnknownCardsSection, unknownCardsSection);
                }
                else
                {
                    page.Replace(PageTemplateFields.UnknownCardsSection, "");
                }
            }

            return page.ToString();
        }

        private string GetFilename()
        {
            return Path.GetTempPath() + "DeckCheckMissingCards.html";
        }

    }
}
