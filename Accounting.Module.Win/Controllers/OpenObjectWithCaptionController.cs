using Accounting.Module.Win.Model;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Utils;
using DevExpress.ExpressApp.Win.SystemModule;
using System.ComponentModel;

namespace Accounting.Module.Win.Controllers
{
    [DesignerCategory("Code")]
    public class OpenObjectWithCaptionController : OpenObjectController, IModelExtender
    {
        public void ExtendModelInterfaces(ModelInterfaceExtenders extenders)
        {
            extenders.Add<IModelOptions, IModelOpenObjectWithCaptionOptions>();
        }

        protected override void UpdateActionState(object objectToOpen)
        {
            base.UpdateActionState(objectToOpen);

            if (objectToOpen != null)
            {
                var classCaption = CaptionHelper.GetClassCaption(objectToOpen.GetType().FullName).ToLower();
                var objectCaption = CaptionHelper.GetLocalizedText("Texts", "OpenObjectWithCaption");

                if (Equals(((IModelOpenObjectWithCaptionOptions)Application.Model.Options).UseLowerCaseClassCaptions, bool.TrueString))
                {
                    classCaption = classCaption.ToLower();
                }

                OpenObjectAction.Caption = string.Format(objectCaption, classCaption);
            }
            else
            {
                OpenObjectAction.Caption = Application.Model.ActionDesign.Actions["OpenObject"].Caption;
            }
        }
    }
}