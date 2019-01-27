using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;

namespace Accounting.Module.BusinessObjects
{
    public class VatDeclarationLine : BaseObject
    {
        public VatDeclarationLine(Session session) : base(session)
        {
        }

        [Appearance("Amount", "Category In ('DeliveriesOrServicesReverseCharged', 'DeliveriesOrServicesFromCountriesOutsideTheEuropeanUnion', 'DeliveriesOrServicesFromCountriesWithinTheEuropeanUnion', 'InputVat', 'SmallBusinessScheme')", Enabled = false)]
        [ImmediatePostData]
        [ModelDefault("DisplayFormat", "{0:C0}")]
        [ModelDefault("EditMask", "C0")]
        public decimal Amount
        {
            get => GetPropertyValue<decimal>(nameof(Amount));
            set => SetPropertyValue(nameof(Amount), value);
        }

        [ModelDefault("AllowEdit", "False")]
        public VatCategory Category
        {
            get => GetPropertyValue<VatCategory>(nameof(Category));
            set => SetPropertyValue(nameof(Category), value);
        }

        [Appearance("Vat", "Category In ('DeliveriesOrServicesUntaxed', 'DeliveriesToCountriesOutsideTheEuropeanUnion', 'DeliveriesOrServicesToCountriesWithinTheEuropeanUnion', 'InstallationOrDistanceSalesWithinTheEuropeanUnion')", Enabled = false)]
        [ImmediatePostData]
        [ModelDefault("Caption", "VAT")]
        [ModelDefault("DisplayFormat", "{0:C0}")]
        [ModelDefault("EditMask", "C0")]
        public decimal Vat
        {
            get => GetPropertyValue<decimal>(nameof(Vat));
            set => SetPropertyValue(nameof(Vat), value);
        }

        [Association]
        public VatDeclaration VatDeclaration
        {
            get => GetPropertyValue<VatDeclaration>(nameof(VatDeclaration));
            set => SetPropertyValue(nameof(VatDeclaration), value);
        }
    }
}