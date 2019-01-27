using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Win.Editors;
using DevExpress.XtraEditors.Controls;
using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Accounting.Module.Win.Editors
{
    public class FilteringEnumPropertyEditor : EnumPropertyEditor
    {
        public FilteringEnumPropertyEditor(Type objectType, IModelMemberViewItem model) : base(objectType, model)
        {
        }

        protected override void OnCustomSetupRepositoryItem(CustomSetupRepositoryItemEventArgs args)
        {
            base.OnCustomSetupRepositoryItem(args);

            var repositoryItemEnumEdit = (RepositoryItemEnumEdit)args.Item;
            for (var i = repositoryItemEnumEdit.Items.Count - 1; i >= 0; i--)
            {
                var comboBoxItem = (ComboBoxItem)repositoryItemEnumEdit.Items[i];
                var memberInfo = comboBoxItem.Value.GetType().GetMember(comboBoxItem.Value.ToString());

                if (memberInfo.FirstOrDefault()?.GetCustomAttribute<BrowsableAttribute>()?.Browsable == false)
                {
                    repositoryItemEnumEdit.Items.RemoveAt(i);
                }
            }
        }
    }
}