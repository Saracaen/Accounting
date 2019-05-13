using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;

namespace Accounting.Module.BusinessObjects.Parameters
{
    [DomainComponent]
    [ModelDefault("Caption", "Correct Purchase Invoice")]
    public class CorrectPurchaseInvoiceParameters
    {
        [DataSourceCriteria("Category = 'Expense' Or IsExactType(This, 'Accounting.Module.BusinessObjects.AssetAccount') Or IsExactType(This, 'Accounting.Module.BusinessObjects.RoundingDifferencesAccount')")]
        public Account Account { get; set; }

        public decimal Amount { get; set; }

        [RuleRequiredField("CorrectPurchaseInvoiceParameters_Description_RuleRequiredField", DefaultContexts.Save)]
        public string Description { get; set; }

        [ModelDefault("Caption", "VAT")]
        public decimal Vat { get; set; }

        [DataSourceCriteria("ReceivableAccount Is Not Null And ReceivableCategory <> 'SmallBusinessScheme'")]
        [ModelDefault("Caption", "VAT Rate")]
        public VatRate VatRate { get; set; }
    }
}