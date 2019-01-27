using Accounting.Module.BusinessObjects;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Utils;
using System.ComponentModel;

namespace Accounting.Module.Controllers
{
    [DesignerCategory("Code")]
    public class NewPartyController : ObjectViewController<ObjectView, Party>
    {
        protected NewObjectViewController NewObjectViewController { get; set; }

        protected override void OnActivated()
        {
            base.OnActivated();

            NewObjectViewController = Frame.GetController<NewObjectViewController>();

            if (NewObjectViewController != null)
            {
                NewObjectViewController.ObjectCreated += NewObjectViewController_ObjectCreated;

                if (View.Id == "Party_DetailView" || View.Id == "Party_ListView" || View.Id == "Party_LookupListView")
                {
                    NewObjectViewController.NewObjectAction.Items.Add(new ChoiceActionItem("Customer", CaptionHelper.GetLocalizedText("Texts", "Customer"), typeof(Party)) { ImageName = "BO_Customer" });
                    NewObjectViewController.NewObjectAction.Items.Add(new ChoiceActionItem("Supplier", CaptionHelper.GetLocalizedText("Texts", "Supplier"), typeof(Party)) { ImageName = "BO_Customer" });
                }
            }
        }

        protected override void OnDeactivated()
        {
            base.OnDeactivated();

            if (NewObjectViewController != null)
            {
                NewObjectViewController.ObjectCreated -= NewObjectViewController_ObjectCreated;
                NewObjectViewController = null;
            }
        }

        private void NewObjectViewController_ObjectCreated(object sender, ObjectCreatedEventArgs e)
        {
            if (e.CreatedObject is Party party)
            {
                if (NewObjectViewController.NewObjectAction.SelectedItem.Id == "Customer" || View.Id == "SalesInvoice_Customer_LookupListView")
                {
                    party.Role = PartyRole.Customer;
                }
                else if (NewObjectViewController.NewObjectAction.SelectedItem.Id == "Supplier" || View.Id == "PurchaseInvoice_Supplier_LookupListView")
                {
                    party.Role = PartyRole.Supplier;
                }
            }
        }
    }
}