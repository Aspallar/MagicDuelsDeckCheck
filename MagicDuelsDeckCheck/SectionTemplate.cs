using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MagicDuelsDeckCheck
{
    internal class SectionTemplate
    {
        public readonly string Template;

        public bool ContainsDeckName { get; private set; }
        public bool ContainsCardTotal { get; private set; }
        public bool ContainsDeckUrl { get; private set; }
        public bool ContainsCards { get; private set; }
        public bool ContainsPossessedCards { get; private set; }
        public bool ContainsUnknownCards { get; private set; }
        public bool ContainsPossessedCardTotal { get; private set; }
        public bool ContainsPath { get; private set; }

        public SectionTemplate()
        {
            Template = "";
        }

        public SectionTemplate(string templateFileName)
        {
            Template = File.ReadAllText(templateFileName);

            ContainsDeckName = Template.IndexOf(SectionTemplateFields.DeckName) != -1;
            ContainsCardTotal = Template.IndexOf(SectionTemplateFields.CardTotal) != -1;
            ContainsDeckUrl = Template.IndexOf(SectionTemplateFields.DeckUrl) != -1;
            ContainsCards = Template.IndexOf(SectionTemplateFields.Cards) != -1;
            ContainsPossessedCards = Template.IndexOf(SectionTemplateFields.PossessedCards) != -1;
            ContainsUnknownCards = Template.IndexOf(SectionTemplateFields.UnknownCards) != -1;
            ContainsPossessedCardTotal = Template.IndexOf(SectionTemplateFields.PossessedCardTotal) != -1;
            ContainsPath = Template.IndexOf(SectionTemplateFields.Path) != -1;
        }

        public StringBuilder GetSectionMarkup(SectionData data)
        {
            StringBuilder section = new StringBuilder(Template);

            if (ContainsDeckName)
                section.Replace(SectionTemplateFields.DeckName, data.Deck.DeckName);

            if (ContainsPath)
                section.Replace(SectionTemplateFields.Path, data.TemplatePath);

            if (ContainsCardTotal)
                section.Replace(SectionTemplateFields.CardTotal, data.TotalNeeded.ToString());

            if (ContainsPossessedCardTotal)
                section.Replace(SectionTemplateFields.PossessedCardTotal, data.TotalPossessed.ToString());

            if (ContainsCards)
                section.Replace(SectionTemplateFields.Cards, data.NeededMarkup);

            if (ContainsUnknownCards)
                section.Replace(SectionTemplateFields.UnknownCards, data.UnknownMarkup);

            if (ContainsDeckUrl)
                section.Replace(SectionTemplateFields.DeckUrl, data.DeckPath);

            if (ContainsPossessedCards)
                section.Replace(SectionTemplateFields.PossessedCards, data.PossessedMarkup);

            return section;
        }
    }
}
