using Accounting.Module.BusinessObjects;
using Accounting.Module.BusinessObjects.Parameters;
using Accounting.Module.Extensions;
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
            PostDepreciationAction.SelectionDependencyType = SelectionDependencyType.RequireSingleObject;
            PostDepreciationAction.TargetObjectsCriteria = "Not IsPosted";

            UnpostDepreciationAction = new SimpleAction(this, "UnpostDepreciation", PredefinedCategory.RecordEdit);
            UnpostDepreciationAction.Caption = "Unpost";
            UnpostDepreciationAction.ConfirmationMessage = "You are about to unpost the selected depreciation(s). Do you want to proceed?";
            UnpostDepreciationAction.Execute += UnpostDepreciationAction_Execute;
            UnpostDepreciationAction.ImageName = "Action_LinkUnlink_Unlink";
            UnpostDepreciationAction.SelectionDependencyType = SelectionDependencyType.RequireSingleObject;
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

            detailView.ViewEditMode = ViewEditMode.Edit;
            e.View = detailView;
        }

        private void PostDepreciationAction_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            var parameters = (PostDepreciationParameters)e.PopupWindowViewCurrentObject;
            Validator.RuleSet.Validate(e.PopupWindowView.ObjectSpace, parameters, DefaultContexts.Save);

            for (var i = 0; i < ViewCurrentObject.Lines.Count; i++)
            {
                var journalEntry = ObjectSpace.CreateObject<JournalEntry>();
                journalEntry.Date = ViewCurrentObject.Lines[i].Date;
                journalEntry.Description = string.Format(CaptionHelper.GetLocalizedText("Texts", "PostDepreciation"), i + 1, ViewCurrentObject.Description);
                journalEntry.Item = ViewCurrentObject;
                journalEntry.Type = JournalEntryType.Depreciation;

                var assetAccount = ObjectSpace.GetObject(parameters.AssetAccount);
                var depreciationExpenseAccount = ObjectSpace.GetObject(parameters.DepreciationExpenseAccount);

                journalEntry.AddLines(assetAccount, depreciationExpenseAccount, -ViewCurrentObject.Lines[i].Amount);
            }

            if (View is ListView)
            {
                ObjectSpace.CommitChanges();
            }
        }

        private void UnpostDepreciationAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            ObjectSpace.Delete(ViewCurrentObject.JournalEntries);

            if (View is ListView)
            {
                ObjectSpace.CommitChanges();
            }
        }
    }
}