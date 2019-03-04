using System.Xml.Serialization;

namespace Accounting.Module.Configuration
{
    public class DefaultCountryConfiguration
    {
        [XmlElement("country")]
        public DefaultCountry[] Countries { get; set; }

        [XmlAttribute("default")]
        public string Default { get; set; }
    }
}