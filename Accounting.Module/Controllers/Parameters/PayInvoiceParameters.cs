using Accounting.Module.BusinessObjects;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System;
using System.ComponentModel;

namespace Accounting.Module.Controllers.Parameters
{
    [DomainComponent]
    [ModelDefault("Caption", "Pay Invoice")]
    public class PayInvoiceParameters
    {
        public PayInvoiceParameters(Invoice invoice)
        {
            if (invoice == null)
                throw new ArgumentNullException(nameof(invoice));

            Invoice = invoice;
        }

        [DataSourceCriteriaProperty("AccountCriteria")]
        [RuleRequiredField("PayInvoiceParameters_Account_RuleRequiredField", DefaultContexts.Save)]
        public Account Account { get; set; }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public CriteriaOperator AccountCriteria
        {
            get
            {
                switch (Invoice)
                {
                    case PurchaseInvoice _:
                        return CriteriaOperator.Parse("IsExactType(This, ?) Or IsExactType(This, ?) Or IsExactType(This, ?) Or IsExactType(This, ?)", typeof(BankAccount).FullName, typeof(CashAccount).FullName, typeof(CreditCardAccount).FullName, typeof(PrivateAccount).FullName);

                    case SalesInvoice _:
                        return CriteriaOperator.Parse("IsExactType(This, ?) Or IsExactType(This, ?)", typeof(BankAccount).FullName, typeof(CashAccount).FullName);

                    default:
                        throw new InvalidOperationException(CaptionHelper.GetLocalizedText(@"Exceptions\UserVisibleExceptions", "UnsupportedInvoiceClass"));
                }
            }
        }

        [RuleRange("PayInvoiceParameters_Amount_RuleRange", DefaultContexts.Save, "Iif(Invoice.DueAmount < 0, Invoice.DueAmount, 0)", "Iif(Invoice.DueAmount < 0, 0, Invoice.DueAmount)", ParametersMode.Expression)]
        public decimal Amount { get; set; }

        [RuleRequiredField("PayInvoiceParameters_Date_RuleRequiredField", DefaultContexts.Save)]
        public DateTime Date { get; set; } = DateTime.Today;

        [RuleRequiredField("PayInvoiceParameters_Description_RuleRequiredField", DefaultContexts.Save)]
        public string Description { get; set; }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public Invoice Invoice { get; }
    }
}