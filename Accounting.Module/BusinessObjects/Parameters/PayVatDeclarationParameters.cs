using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Validation;
using System;

namespace Accounting.Module.BusinessObjects.Parameters
{
    [DomainComponent]
    [ModelDefault("Caption", "Pay VAT Declaration")]
    public class PayVatDeclarationParameters
    {
        [RuleRequiredField("PayVatDeclarationParameters_Account_RuleRequiredField", DefaultContexts.Save)]
        public BankAccount Account { get; set; }

        [ModelDefault("AllowEdit", "False")]
        public decimal Amount { get; set; }

        [RuleRequiredField("PayVatDeclarationParameters_Date_RuleRequiredField", DefaultContexts.Save)]
        public DateTime Date { get; set; } = DateTime.Today;

        [RuleRequiredField("PayVatDeclarationParameters_Description_RuleRequiredField", DefaultContexts.Save)]
        public string Description { get; set; }
    }
}