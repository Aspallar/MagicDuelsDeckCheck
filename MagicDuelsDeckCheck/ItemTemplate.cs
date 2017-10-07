﻿using System.IO;
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
        }

        public StringBuilder GetItemHtml(DeckEntry card)
        {
            StringBuilder item = new StringBuilder(Template);

            item.Replace(ItemTemplateFields.Count, card.Shortfall > 0 ? card.Shortfall.ToString() : card.Required.ToString());
            item.Replace(ItemTemplateFields.CardName, card.CardName);

            if (ContainsUrlCardName)
                item.Replace(ItemTemplateFields.UrlCardName, card.UrlEncodedCardName);

            if (ContainsWikiCardName)
                item.Replace(ItemTemplateFields.WikiCardName, card.WikiCardName);

            if (ContainsTappedOutCardName)
                item.Replace(ItemTemplateFields.TappedOutCardName, card.TappedOutCardName);

            if (ContainsSet)
                item.Replace(ItemTemplateFields.Set, card.Set);

            if (!string.IsNullOrEmpty(card.CorrectName))
                item.Replace(ItemTemplateFields.CorrectName, "Correct name: " + card.CorrectName);
            else
                item.Replace(ItemTemplateFields.CorrectName, "");

            return item;
        }
    }
}
