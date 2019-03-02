using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Accounting.Module.Configuration
{
    public class DefaultCountryConfiguration : IEnumerable<DefaultCountry>
    {
        [XmlElement("country")]
        public DefaultCountry[] Countries { get; set; }

        [XmlAttribute("default")]
        public string Default { get; set; }

        #region IEnumerable

        public IEnumerator<DefaultCountry> GetEnumerator()
        {
            return ((IEnumerable<DefaultCountry>)Countries).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<DefaultCountry>)Countries).GetEnumerator();
        }

        #endregion IEnumerable
    }
}