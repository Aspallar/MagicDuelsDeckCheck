namespace MagicDuelsDeckCheck
{
    internal class SectionData
    {
        public DeckInfo Deck { get; set; }
        public string NeededMarkup { get; set; }
        public int TotalNeeded { get; set; }
        public string UnknownMarkup { get; set; }
        public string PossessedMarkup { get; set; }
        public int TotalPossessed { get; set; }
        public string DeckPath { get; set; }
        public string TemplatePath { get; set; }
    }
}
