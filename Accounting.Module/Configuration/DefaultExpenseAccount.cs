using System.Xml.Serialization;

namespace Accounting.Module.Configuration
{
    public class DefaultExpenseAccount : DefaultAccount
    {
        [XmlAttribute("defaultVatRate")]
        public string DefaultVatRate { get; set; }

        [XmlAttribute("percentageDeductible")]
        public float PercentageDeductible { get; set; }
    }
}