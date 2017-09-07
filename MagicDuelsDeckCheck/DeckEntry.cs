namespace MagicDuelsDeckCheck
{
    internal class DeckEntry
    {
        public int Required { get; set; }
        public int Owned { get; set; } 
        public string CardName { get; set; }
        public bool Unknown { get; set; }
        public string Set { get; set; }
        public string CorrectName { get; set; }
        public int Shortfall => Required - Owned;
    }
}
