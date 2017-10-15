using System.Collections.Generic;
using System.Xml.Serialization;

namespace MagicDuels
{
    [XmlRoot(ElementName = "cards")]
    public class MagicDuelsCardList : List<CardInfo>
    {
    }

    [XmlType("card")]
    public class CardInfo
    {
        [XmlAttribute(AttributeName = "name")]
        public string DisplayName { get; set; }

        [XmlAttribute(AttributeName = "magicDuelsId")]
        public int MagicDuelsId { get; set; }

        [XmlAttribute(AttributeName = "set")]
        public string Set { get; set; }

        [XmlAttribute(AttributeName = "wikiImageUrl")]
        public string WikiImageUrl { get; set; }

        [XmlIgnore]
        public int NumberOwned { get; set; }
    }

}
