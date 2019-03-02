using Accounting.Module.Configuration;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System.ComponentModel;

namespace Accounting.Module.BusinessObjects
{
    [Appearance("Actions", AppearanceItemType.Action, "True", TargetItems = "Delete;New;SaveAndNew", Visibility = ViewItemVisibility.Hide)]
    [DefaultProperty("Name")]
    [ImageName("BO_Organization")]
    [MapInheritance(MapInheritanceType.ParentTable)]
    public class Company : Party
    {
        public Company(Session session) : base(session)
        {
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

            Country = Session.FindObject<Country>(new BinaryOperator("Code", DefaultConfiguration.Instance.Countries.Default));
            PaymentTerm = Session.FindObject<PaymentTerm>(new BinaryOperator("Term", 30));
            VatDeclarationType = VatDeclarationType.Quarterly;
        }
    }
}