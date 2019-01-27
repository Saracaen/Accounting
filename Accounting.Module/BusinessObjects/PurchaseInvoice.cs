using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System.ComponentModel;

namespace Accounting.Module.BusinessObjects
{
    [DefaultProperty("Identifier")]
    [ImageName("BO_Contract")]
    [MapInheritance(MapInheritanceType.ParentTable)]
    [VisibleInReports]
    public class PurchaseInvoice : Invoice
    {
        public PurchaseInvoice(Session session) : base(session)
        {
        }

        [Association]
        [DataSourceCriteria("Role In ('None', 'Supplier')")]
        [Persistent("Party")]
        [RuleRequiredField("PurchaseInvoice_Supplier_RuleRequiredField", DefaultContexts.Save)]
        public Party Supplier
        {
            get => GetPropertyValue<Party>(nameof(Supplier));
            set => SetPropertyValue(nameof(Supplier), value);
        }

        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);

            if (IsLoading)
                return;

            if (propertyName == nameof(Supplier) && Supplier != null)
            {
                PaymentTerm = Supplier.PaymentTerm;
            }
        }
    }
}