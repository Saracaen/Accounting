using DevExpress.ExpressApp;
using DevExpress.ExpressApp.SystemModule;
using System.ComponentModel;

namespace Accounting.Module.Controllers
{
    [DesignerCategory("Code")]
    public class CustomizeWindowCaptionController : WindowController
    {
        protected override void OnActivated()
        {
            base.OnActivated();

            var windowTemplateController = Frame.GetController<WindowTemplateController>();
            if (windowTemplateController != null)
            {
                windowTemplateController.CustomizeWindowCaption += WindowTemplateController_CustomizeWindowCaption;
            }
        }

        protected override void OnDeactivated()
        {
            base.OnDeactivated();

            var windowTemplateController = Frame.GetController<WindowTemplateController>();
            if (windowTemplateController != null)
            {
                windowTemplateController.CustomizeWindowCaption -= WindowTemplateController_CustomizeWindowCaption;
            }
        }

        private void WindowTemplateController_CustomizeWindowCaption(object sender, CustomizeWindowCaptionEventArgs e)
        {
            if (Frame?.View != null)
            {
                if (Frame.View.CurrentObject is AboutInfo aboutInfo)
                {
                    e.WindowCaption.Text = $"{e.WindowCaption.SecondPart} {aboutInfo.ProductName}";
                }
                else if (e.WindowCaption.FirstPart == Frame.View.Caption)
                {
                    e.WindowCaption.Text = $"{Frame.View.Caption}{e.WindowCaption.Separator}{Application.Title}";
                }
                else if (string.IsNullOrEmpty(e.WindowCaption.FirstPart))
                {
                    e.WindowCaption.Text = Frame.View.Caption;
                }
                else
                {
                    e.WindowCaption.Text = $"{Frame.View.Caption}{e.WindowCaption.Separator}{e.WindowCaption.FirstPart}{e.WindowCaption.Separator}{Application.Title}";
                }
            }
        }
    }
}