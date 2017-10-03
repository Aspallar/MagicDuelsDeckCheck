using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicDuelsDeckCheck
{
    internal static class Utils
    {
        public static bool IsHttpPath(string deckPath)
        {
            return deckPath.StartsWith("http", StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
