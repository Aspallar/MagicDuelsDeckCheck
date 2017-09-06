using System.Linq;

namespace MagicDuels
{
    public static class MagicDuelsHelper
    {
        private static string[] _landNames = { "Swamp", "Mountain", "Forest", "Island", "Plains", "Wastes" };

        public static bool IsBasicLand(string cardName)
        {
            return _landNames.Contains(cardName);
        }
    }
}
