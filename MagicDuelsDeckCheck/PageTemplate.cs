using System;
using System.Collections.Generic;
using System.Text;

namespace MagicDuelsDeckCheck
{
    internal class PageTemplate : SectionTemplate
    {
        public bool ContainsCardsSection { get; private set; }
        public bool ContainsDeckLinkSection { get; private set; }
        public bool ContainsUnknownCardsSection { get; private set; }
        public bool ContainsPossesedCardsSection { get; private set; }

        public PageTemplate(string templateFileName) : base(templateFileName)
        {
            ContainsCardsSection = Template.IndexOf(PageTemplateFields.CardsSection) != -1;
            ContainsDeckLinkSection = Template.IndexOf(PageTemplateFields.DeckLinkSection) != -1;
            ContainsUnknownCardsSection = Template.IndexOf(PageTemplateFields.UnknownCardsSection) != -1;
            ContainsPossesedCardsSection = Template.IndexOf(PageTemplateFields.PossesedCardsSection) != -1;
        }

    }
}