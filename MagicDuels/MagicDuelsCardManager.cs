using System;
using System.IO;
using System.Xml.Serialization;

namespace MagicDuels
{
    public class MagicDuelsCardManager
    {
        private const string cardDataFileName = "Cards.xml";

        public MagicDuelsCards GetCards(string profileFileName)
        {
            MagicDuelsCardList magicCardList;

            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(MagicDuelsCardList));
                using (var reader = new StreamReader(cardDataFileName))
                    magicCardList = (MagicDuelsCardList)xmlSerializer.Deserialize(reader);
            }
            catch (FileNotFoundException ex)
            {
                throw new BadCardDataException($"Missing {cardDataFileName}.", ex);
            }
            catch (Exception ex)
            {
                throw new BadCardDataException($"Error loading {cardDataFileName}.", ex);
            }

            try
            {
                MagicDuelsCards cards = new MagicDuelsCards();
                byte[] profileCardData = MagicDuelsSteamProfile.ReadCards(profileFileName, magicCardList.Count);

                int k = -1;
                foreach (CardInfo card in magicCardList)
                {
                    card.NumberOwned = profileCardData[++k] & 7;
                    cards.Add(card.DisplayName, card);
                }
                return cards;
            }
            catch (Exception ex)
            {
                throw new BadSteamProfileException("Unable to read steam profile. Is the profile path set correctly.", ex);
            }
        }
    }
}
