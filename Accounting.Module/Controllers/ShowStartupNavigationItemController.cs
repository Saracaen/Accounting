using Accounting.Module.BusinessObjects;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.SystemModule;
using System;
using System.ComponentModel;

namespace Accounting.Module.Controllers
{
    [DesignerCategory("Code")]
    public class ShowStartupNavigationItemController : WindowController
    {
        private ShowNavigationItemController showNavigationItemController;

        public ShowStartupNavigationItemController()
        {
            TargetWindowType = WindowType.Main;
        }

        protected override void OnActivated()
        {
            base.OnActivated();

            using (var objectSpace = Application.CreateObjectSpace(typeof(Company)))
            {
                var company = objectSpace.FindObject<Company>(null);
                if (company == null)
                {
                    this.showNavigationItemController = Frame.GetController<ShowNavigationItemController>();
                    if (this.showNavigationItemController != null)
                    {
                        this.showNavigationItemController.CustomShowNavigationItem += ShowNavigationItemController_CustomShowNavigationItem;
                    }
                }
                else
                {
                    Application.Title = company.Name;
                }
            }
        }

        protected override void OnDeactivated()
        {
            base.OnDeactivated();

            if (this.showNavigationItemController != null)
            {
                this.showNavigationItemController.CustomShowNavigationItem -= ShowNavigationItemController_CustomShowNavigationItem;
            }
        }

        private void ObjectSpace_Committed(object sender, EventArgs e)
        {
            var objectSpace = (IObjectSpace)sender;
            var showNavigationItemAction = this.showNavigationItemController.ShowNavigationItemAction;

            showNavigationItemAction.SelectedItem = showNavigationItemAction.Items.FindItemByID("Settings").Items.FindItemByID("Company");
            showNavigationItemAction.Enabled[Name] = true;

            objectSpace.Committed -= ObjectSpace_Committed;
        }

        private void ShowNavigationItemController_CustomShowNavigationItem(object sender, CustomShowNavigationItemEventArgs e)
        {
            this.showNavigationItemController.CustomShowNavigationItem -= ShowNavigationItemController_CustomShowNavigationItem;
            this.showNavigationItemController.ShowNavigationItemAction.Enabled[Name] = false;

            var objectSpace = Application.CreateObjectSpace(typeof(Company));
            var company = objectSpace.CreateObject<Company>();
            var detailView = Application.CreateDetailView(objectSpace, company, true);

            detailView.ViewEditMode = ViewEditMode.Edit;
            objectSpace.Committed += ObjectSpace_Committed;

            e.ActionArguments.ShowViewParameters.CreatedView = detailView;
            e.Handled = true;
        }
    }
}