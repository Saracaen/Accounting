using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Utils.Design;
using DevExpress.Xpo;
using System.ComponentModel;
using System.Drawing;

namespace Accounting.Module.BusinessObjects
{
    [DefaultProperty("Name")]
    [ImageName("BO_Organization")]
    [ModelDefault("ObjectCaptionFormat", "")]
    public class Company : Organization
    {
        public Company(Session session) : base(session)
        {
        }

        public string AccountNumber
        {
            get => GetPropertyValue<string>(nameof(AccountNumber));
            set => SetPropertyValue(nameof(AccountNumber), value);
        }

        public object Logo
        {
            get => GetLogo(LogoData);
        }

        [Delayed]
        [ImageEditor]
        [ModelDefault("Caption", "Logo")]
        [Persistent("Logo")]
        [VisibleInReports(false)]
        public byte[] LogoData
        {
            get => GetDelayedPropertyValue<byte[]>(nameof(LogoData));
            set => SetDelayedPropertyValue(nameof(LogoData), value);
        }

        [ModelDefault("Caption", "VAT Declaration Type")]
        public VatDeclarationType VatDeclarationType
        {
            get => GetPropertyValue<VatDeclarationType>(nameof(VatDeclarationType));
            set => SetPropertyValue(nameof(VatDeclarationType), value);
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            VatDeclarationType = VatDeclarationType.Quarterly;
        }

        private object GetLogo(byte[] bytes)
        {
            if (bytes == null)
                return null;

            try
            {
                return new BinaryTypeConverter().ConvertFrom(LogoData);
            }
            catch
            {
                return new ImageConverter().ConvertFrom(LogoData);
            }
        }
    }
}