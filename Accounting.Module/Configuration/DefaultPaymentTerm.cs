using System.Xml.Serialization;

namespace Accounting.Module.Configuration
{
    public class DefaultPaymentTerm
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("term")]
        public int Term { get; set; }
    }
}