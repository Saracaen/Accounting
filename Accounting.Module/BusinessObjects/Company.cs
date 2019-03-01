using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System.ComponentModel;

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
    }
}