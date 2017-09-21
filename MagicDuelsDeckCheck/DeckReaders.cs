using System;
using System.IO;
using System.Linq;

namespace MagicDuelsDeckCheck
{
    internal static class DeckReaders
    {
        private static DeckReaderInfo[] _readers =
        {
            new DeckReaderInfo { UrlStart = "https://www.magicduelshelper.com/decklist/details", ReaderType = typeof(MagicDuelsHelperDeckReader) },
            new DeckReaderInfo { UrlStart = "http://magicduels.wikia.com/wiki/Decks", ReaderType = typeof(MagicDuelsWikiDeckReader) },
            new DeckReaderInfo { UrlStart = "http://tappedout.net/mtg-decks", ReaderType = typeof(TappedOutDeckReader) },
            new DeckReaderInfo { UrlStart = "https://deckstats.net/decks", ReaderType = typeof(DeckStatsDeckReader) },
        };

        public static bool HasReaderFor(string text)
        {
            if (IsFile(text))
                return text.IndexOfAny(Path.GetInvalidPathChars()) == -1;
            return _readers.Any(x => text.StartsWith(x.UrlStart, StringComparison.InvariantCultureIgnoreCase));
        }

        public static IDeckReader GetReader(string text)
        {
            if (IsFile(text))
                return new TextDeckReader();
            DeckReaderInfo readerInfo = _readers.Where(x => text.StartsWith(x.UrlStart, StringComparison.InvariantCultureIgnoreCase)).Single();
            return (IDeckReader)Activator.CreateInstance(readerInfo.ReaderType);
        }

        private static bool IsFile(string text)
        {
            return text.EndsWith(".txt", StringComparison.InvariantCultureIgnoreCase)
                ||  text.EndsWith(".dec", StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
