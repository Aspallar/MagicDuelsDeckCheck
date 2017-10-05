using System;
using System.Collections.Generic;
using MagicDuels;
using System.Xml.Serialization;
using System.IO;
using System.Net;
using System.Threading;

namespace WikiCardCheck
{
    // quick console app to check rules for geterating card page urls for http://magicduels.wikia.com
    // in preparation for changiing card links over to wiki in v1.10.0 or later

    class Program
    {


        static void Main(string[] args)
        {
            List<CardInfo> notFoundCards = new List<CardInfo>();
            List<CardInfo> errorCards = new List<CardInfo>();
            WebClient client = new WebClient();
            MagicDuelsCardList cards = GetCards();
            Random rand = new Random();
            int count = 0;
            foreach (var card in cards)
            {
                ++count;
                string urlEnd = GetUrlCardName(card.DisplayName);
                Console.Write($"{count}. {card.DisplayName} [{urlEnd}]");
                try
                {
                    string content = client.DownloadString("http://magicduels.wikia.com/wiki/" + urlEnd);
                    if (content.IndexOf("was not found") > -1 && content.IndexOf("What do you want to do") > -1)
                    {
                        notFoundCards.Add(card);
                        Console.WriteLine(" NOT FOUND.");
                    }
                    else
                    {
                        Console.WriteLine(" Found.");
                    }
                }
                catch (WebException ex)
                {
                    if (ex.Message.IndexOf("(404)") > -1)
                    {
                        notFoundCards.Add(card);
                        Console.WriteLine(" NOT FOUND (404).");
                    }
                    else
                    {
                        errorCards.Add(card);
                        Console.WriteLine(" Error: " + ex.Message);
                    }
                }
                // be nice, dont thrash the wiki
                Thread.Sleep(20000 + rand.Next(10) * 1000); 
            }
            client.Dispose();
            Console.WriteLine($"\n\nCards not found ({notFoundCards.Count})");
            foreach (var card in notFoundCards)
                Console.WriteLine(card.DisplayName);
            Console.WriteLine("\n\nError on");
            foreach (var card in notFoundCards)
                Console.WriteLine(card.DisplayName);
        }

        private static string GetUrlCardName(string displayName)
        {
            string name = displayName.Replace(' ', '_');
            name = name.Replace("'", "%27");
            return name;
        }

        private static MagicDuelsCardList GetCards()
        {
            const string cardDataFileName = "Cards.xml";

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(MagicDuelsCardList));
            using (var reader = new StreamReader(cardDataFileName))
                return (MagicDuelsCardList)xmlSerializer.Deserialize(reader);
        }
    }
}
