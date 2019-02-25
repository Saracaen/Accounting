using Accounting.Module.BusinessObjects;
using Accounting.Module.Extensions;
using Accounting.Module.Utils;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using System.ComponentModel;
using System.Linq;

namespace Accounting.Module.Controllers
{
    [DesignerCategory("Code")]
    public class PostVatDeclarationController : ObjectViewController<ObjectView, VatDeclaration>
    {
        public PostVatDeclarationController()
        {
            PostVatDeclarationAction = new SimpleAction(this, "PostVatDeclaration", PredefinedCategory.RecordEdit);
            PostVatDeclarationAction.Caption = "Post";
            PostVatDeclarationAction.Execute += PostVatDeclarationAction_Execute;
            PostVatDeclarationAction.ImageName = "Action_LinkUnlink_Link";
            PostVatDeclarationAction.SelectionDependencyType = SelectionDependencyType.RequireSingleObject;
            PostVatDeclarationAction.TargetObjectsCriteria = "Not IsPosted";

            UnpostVatDeclarationAction = new SimpleAction(this, "UnpostVatDeclaration", PredefinedCategory.RecordEdit);
            UnpostVatDeclarationAction.Caption = "Unpost";
            UnpostVatDeclarationAction.ConfirmationMessage = "You are about to unpost the selected VAT declaration(s). Do you want to proceed?";
            UnpostVatDeclarationAction.Execute += UnpostVatDeclarationAction_Execute;
            UnpostVatDeclarationAction.ImageName = "Action_LinkUnlink_Unlink";
            UnpostVatDeclarationAction.SelectionDependencyType = SelectionDependencyType.RequireSingleObject;
            UnpostVatDeclarationAction.TargetObjectsCriteria = "IsPosted";

            RegisterActions(PostVatDeclarationAction, UnpostVatDeclarationAction);
        }

        public SimpleAction PostVatDeclarationAction { get; set; }

        public SimpleAction UnpostVatDeclarationAction { get; set; }

        private void PostVatDeclaration(JournalEntry journalEntry, Account vatAccount, VatPaymentAccount vatPaymentAccount, CriteriaOperator criteria)
        {
            vatAccount.JournalEntryLines.Criteria = criteria;

            var amount = vatAccount.JournalEntryLines.Sum(x => x.Amount);
            if (amount != 0)
            {
                journalEntry.AddLines(vatAccount, vatPaymentAccount, -amount);
            }
        }

        private void PostVatDeclarationAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var journalEntry = ObjectSpace.CreateObject<JournalEntry>();
            journalEntry.Date = VatDeclarationHelper.GetLastDayOfPeriod(ViewCurrentObject.Year, ViewCurrentObject.Period);
            journalEntry.Description = string.Format(CaptionHelper.GetLocalizedText("Texts", "PostVatDeclaration"), CaptionHelper.GetDisplayText(ViewCurrentObject.Period), ViewCurrentObject.Year);
            journalEntry.Item = ViewCurrentObject;
            journalEntry.Type = JournalEntryType.Posting;

            var firstDayOfPeriod = VatDeclarationHelper.GetFirstDayOfPeriod(ViewCurrentObject.Year, ViewCurrentObject.Period);
            var lastDayOfPeriod = VatDeclarationHelper.GetLastDayOfPeriod(ViewCurrentObject.Year, ViewCurrentObject.Period);
            var criteria = CriteriaOperator.Parse("JournalEntry.Date >= ? And JournalEntry.Date <= ?", firstDayOfPeriod, lastDayOfPeriod);
            var vatPaymentAccount = ObjectSpace.FindObject<VatPaymentAccount>(null);
            var vatToPayAccount = ObjectSpace.FindObject<VatToPayAccount>(null);

            foreach (var inputVatAccount in ObjectSpace.GetObjects<InputVatAccount>())
            {
                PostVatDeclaration(journalEntry, inputVatAccount, vatPaymentAccount, criteria);
            }

            foreach (var outputVatAccount in ObjectSpace.GetObjects<OutputVatAccount>())
            {
                PostVatDeclaration(journalEntry, outputVatAccount, vatPaymentAccount, criteria);
            }

            journalEntry.AddLines(vatPaymentAccount, vatToPayAccount, -ViewCurrentObject.Total);

            if (View is ListView)
            {
                ObjectSpace.CommitChanges();
            }
        }

        private void UnpostVatDeclarationAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            ObjectSpace.Delete(ViewCurrentObject.JournalEntries);

            if (View is ListView)
            {
                ObjectSpace.CommitChanges();
            }
        }
    }
}