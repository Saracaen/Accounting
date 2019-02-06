using System.Xml.Serialization;

namespace Accounting.Module.Configuration
{
    public class DefaultExpenseAccount : DefaultAccount
    {
        [XmlAttribute]
        public string DefaultVatRate { get; set; }

        [XmlAttribute]
        public float PercentageDeductible { get; set; } = 100;
    }
}