using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.XtraBars.Navigation;
using System.ComponentModel;

namespace Accounting.Module.Win.Controllers
{
    [DesignerCategory("Code")]
    public class ExpandAccordionController : WindowController
    {
        public ExpandAccordionController()
        {
            TargetWindowType = WindowType.Main;
        }

        protected override void OnActivated()
        {
            base.OnActivated();

            var showNavigationItemController = Frame.GetController<ShowNavigationItemController>();
            if (showNavigationItemController != null)
            {
                showNavigationItemController.ShowNavigationItemAction.CustomizeControl += ShowNavigationItemAction_CustomizeControl;
            }
            Frame.GetController<ShowNavigationItemController>().ShowNavigationItemAction.CustomizeControl += ShowNavigationItemAction_CustomizeControl;
        }

        protected override void OnDeactivated()
        {
            base.OnDeactivated();

            var showNavigationItemController = Frame.GetController<ShowNavigationItemController>();
            if (showNavigationItemController != null)
            {
                showNavigationItemController.ShowNavigationItemAction.CustomizeControl -= ShowNavigationItemAction_CustomizeControl;
            }
        }

        private void ShowNavigationItemAction_CustomizeControl(object sender, CustomizeControlEventArgs e)
        {
            if (e.Control is AccordionControl accordionControl)
            {
                accordionControl.ExpandAll();
            }
        }
    }
}