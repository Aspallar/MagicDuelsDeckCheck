using System;
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
        };

        public static bool HasReaderFor(string text) 
            => _readers.Any(x => text.StartsWith(x.UrlStart, StringComparison.InvariantCultureIgnoreCase));

        public static IDeckReader GetReader(string text)
        {
            DeckReaderInfo readerInfo = _readers.Where(x => text.StartsWith(x.UrlStart, StringComparison.InvariantCultureIgnoreCase)).Single();
            return (IDeckReader)Activator.CreateInstance(readerInfo.ReaderType);
        }
    }
}
