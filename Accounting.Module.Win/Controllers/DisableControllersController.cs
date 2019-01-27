using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Win.SystemModule;
using System.ComponentModel;

namespace Accounting.Module.Win.Controllers
{
    [DesignerCategory("Code")]
    public class DisableControllersController : WindowController
    {
        protected override void OnActivated()
        {
            base.OnActivated();

            DisableController<ChooseSkinController>();
            DisableController<ConfigureSkinController>();
            DisableController<EditModelController>();
        }

        private void DisableController<T>() where T : Controller
        {
            var controller = Frame.GetController<T>();
            if (controller != null)
            {
                controller.Active[Name] = false;
            }
        }
    }
}