using System;
using System.IO;
using System.Xml.Serialization;

namespace Accounting.Module.Configuration
{
    [XmlRoot("configuration")]
    public class DefaultConfiguration
    {
        private static readonly Lazy<DefaultConfiguration> Lazy = new Lazy<DefaultConfiguration>(() => Load("Accounting.Defaults.config"));

        public static DefaultConfiguration Instance
        {
            get => Lazy.Value;
        }

        [XmlElement("accounts")]
        public DefaultAccountConfiguration Accounts { get; set; }

        [XmlElement("countries")]
        public DefaultCountryConfiguration Countries { get; set; }

        [XmlArray("paymentTerms")]
        [XmlArrayItem("paymentTerm")]
        public DefaultPaymentTerm[] PaymentTerms { get; set; }

        [XmlArray("vatRates")]
        [XmlArrayItem("vatRate")]
        public DefaultVatRate[] VatRates { get; set; }

        public static DefaultConfiguration Load(string fileName)
        {
            using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                var serializer = new XmlSerializer(typeof(DefaultConfiguration));
                return (DefaultConfiguration)serializer.Deserialize(stream);
            }
        }
    }
}