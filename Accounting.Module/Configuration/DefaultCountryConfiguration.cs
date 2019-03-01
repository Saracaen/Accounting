using System.Collections.Generic;
using System.Xml.Serialization;

namespace Accounting.Module.Configuration
{
    public class DefaultCountryConfiguration
    {
        [XmlElement("country")]
        public List<DefaultCountry> Countries { get; set; } = new List<DefaultCountry>();

        [XmlAttribute("default")]
        public string Default { get; set; }
    }
}