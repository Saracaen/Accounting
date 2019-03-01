using System.Xml.Serialization;

namespace Accounting.Module.Configuration
{
    public class DefaultCountry
    {
        [XmlAttribute("code")]
        public string Code { get; set; }

        [XmlAttribute("name")]
        public string Name { get; set; }
    }
}