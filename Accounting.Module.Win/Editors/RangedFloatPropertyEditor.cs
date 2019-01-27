using Accounting.Module.Win.Utils;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Win.Editors;
using DevExpress.XtraEditors.Repository;
using System;

namespace Accounting.Module.Win.Editors
{
    public class RangedFloatPropertyEditor : FloatPropertyEditor
    {
        public RangedFloatPropertyEditor(Type objectType, IModelMemberViewItem model) : base(objectType, model)
        {
        }

        protected override void OnCustomSetupRepositoryItem(CustomSetupRepositoryItemEventArgs args)
        {
            base.OnCustomSetupRepositoryItem(args);
            RepositoryItemSpinEditHelper.SetupRepositoryItem((RepositoryItemSpinEdit)args.Item, Model.ModelMember.MemberInfo, CurrentObject);
        }
    }
}