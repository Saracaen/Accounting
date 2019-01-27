using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System.ComponentModel;

namespace Accounting.Module.BusinessObjects
{
    [DefaultProperty("Identifier")]
    [ImageName("BO_Invoice")]
    [MapInheritance(MapInheritanceType.ParentTable)]
    [VisibleInReports]
    public class SalesInvoice : Invoice
    {
        public SalesInvoice(Session session) : base(session)
        {
        }

        [Association]
        [DataSourceCriteria("Role In ('None', 'Customer')")]
        [Persistent("Party")]
        [RuleRequiredField("SalesInvoice_Customer_RuleRequiredField", DefaultContexts.Save)]
        public Party Customer
        {
            get => GetPropertyValue<Party>(nameof(Customer));
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