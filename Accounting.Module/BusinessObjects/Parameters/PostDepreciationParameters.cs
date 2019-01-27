using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Validation;

namespace Accounting.Module.BusinessObjects.Parameters
{
    [DomainComponent]
    [ModelDefault("Caption", "Post Depreciation")]
    public class PostDepreciationParameters
    {
        [RuleRequiredField("PostDepreciationParameters_AssetAccount_RuleRequiredField", DefaultContexts.Save)]
        public AssetAccount AssetAccount { get; set; }

        [RuleRequiredField("PostDepreciationParameters_DepreciationExpenseAccount_RuleRequiredField", DefaultContexts.Save)]
        public DepreciationExpenseAccount DepreciationExpenseAccount { get; set; }
    }
}