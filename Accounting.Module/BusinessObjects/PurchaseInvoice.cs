using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;

namespace Accounting.Module.BusinessObjects
{
    [ImageName("BO_Contract")]
    [MapInheritance(MapInheritanceType.ParentTable)]
    [VisibleInReports]
    public class PurchaseInvoice : Invoice
    {
        public PurchaseInvoice(Session session) : base(session)
        {
        }

        [Association]
        [ImmediatePostData]
        [Persistent("Party")]
        [RuleRequiredField("PurchaseInvoice_Supplier_RuleRequiredField", DefaultContexts.Save)]
        public Supplier Supplier
        {
            get => GetPropertyValue<Supplier>(nameof(Supplier));
            set => SetPropertyValue(nameof(Supplier), value);
        }

        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);

            if (IsLoading)
                return;

            if (propertyName == nameof(Supplier) && Supplier != null)
            {
                IsVatIncluded = Supplier.IsVatIncluded;
                PaymentTerm = Supplier.PaymentTerm;
            }
        }
    }
}