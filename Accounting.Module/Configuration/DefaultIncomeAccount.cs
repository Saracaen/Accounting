using System.Xml.Serialization;

namespace Accounting.Module.Configuration
{
    public class DefaultIncomeAccount : DefaultAccount
    {
        [XmlAttribute("defaultVatRate")]
        public string DefaultVatRate { get; set; }
    }
}