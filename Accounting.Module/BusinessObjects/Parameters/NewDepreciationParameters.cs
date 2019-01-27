using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Validation;
using System;

namespace Accounting.Module.BusinessObjects.Parameters
{
    [DomainComponent]
    [ModelDefault("Caption", "New Depreciation")]
    [RuleCriteria("NewDepreciationParameters_ResidualValueValue_RuleCriteria", DefaultContexts.Save, "Value > ResidualValue", "The value must be greater than the residual value.")]
    public class NewDepreciationParameters
    {
        [ModelDefault("DisplayFormat", "{0:D0}")]
        [ModelDefault("EditMask", "D")]
        [RuleRange("NewDepreciationParameters_DepreciationTime_RuleRange", DefaultContexts.Save, 1, 100)]
        public int DepreciationTime { get; set; } = 1;

        [RuleRequiredField("NewDepreciationParameters_Description_RuleRequiredField", DefaultContexts.Save)]
        public string Description { get; set; }

        [RuleRange("NewDepreciationParameters_ResidualValue_RuleRange", DefaultContexts.Save, 0, long.MaxValue)]
        public decimal ResidualValue { get; set; }

        [RuleRequiredField("NewDepreciationParameters_StartDate_RuleRequiredField", DefaultContexts.Save)]
        public DateTime StartDate { get; set; } = DateTime.Today;

        public DepreciationType Type { get; set; }

        [RuleRange("NewDepreciationParameters_Value_RuleRange", DefaultContexts.Save, 450, long.MaxValue)]
        public decimal Value { get; set; }
    }
}