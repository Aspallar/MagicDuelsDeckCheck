using AngleSharp.Dom.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicDuelsDeckCheck
{
    internal interface IDeckReader
    {
        DeckInfo ReadDeck(string deckDefinition);
    }
}
