using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Accounting.Module.Configuration
{
    [XmlRoot("configuration")]
    public class DefaultConfiguration
    {
        [XmlElement("accounts")]
        public DefaultAccountConfiguration Accounts { get; set; } = new DefaultAccountConfiguration();

        [XmlArray("paymentTerms")]
        [XmlArrayItem("paymentTerm")]
        public List<DefaultPaymentTerm> PaymentTerms { get; set; } = new List<DefaultPaymentTerm>();

        [XmlArray("vatRates")]
        [XmlArrayItem("vatRate")]
        public List<DefaultVatRate> VatRates { get; set; } = new List<DefaultVatRate>();

        public static DefaultConfiguration Load(string fileName)
        {
            using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                var serializer = new XmlSerializer(typeof(DefaultConfiguration));
                return (DefaultConfiguration)serializer.Deserialize(stream);
            }
        }

        public void Save(string fileName)
        {
            using (var stream = new FileStream(fileName, FileMode.Create))
            {
                var serializer = new XmlSerializer(typeof(DefaultConfiguration));
                serializer.Serialize(stream, this);
            }
        }
    }
}