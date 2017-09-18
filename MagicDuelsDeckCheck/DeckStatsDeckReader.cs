using MagicDuels;
using Microsoft.CSharp.RuntimeBinder;
using Newtonsoft.Json.Linq;
using System;

namespace MagicDuelsDeckCheck
{
    internal class DeckStatsDeckReader : IDeckReader
    {
        public DeckInfo ReadDeck(string deckDefinition)
        {
            string deckJson = ExtractDeckJson(deckDefinition);
            dynamic deck = JObject.Parse(deckJson);
            DeckInfo deckInfo = new DeckInfo((string)deck.name);
            GetCards(deck, deckInfo);
            return deckInfo;
        }

        private void GetCards(dynamic deck, DeckInfo deckInfo)
        {
            foreach (var section in deck.sections)
            {
                dynamic cards;
                try { cards = section.cards; } catch (RuntimeBinderException) { cards = null; }
                if (cards != null)
                {
                    foreach (var card in cards)
                    {
                        if ((bool)card.isSideboard) continue;
                        string cardName = AdjustTwoFacedCardNames((string)card.name);
                        if (!MagicDuelsHelper.IsBasicLand(cardName))
                        {
                            deckInfo.Cards.Add(new DeckEntry
                            {
                                Required = (int)card.amount,
                                CardName = cardName,
                            });
                        }
                    }
                }
            }
        }

        private string AdjustTwoFacedCardNames(string name)
        {
            int pos = name.IndexOf("//");
            return pos == -1 ? name : name.Substring(0, pos - 1);
        }

        private static string ExtractDeckJson(string deckDefinition)
        {
            const string jsonStart = "deck_json = ";
            int startPos = deckDefinition.IndexOf(jsonStart);
            if (startPos == -1)
                throw new InvalidOperationException("Unable to find start of deck definition.");
            startPos += jsonStart.Length;

            const string jsonEnd = "deck_url = ";
            int endPos = deckDefinition.IndexOf(jsonEnd);
            while (deckDefinition[endPos] != '}')
            {
                --endPos;
                if (endPos <= startPos)
                    throw new InvalidOperationException("Unable to find end of deck definition.");
            }

            return deckDefinition.Substring(startPos, endPos - startPos + 1);
        }
    }
}
