using DevExpress.ExpressApp;
using DevExpress.ExpressApp.SystemModule;
using System.ComponentModel;

namespace Accounting.Module.Win.Controllers
{
    [DesignerCategory("Code")]
    public class DisableResetViewSettingsController : ViewController<DetailView>
    {
        public DisableResetViewSettingsController()
        {
            TargetViewNesting = Nesting.Root;
        }

        protected override void OnActivated()
        {
            base.OnActivated();

            var resetViewSettingsController = Frame.GetController<ResetViewSettingsController>();
            if (resetViewSettingsController != null)
            {
                resetViewSettingsController.Active[Name] = false;
            }
        }
    }
}