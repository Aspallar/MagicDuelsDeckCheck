using System;
using System.IO;

namespace MagicDuels
{
    internal static class MagicDuelsSteamProfile
    {
        public static byte[] ReadCards(string profileFileName, int numCards)
        {
            byte[] profileData = GetProfileData(profileFileName);
            return ExtractCardData(numCards, profileData);
        }

        private static byte[] ExtractCardData(int numCards, byte[] profileData)
        {
            byte[] cards = new byte[numCards];
            int cardsBaseIndex = profileData.Length - 1026;
            for (int k = 0; k * 2 < cards.Length; k++)
            {
                int cardIndex = k * 2;
                byte cardData = profileData[cardsBaseIndex + k];  // 2 cards per byte
                cards[cardIndex] = (byte)(cardData & 0x0F);
                if (cardIndex + 1 < cards.Length)
                    cards[cardIndex + 1] = (byte)((cardData >> 4) & 0x0F);
            }
            return cards;
        }

        private static byte[] GetProfileData(string profileFileName)
        {
            byte[] fileContent = File.ReadAllBytes(profileFileName);

            for (int k = fileContent.Length - 1; k >= 1; k--)
                fileContent[k] ^= fileContent[k - 1];
            fileContent[0] ^= fileContent[fileContent.Length - 1];

            byte[] data = new byte[fileContent.Length - 28];

            Buffer.BlockCopy(fileContent, 0, data, 0, 0x842);
            Buffer.BlockCopy(fileContent, 0x85E, data, 0x842, fileContent.Length - 0x85E);

            int fieldLen = (data[0x45A] & 0xFF) + (((data[0x45B] ^ data[0x45A]) & 0xFF) << 8);
            for (int k = 0x459 + fieldLen; k >= 0x45B; k--)
                data[k] ^= data[k - 1];
            data[0x45A] ^= data[0x459 + fieldLen];
            return data;
        }
    }
}
