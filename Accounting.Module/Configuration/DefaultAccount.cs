using System.Xml.Serialization;

namespace Accounting.Module.Configuration
{
    public class DefaultAccount
    {
        [XmlAttribute]
        public string Name { get; set; }
    }
}