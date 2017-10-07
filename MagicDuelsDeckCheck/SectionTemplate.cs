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

        public SectionTemplate(string templateFileName)
        {
            Template = File.ReadAllText(templateFileName);

            ContainsDeckName = Template.IndexOf(SectionTemplateFields.DeckName) != -1;
            ContainsCardTotal = Template.IndexOf(SectionTemplateFields.CardTotal) != -1;
            ContainsDeckUrl = Template.IndexOf(SectionTemplateFields.DeckUrl) != -1;
            ContainsCards = Template.IndexOf(SectionTemplateFields.Cards) != -1;
            ContainsPossessedCards = Template.IndexOf(SectionTemplateFields.PossessedCards) != -1;
            ContainsUnknownCards = Template.IndexOf(SectionTemplateFields.UnknownCards) != -1;
        }

        public StringBuilder GetSectionMarkup(DeckInfo deck, string neededMarkup, int totalNeeded, string unknownMarkup, string possessedMarkup, string deckPath)
        {
            StringBuilder section = new StringBuilder(Template);

            if (ContainsDeckName)
                section.Replace(SectionTemplateFields.DeckName, deck.DeckName);

            if (ContainsCardTotal)
                section.Replace(SectionTemplateFields.CardTotal, totalNeeded.ToString());

            if (ContainsCards)
                section.Replace(SectionTemplateFields.Cards, neededMarkup);



            return section;
        }
    }
}
