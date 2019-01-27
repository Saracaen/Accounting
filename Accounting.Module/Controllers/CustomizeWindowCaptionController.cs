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
                    e.WindowCaption.FirstPart = e.WindowCaption.SecondPart;
                    e.WindowCaption.SecondPart = aboutInfo.ProductName;
                    e.WindowCaption.Separator = " ";
                }
                else
                {
                    if (e.WindowCaption.FirstPart != Frame.View.Caption)
                    {
                        e.WindowCaption.FirstPart = string.IsNullOrEmpty(e.WindowCaption.FirstPart) ? Frame.View.Caption : $"{Frame.View.Caption}{e.WindowCaption.Separator}{e.WindowCaption.FirstPart}";
                    }

                    if (string.Equals(e.WindowCaption.SecondPart, Frame.View.Caption))
                    {
                        e.WindowCaption.SecondPart = Application.Title;
                    }

                    if (string.Equals(e.WindowCaption.FirstPart, e.WindowCaption.SecondPart))
                    {
                        e.WindowCaption.SecondPart = null;
                    }
                }
            }
        }
    }
}