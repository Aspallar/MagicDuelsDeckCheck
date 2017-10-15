using System;
using System.Linq;
using System.Web;

namespace MagicDuelsDeckCheck
{
    internal class DeckEntry
    {
        public int Required { get; set; }
        public int Owned { get; set; } 
        public string CardName { get; set; }
        public bool Unknown { get; set; }
        public string Set { get; set; }
        public string CorrectName { get; set; }
        public int Shortfall => Required - Owned;
        public int Possessed => Math.Min(Required, Owned);

        public string UrlEncodedCardName => HttpUtility.UrlEncode(CardName);

        public string EncodedApostropheCardName => CardName.Replace(" ", "%20");

        public string WikiImageUrl { get; set; }

        public string WikiCardName
        {
            get
            {
                string name = CardName.Replace(' ', '_');
                name = name.Replace("'", "%27");
                return name;
            }
        }

        public string TappedOutCardName
        {
            get
            {
                string name = RemovePunctuation(CardName);
                name = name.Replace(' ', '-');
                return name;
            }
        }

        private static string RemovePunctuation(string cardName)
            => new string(cardName.Where(c => !char.IsPunctuation(c)).ToArray());

    }
}
