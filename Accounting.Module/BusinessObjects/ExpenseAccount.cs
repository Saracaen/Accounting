using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;

namespace Accounting.Module.BusinessObjects
{
    [MapInheritance(MapInheritanceType.ParentTable)]
    public class ExpenseAccount : Account, ISupportDefaultVatRate
    {
        public ExpenseAccount(Session session) : base(session)
        {
        }

        [DataSourceCriteria("ReceivableAccount Is Not Null Or Rate = 0 And PayableAccount Is Null And ReceivableAccount Is Null")]
        [ModelDefault("AllowClear", "False")]
        [ModelDefault("Caption", "Default VAT Rate")]
        [RuleRequiredField("ExpenseAccount_DefaultVatRate_RuleRequiredField", DefaultContexts.Save)]
        public VatRate DefaultVatRate
        {
            get => GetPropertyValue<VatRate>(nameof(DefaultVatRate));
            set => SetPropertyValue(nameof(DefaultVatRate), value);
        }

        [Appearance("PercentageDeductible", "JournalEntryLines.Count() > 0", Enabled = false)]
        [RuleRange("ExpenseAccount_DeductiblePercentage_RuleRange", DefaultContexts.Save, 0, 100)]
        public float PercentageDeductible
        {
            get => GetPropertyValue<float>(nameof(PercentageDeductible));
            set => SetPropertyValue(nameof(PercentageDeductible), value);
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            Category = AccountCategory.Expense;
            DefaultVatRate = Session.FindObject<VatRate>(CriteriaOperator.Parse("PayableAccount Is Null And ReceivableAccount Is Null And Rate = 0"));
            PercentageDeductible = 100;
            Type = AccountType.Debit;
        }
    }
}