using System.Xml.Serialization;

namespace Accounting.Module.Configuration
{
    public class DefaultPaymentTerm
    {
        [XmlAttribute]
        public string Name { get; set; }

        [XmlAttribute]
        public int Term { get; set; }
    }
}