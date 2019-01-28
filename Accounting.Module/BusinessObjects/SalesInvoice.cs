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
        [Persistent("Party")]
        [RuleRequiredField("SalesInvoice_Customer_RuleRequiredField", DefaultContexts.Save)]
        public Customer Customer
        {
            get => GetPropertyValue<Customer>(nameof(Customer));
            set => SetPropertyValue(nameof(Customer), value);
        }

        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);

            if (IsLoading)
                return;

            if (propertyName == nameof(Customer) && Customer != null)
            {
                IsVatIncluded = Customer.IsVatIncluded;
                PaymentTerm = Customer.PaymentTerm;
            }
        }
    }
}