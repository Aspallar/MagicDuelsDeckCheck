using AngleSharp.Dom.Html;
using AngleSharp.Parser.Html;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;

namespace ScrapeWizardDecks
{
    // quick console utility to make deck definition files from articles posted on Wizards of the Coast web site
    // no error handling or finesse
    // sample deck source
    // https://magic.wizards.com/en/articles/archive/magic-digital/magic-duels-decks-soi-and-ogw-2016-06-15
    // Thought there would be more but they turned out to be articles on the pre-made duels decks
    // in mtg proper and nothing to do with Magic Duels.

    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("ScrapeWizardDecks <output folder> <url>");
                return;
            }
            ScrapeDecks(args[0], args[1]);
            Pause();
            Process.Start(args[0] + "\\");
        }

        private static void Pause()
        {
            Console.WriteLine("\nPress any key to continue.");
            Console.ReadKey();
        }

        private static void ScrapeDecks(string outputFolder, string pageUrl)
        {
            IHtmlDocument doc = ReadPage(pageUrl);
            var decks = doc.QuerySelectorAll("div.bean_block_deck_list");
            ShowDecks(decks);
            Pause();
            WriteDeckDefinitionFiles(outputFolder, pageUrl, decks);
        }

        private static void WriteDeckDefinitionFiles(string outputFolder, string pageUrl, AngleSharp.Dom.IHtmlCollection<AngleSharp.Dom.IElement> decks)
        {
            foreach (var deck in decks)
            {
                string deckName = GetTitle(deck);
                Console.WriteLine($"\n{deckName}");
                string fileName = outputFolder + "\\" + deckName + ".txt";
                var cards = deck.QuerySelectorAll("div.sorted-by-overview-container span.row");
                using (StreamWriter writer = new StreamWriter(fileName))
                {
                    writer.WriteLine("// " + deckName);
                    writer.WriteLine("// Source: " + pageUrl);
                    foreach (var card in cards)
                    {
                        int amount = int.Parse(card.QuerySelector("span.card-count").TextContent);
                        string cardName = card.QuerySelector("span.card-name a").TextContent;
                        string line = $"{amount}x {cardName}";
                        Console.WriteLine(line);
                        writer.WriteLine(line);
                    }
                }
            }
        }

        private static void ShowDecks(AngleSharp.Dom.IHtmlCollection<AngleSharp.Dom.IElement> decks)
        {
            Console.WriteLine($"Found {decks.Length} decks on page.");
            foreach (var deck in decks)
            {
                string deckName = GetTitle(deck);
                Console.WriteLine(deckName);
            }
        }

        private static string GetTitle(AngleSharp.Dom.IElement deck)
        {
            return deck.QuerySelector("h4").TextContent;
        }

        private static IHtmlDocument ReadPage(string pageUrl)
        {
            Console.Write("Reading page...");
            string content;
            using (WebClient client = new WebClient())
                content = client.DownloadString(pageUrl);
            Console.WriteLine(" Done.");

            IHtmlDocument doc = ParseContent(content);
            return doc;
        }

        private static IHtmlDocument ParseContent(string content)
        {
            HtmlParser parser = new HtmlParser();
            return parser.Parse(content);
        }
    }
}
