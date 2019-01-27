using Accounting.Module.Utils;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Win.Editors;
using DevExpress.Persistent.Validation;
using DevExpress.XtraEditors.Repository;
using System;

namespace Accounting.Module.Win.Editors
{
    public class RangedDatePropertyEditor : DatePropertyEditor
    {
        public RangedDatePropertyEditor(Type objectType, IModelMemberViewItem model) : base(objectType, model)
        {
        }

        protected override void OnCustomSetupRepositoryItem(CustomSetupRepositoryItemEventArgs args)
        {
            base.OnCustomSetupRepositoryItem(args);

            var ruleRangeAttribute = Model.ModelMember.MemberInfo.FindAttribute<RuleRangeAttribute>();
            if (ruleRangeAttribute is IRuleRangeProperties ruleRangeProperties)
            {
                var repositoryItemDateEdit = (RepositoryItemDateEdit)args.Item;

                repositoryItemDateEdit.MinValue = RuleRangeHelper.GetDateTimeValue(CurrentObject, ruleRangeProperties.MinimumValue, ruleRangeProperties.MinimumValueExpression);
                repositoryItemDateEdit.MaxValue = RuleRangeHelper.GetDateTimeValue(CurrentObject, ruleRangeProperties.MaximumValue, ruleRangeProperties.MaximumValueExpression);
            }
        }
    }
}