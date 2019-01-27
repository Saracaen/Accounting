using Accounting.Module.BusinessObjects;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Win.Editors;
using DevExpress.XtraEditors.Controls;
using System;
using System.Linq;

namespace Accounting.Module.Win.Editors
{
    public class PartyRolePropertyEditor : EnumPropertyEditor
    {
        public PartyRolePropertyEditor(Type objectType, IModelMemberViewItem model) : base(objectType, model)
        {
        }

        protected override void OnCustomSetupRepositoryItem(CustomSetupRepositoryItemEventArgs args)
        {
            base.OnCustomSetupRepositoryItem(args);

            var party = CurrentObject as Party;
            var repositoryItemEnumEdit = (RepositoryItemEnumEdit)args.Item;

            if (party?.SalesInvoices.Count > 0 || Id == "CustomerRole")
            {
                repositoryItemEnumEdit.Items.Remove(repositoryItemEnumEdit.Items.Cast<ComboBoxItem>().First(x => (PartyRole)x.Value == PartyRole.Supplier));
            }

            if (party?.PurchaseInvoices.Count > 0 || Id == "SupplierRole")
            {
                repositoryItemEnumEdit.Items.Remove(repositoryItemEnumEdit.Items.Cast<ComboBoxItem>().First(x => (PartyRole)x.Value == PartyRole.Customer));
            }
        }
    }
}