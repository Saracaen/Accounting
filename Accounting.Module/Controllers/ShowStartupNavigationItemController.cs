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
        public ShowStartupNavigationItemController()
        {
            TargetWindowType = WindowType.Main;
        }

        protected override void OnActivated()
        {
            base.OnActivated();

            using (var objectSpace = Application.CreateObjectSpace(typeof(Company)))
            {
                if (objectSpace.GetObjectsCount(typeof(Company), null) == 0)
                {
                    Frame.GetController<ShowNavigationItemController>().CustomShowNavigationItem += ShowNavigationItemController_CustomShowNavigationItem;
                }
            }
        }

        private void ObjectSpace_Committed(object sender, EventArgs e)
        {
            var objectSpace = (IObjectSpace)sender;
            objectSpace.Committed -= ObjectSpace_Committed;
            objectSpace.Disposed -= ObjectSpace_Disposed;

            var showNavigationItemAction = Frame.GetController<ShowNavigationItemController>().ShowNavigationItemAction;
            showNavigationItemAction.SelectedItem = showNavigationItemAction.Items.FindItemByID("Settings").Items.FindItemByID("Company");
            showNavigationItemAction.Enabled[Name] = true;
        }

        private void ObjectSpace_Disposed(object sender, EventArgs e)
        {
            Application?.Exit();
        }

        private void ShowNavigationItemController_CustomShowNavigationItem(object sender, CustomShowNavigationItemEventArgs e)
        {
            var showNavigationItemController = (ShowNavigationItemController)sender;
            showNavigationItemController.CustomShowNavigationItem -= ShowNavigationItemController_CustomShowNavigationItem;
            showNavigationItemController.ShowNavigationItemAction.Enabled[Name] = false;

            var objectSpace = Application.CreateObjectSpace();
            objectSpace.Committed += ObjectSpace_Committed;
            objectSpace.Disposed += ObjectSpace_Disposed;

            var detailView = Application.CreateDetailView(objectSpace, objectSpace.CreateObject<Company>(), true);
            detailView.ViewEditMode = ViewEditMode.Edit;

            e.ActionArguments.ShowViewParameters.CreatedView = detailView;
            e.Handled = true;
        }
    }
}