using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MagicDuelsDeckCheck
{
    internal class ItemTemplate
    {
        public readonly string Template;

        public bool ContainCardName { get; private set; }
        public bool ContainsCount { get; private set; }
        public bool ContainsUrlCardName { get; private set; }
        public bool ContainsWikiCardName { get; private set; }
        public bool ContainsTappedOutCardName { get; private set; }
        public bool ContainsSet { get; private set; }
        public bool ContainsCorrectName { get; private set; }
        public bool ContainsApostophieCardName { get; private set; }

        public ItemTemplate(string templateFileName)
        {
            Template = File.ReadAllText(templateFileName);

            ContainsCount = Template.IndexOf(ItemTemplateFields.Count) != -1;
            ContainCardName = Template.IndexOf(ItemTemplateFields.CardName) != -1;
            ContainsUrlCardName = Template.IndexOf(ItemTemplateFields.UrlCardName) != -1;
            ContainsUrlCardName = Template.IndexOf(ItemTemplateFields.UrlCardName) != -1;
            ContainsWikiCardName = Template.IndexOf(ItemTemplateFields.WikiCardName) != -1;
            ContainsTappedOutCardName = Template.IndexOf(ItemTemplateFields.TappedOutCardName) != -1;
            ContainsSet = Template.IndexOf(ItemTemplateFields.Set) != -1;
            ContainsCorrectName = Template.IndexOf(ItemTemplateFields.CorrectName) != -1;
            ContainsApostophieCardName = Template.IndexOf(ItemTemplateFields.ApostophieCardName) != -1;
        }

        public StringBuilder GetItemMarkup(DeckEntry card, bool useShortfallForCount)
        {
            StringBuilder item = new StringBuilder(Template);

            item.Replace(ItemTemplateFields.Count, useShortfallForCount ? card.Shortfall.ToString() : card.Possessed.ToString());
            item.Replace(ItemTemplateFields.CardName, card.CardName);

            if (ContainsUrlCardName)
                item.Replace(ItemTemplateFields.UrlCardName, card.UrlEncodedCardName);

            if (ContainsWikiCardName)
                item.Replace(ItemTemplateFields.WikiCardName, card.WikiCardName);

            if (ContainsTappedOutCardName)
                item.Replace(ItemTemplateFields.TappedOutCardName, card.TappedOutCardName);

            if (ContainsApostophieCardName)
                item.Replace(ItemTemplateFields.ApostophieCardName, card.EncodedApostropheCardName);

            if (ContainsSet)
                item.Replace(ItemTemplateFields.Set, card.Set);

            if (!string.IsNullOrEmpty(card.CorrectName))
                item.Replace(ItemTemplateFields.CorrectName, "Correct name: " + card.CorrectName);
            else
                item.Replace(ItemTemplateFields.CorrectName, "");

            return item;
        }

        public string GetAllItemsMarkup(IEnumerable<DeckEntry> cards, bool useShortfallForCount)
        {
            StringBuilder cardItems = new StringBuilder();
            foreach (var card in cards)
                cardItems.Append(GetItemMarkup(card, useShortfallForCount));
            return cardItems.ToString();
        }

    }
}
