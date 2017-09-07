using System.Xml.Serialization;

namespace MagicDuelsDeckCheck
{
    [XmlType("card")]
    public class NameCorrection
    {
        [XmlAttribute("webSiteName")]
        public string WebSiteName { get; set; }

        [XmlAttribute("correctName")]
        public string CorrectName { get; set; }
    }
}