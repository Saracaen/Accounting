using Accounting.Module.BusinessObjects;
using Accounting.Module.Controllers.Parameters;
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
    public class PostDepreciationController : ObjectViewController<ObjectView, Depreciation>
    {
        public PostDepreciationController()
        {
            PostDepreciationAction = new PopupWindowShowAction(this, "PostDepreciation", PredefinedCategory.RecordEdit);
            PostDepreciationAction.Caption = "Post";
            PostDepreciationAction.CustomizePopupWindowParams += PostDepreciationAction_CustomizePopupWindowParams;
            PostDepreciationAction.Execute += PostDepreciationAction_Execute;
            PostDepreciationAction.ImageName = "Action_LinkUnlink_Link";
            PostDepreciationAction.SelectionDependencyType = SelectionDependencyType.RequireMultipleObjects;
            PostDepreciationAction.TargetObjectsCriteria = "Not IsPosted";

            UnpostDepreciationAction = new SimpleAction(this, "UnpostDepreciation", PredefinedCategory.RecordEdit);
            UnpostDepreciationAction.Caption = "Unpost";
            UnpostDepreciationAction.ConfirmationMessage = "You are about to unpost the selected depreciation(s). Do you want to proceed?";
            UnpostDepreciationAction.Execute += UnpostDepreciationAction_Execute;
            UnpostDepreciationAction.ImageName = "Action_LinkUnlink_Unlink";
            UnpostDepreciationAction.SelectionDependencyType = SelectionDependencyType.RequireMultipleObjects;
            UnpostDepreciationAction.TargetObjectsCriteria = "IsPosted";

            RegisterActions(PostDepreciationAction, UnpostDepreciationAction);
        }

        public PopupWindowShowAction PostDepreciationAction { get; set; }

        public SimpleAction UnpostDepreciationAction { get; set; }

        private void PostDepreciationAction_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            var objectSpace = Application.CreateObjectSpace();
            var parameters = new PostDepreciationParameters();
            var detailView = Application.CreateDetailView(objectSpace, parameters);

            parameters.AssetAccount = objectSpace.FindObject<AssetAccount>(null);
            parameters.DepreciationExpenseAccount = objectSpace.FindObject<DepreciationExpenseAccount>(null);

            detailView.ViewEditMode = ViewEditMode.Edit;
            e.View = detailView;
        }

        private void PostDepreciationAction_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            var parameters = (PostDepreciationParameters)e.PopupWindowViewCurrentObject;
            Validator.RuleSet.Validate(e.PopupWindowView.ObjectSpace, parameters, DefaultContexts.Save);

            foreach (var depreciation in ViewSelectedObjects)
            {
                for (var i = 0; i < depreciation.Lines.Count; i++)
                {
                    var journalEntry = ObjectSpace.CreateObject<JournalEntry>();
                    journalEntry.Date = depreciation.Lines[i].Date;
                    journalEntry.Description = string.Format(CaptionHelper.GetLocalizedText("Texts", "PostDepreciation"), i + 1, depreciation.Description);
                    journalEntry.Item = depreciation;
                    journalEntry.Type = JournalEntryType.Depreciation;

                    var assetAccount = ObjectSpace.GetObject(parameters.AssetAccount);
                    var depreciationExpenseAccount = ObjectSpace.GetObject(parameters.DepreciationExpenseAccount);

                    journalEntry.AddLines(assetAccount, depreciationExpenseAccount, -depreciation.Lines[i].Amount);
                }
            }

            if (View is ListView)
            {
                ObjectSpace.CommitChanges();
            }
        }

        private void UnpostDepreciationAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            foreach (var depreciation in ViewSelectedObjects)
            {
                ObjectSpace.Delete(depreciation.JournalEntries);
            }

            if (View is ListView)
            {
                ObjectSpace.CommitChanges();
            }
        }
    }
}