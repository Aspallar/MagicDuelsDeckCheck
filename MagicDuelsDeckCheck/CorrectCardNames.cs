using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace MagicDuelsDeckCheck
{
    internal class CorrectCardNames
    {
        public const string FileName = "CorrectCardNames.xml";
        private List<NameCorrection> nameCorrections;

        public void Initialize()
        {
            if (File.Exists(FileName))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<NameCorrection>), new XmlRootAttribute("cards"));
                using (StreamReader reader = new StreamReader(FileName))
                    nameCorrections = (List<NameCorrection>)serializer.Deserialize(reader);
            }
            else
            {
                nameCorrections = new List<NameCorrection>();
            }
        }

        public string GetCorrectName(string name)
        {
            NameCorrection correction = nameCorrections.Find(x => x.WebSiteName == name);
            return correction == null ? "" : correction.CorrectName;
        }
    }
}