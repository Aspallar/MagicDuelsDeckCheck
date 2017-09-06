using System;
using System.Collections.Generic;

namespace MagicDuels
{
    public class MagicDuelsCards : Dictionary<string, CardInfo>
    {
        public MagicDuelsCards() : base(StringComparer.InvariantCultureIgnoreCase) { }
    }
}
