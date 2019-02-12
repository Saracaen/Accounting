using Accounting.Module.BusinessObjects;
using System.Xml.Serialization;

namespace Accounting.Module.Configuration
{
    public class DefaultVatRate
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("payableAccount")]
        public string PayableAccount { get; set; }

        [XmlAttribute("payableCategory")]
        public VatCategory PayableCategory { get; set; }

        [XmlAttribute("rate")]
        public float Rate { get; set; }

        [XmlAttribute("receivableAccount")]
        public string ReceivableAccount { get; set; }

        [XmlAttribute("receivableCategory")]
        public VatCategory ReceivableCategory { get; set; }
    }
}