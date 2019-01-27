using DevExpress.ExpressApp;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates.ActionControls;
using DevExpress.ExpressApp.Win.Templates.Bars.ActionControls;
using DevExpress.ExpressApp.Win.Templates.Navigation;
using DevExpress.XtraBars;
using System.ComponentModel;
using System.Linq;

namespace Accounting.Module.Win.Controllers
{
    [DesignerCategory("Code")]
    public class HideNavigationActionsController : WindowController
    {
        protected override void OnActivated()
        {
            base.OnActivated();

            Application.CustomizeTemplate += Application_CustomizeTemplate;

            var actionControlsSiteController = Frame.GetController<ActionControlsSiteController>();
            if (actionControlsSiteController != null)
            {
                actionControlsSiteController.CustomAddActionControlToContainer += ActionControlsSiteController_CustomAddActionControlToContainer;
            }
        }

        protected override void OnDeactivated()
        {
            base.OnDeactivated();

            Application.CustomizeTemplate -= Application_CustomizeTemplate;

            var actionControlsSiteController = Frame.GetController<ActionControlsSiteController>();
            if (actionControlsSiteController != null)
            {
                actionControlsSiteController.CustomAddActionControlToContainer -= ActionControlsSiteController_CustomAddActionControlToContainer;
            }
        }

        private void ActionControlsSiteController_CustomAddActionControlToContainer(object sender, CustomAddActionControlEventArgs e)
        {
            if (e.Action.Id == "ShowNavigationItem" && e.Container is BarLinkActionControlContainer)
            {
                e.Handled = true;
            }
        }

        private void Application_CustomizeTemplate(object sender, CustomizeTemplateEventArgs e)
        {
            if (e.Template is IActionControlsSite actionControlsSite)
            {
                var sidePanelActionControlContainer = actionControlsSite.ActionContainers.OfType<SidePanelActionControlContainer>().FirstOrDefault();
                if (sidePanelActionControlContainer != null)
                {
                    sidePanelActionControlContainer.BarSubItemNavigationPane.Visibility = BarItemVisibility.Never;
                }
            }
        }
    }
}