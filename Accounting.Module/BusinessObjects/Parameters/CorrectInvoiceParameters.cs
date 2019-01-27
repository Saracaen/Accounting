using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System;
using System.ComponentModel;

namespace Accounting.Module.BusinessObjects.Parameters
{
    [DomainComponent]
    [ModelDefault("Caption", "Correct Invoice")]
    public class CorrectInvoiceParameters
    {
        [DataSourceCriteriaProperty("AccountCriteria")]
        public Account Account { get; set; }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public CriteriaOperator AccountCriteria
        {
            get
            {
                if (Invoice != null)
                {
                    switch (Invoice)
                    {
                        case PurchaseInvoice _:
                            switch (Invoice.Type)
                            {
                                case InvoiceType.Invoice:
                                case InvoiceType.CreditNote:
                                    return CriteriaOperator.Parse("Category = 'Expense' Or IsExactType(This, ?) Or IsExactType(This, ?)", typeof(AssetAccount).FullName, typeof(RoundingDifferencesAccount).FullName);

                                default:
                                    throw new InvalidOperationException(CaptionHelper.GetLocalizedText(@"Exceptions\UserVisibleExceptions", "UnsupportedInvoiceType"));
                            }

                        case SalesInvoice _:
                            switch (Invoice.Type)
                            {
                                case InvoiceType.Invoice:
                                case InvoiceType.CreditNote:
                                    return CriteriaOperator.Parse("Category = 'Income' Or IsExactType(This, ?)", typeof(RoundingDifferencesAccount).FullName);

                                default:
                                    throw new InvalidOperationException(CaptionHelper.GetLocalizedText(@"Exceptions\UserVisibleExceptions", "UnsupportedInvoiceType"));
                            }

                        default:
                            throw new InvalidOperationException(CaptionHelper.GetLocalizedText(@"Exceptions\UserVisibleExceptions", "UnsupportedInvoiceClass"));
                    }
                }
                return null;
            }
        }

        public decimal Amount { get; set; }

        [RuleRequiredField("CorrectInvoiceParameters_Description_RuleRequiredField", DefaultContexts.Save)]
        public string Description { get; set; }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public Invoice Invoice { get; set; }

        [ModelDefault("Caption", "VAT")]
        public decimal Vat { get; set; }

        [DataSourceCriteriaProperty("VatRateCriteria")]
        [ModelDefault("Caption", "VAT Rate")]
        public VatRate VatRate { get; set; }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public CriteriaOperator VatRateCriteria
        {
            get
            {
                if (Invoice != null)
                {
                    switch (Invoice)
                    {
                        case PurchaseInvoice _:
                            return CriteriaOperator.Parse("ReceivableAccount Is Not Null And ReceivableCategory <> 'SmallBusinessScheme'");

                        case SalesInvoice _:
                            return CriteriaOperator.Parse("PayableAccount Is Not Null And ReceivableAccount Is Null");

                        default:
                            throw new InvalidOperationException(CaptionHelper.GetLocalizedText(@"Exceptions\UserVisibleExceptions", "UnsupportedInvoiceClass"));
                    }
                }
                return null;
            }
        }
    }
}