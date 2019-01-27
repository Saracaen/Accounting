using Accounting.Module.Utils;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Validation;
using DevExpress.XtraEditors.Repository;
using System;

namespace Accounting.Module.Win.Utils
{
    public static class RepositoryItemSpinEditHelper
    {
        public static void SetupRepositoryItem(RepositoryItemSpinEdit repositoryItemSpinEdit, IMemberInfo memberInfo, object currentObject)
        {
            if (repositoryItemSpinEdit == null)
                throw new ArgumentNullException(nameof(repositoryItemSpinEdit));
            if (memberInfo == null)
                throw new ArgumentNullException(nameof(memberInfo));

            var ruleRangeAttribute = memberInfo.FindAttribute<RuleRangeAttribute>();
            if (ruleRangeAttribute is IRuleRangeProperties ruleRangeProperties)
            {
                repositoryItemSpinEdit.MinValue = RuleRangeHelper.GetDecimalValue(currentObject, ruleRangeProperties.MinimumValue, ruleRangeProperties.MinimumValueExpression);
                repositoryItemSpinEdit.MaxValue = RuleRangeHelper.GetDecimalValue(currentObject, ruleRangeProperties.MaximumValue, ruleRangeProperties.MaximumValueExpression);
            }
        }
    }
}