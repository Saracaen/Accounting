using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;

namespace Accounting.Module.BusinessObjects
{
    [ImageName("BO_Invoice")]
    [MapInheritance(MapInheritanceType.ParentTable)]
    [VisibleInReports]
    public class SalesInvoice : Invoice
    {
        public SalesInvoice(Session session) : base(session)
        {
        }

        [Association]
        [ImmediatePostData]
        [Persistent("Party")]
        [RuleRequiredField("SalesInvoice_Customer_RuleRequiredField", DefaultContexts.Save)]
        public Customer Customer
        {
            get => GetPropertyValue<Customer>(nameof(Customer));
            set => SetCustomer(value);
        }

        private void SetCustomer(Customer value)
        {
            if (SetPropertyValue(nameof(Customer), value))
            {
                if (IsLoading || value == null)
                    return;

                IsVatIncluded = value.IsVatIncluded;
                PaymentTerm = value.PaymentTerm;
            }
        }
    }
}