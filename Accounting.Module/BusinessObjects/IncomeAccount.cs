using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;

namespace Accounting.Module.BusinessObjects
{
    [MapInheritance(MapInheritanceType.ParentTable)]
    public class IncomeAccount : Account, ISupportDefaultVatRate
    {
        public IncomeAccount(Session session) : base(session)
        {
        }

        [DataSourceCriteria("PayableAccount Is Not Null And ReceivableAccount Is Null Or Rate = 0 And PayableAccount Is Null And ReceivableAccount Is Null")]
        [ModelDefault("AllowClear", "False")]
        [ModelDefault("Caption", "Default VAT Rate")]
        [RuleRequiredField("IncomeAccount_DefaultVatRate_RuleRequiredField", DefaultContexts.Save)]
        public VatRate DefaultVatRate
        {
            get => GetPropertyValue<VatRate>(nameof(DefaultVatRate));
            set => SetPropertyValue(nameof(DefaultVatRate), value);
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            Category = AccountCategory.Income;
            DefaultVatRate = Session.FindObject<VatRate>(CriteriaOperator.Parse("PayableAccount Is Null And ReceivableAccount Is Null And Rate = 0"));
            Type = AccountType.Credit;
        }
    }
}