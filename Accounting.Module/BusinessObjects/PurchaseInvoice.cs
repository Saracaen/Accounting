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
            set => SetSupplier(value);
        }

        private void SetSupplier(Supplier value)
        {
            if (SetPropertyValue(nameof(Supplier), value))
            {
                if (IsLoading || IsSaving || value == null)
                    return;

                IsVatIncluded = value.IsVatIncluded;
                PaymentTerm = value.PaymentTerm;
            }
        }
    }
}