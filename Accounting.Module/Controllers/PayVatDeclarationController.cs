using Accounting.Module.BusinessObjects;
using Accounting.Module.BusinessObjects.Parameters;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System.ComponentModel;

namespace Accounting.Module.Controllers
{
    [DesignerCategory("Code")]
    public class PayVatDeclarationController : ObjectViewController<ObjectView, VatDeclaration>
    {
        public PayVatDeclarationController()
        {
            PayVatDeclarationAction = new PopupWindowShowAction(this, "PayVatDeclaration", PredefinedCategory.RecordEdit);
            PayVatDeclarationAction.Caption = "Pay";
            PayVatDeclarationAction.CustomizePopupWindowParams += PayVatDeclarationAction_CustomizePopupWindowParams;
            PayVatDeclarationAction.Execute += PayVatDeclarationAction_Execute;
            PayVatDeclarationAction.ImageName = "BO_Sale";
            PayVatDeclarationAction.SelectionDependencyType = SelectionDependencyType.RequireSingleObject;
            PayVatDeclarationAction.TargetObjectsCriteria = "IsPosted And IsNullOrEmpty(PaidDate) And Total <> 0";

            RegisterActions(PayVatDeclarationAction);
        }

        public PopupWindowShowAction PayVatDeclarationAction { get; }

        private void PayVatDeclarationAction_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            var objectSpace = Application.CreateObjectSpace();
            var parameters = new PayVatDeclarationParameters();
            var detailView = Application.CreateDetailView(objectSpace, parameters);

            parameters.Account = objectSpace.FindObject<BankAccount>(null);
            parameters.Amount = ViewCurrentObject.Total;
            parameters.Description = string.Format(CaptionHelper.GetLocalizedText("Texts", "PayVatDeclaration"), CaptionHelper.GetDisplayText(ViewCurrentObject.Period), ViewCurrentObject.Year);

            detailView.ViewEditMode = ViewEditMode.Edit;
            e.View = detailView;
        }

        private void PayVatDeclarationAction_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            var parameters = (PayVatDeclarationParameters)e.PopupWindowViewCurrentObject;
            Validator.RuleSet.Validate(e.PopupWindowView.ObjectSpace, parameters, DefaultContexts.Save);

            var journalEntry = ObjectSpace.CreateObject<JournalEntry>();
            journalEntry.Date = parameters.Date;
            journalEntry.Description = parameters.Description;
            journalEntry.Item = ViewCurrentObject;
            journalEntry.Type = JournalEntryType.Payment;

            var account = ObjectSpace.GetObject(parameters.Account);
            var vatToPayAccount = ObjectSpace.FindObject<VatToPayAccount>(null);

            journalEntry.AddLines(account, vatToPayAccount, ViewCurrentObject.Total);

            if (View is ListView)
            {
                ObjectSpace.CommitChanges();
            }
        }
    }
}