using System.Xml.Serialization;

namespace Accounting.Module.Configuration
{
    public class DefaultAccount
    {
        [XmlAttribute("name")]
        public string Name { get; set; }
    }
}