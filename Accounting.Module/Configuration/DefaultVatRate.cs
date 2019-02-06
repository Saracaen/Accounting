using Accounting.Module.BusinessObjects;
using System.Xml.Serialization;

namespace Accounting.Module.Configuration
{
    public class DefaultVatRate
    {
        [XmlAttribute]
        public string Name { get; set; }

        [XmlAttribute]
        public string PayableAccount { get; set; }

        [XmlAttribute]
        public VatCategory PayableCategory { get; set; }

        [XmlAttribute]
        public float Rate { get; set; }

        [XmlAttribute]
        public string ReceivableAccount { get; set; }

        [XmlAttribute]
        public VatCategory ReceivableCategory { get; set; }
    }
}