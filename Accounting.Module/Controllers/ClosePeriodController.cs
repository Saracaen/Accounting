using Accounting.Module.BusinessObjects;
using Accounting.Module.BusinessObjects.Parameters;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System;
using System.ComponentModel;
using System.Linq;

namespace Accounting.Module.Controllers
{
    [DesignerCategory("Code")]
    public class ClosePeriodController : ObjectViewController<ListView, JournalEntry>
    {
        public ClosePeriodController()
        {
            ClosePeriodAction = new PopupWindowShowAction(this, "ClosePeriod", PredefinedCategory.RecordEdit);
            ClosePeriodAction.Caption = "Close Period";
            ClosePeriodAction.CustomizePopupWindowParams += ClosePeriodAction_CustomizePopupWindowParams;
            ClosePeriodAction.Execute += ClosePeriodAction_Execute;
            ClosePeriodAction.ImageName = "Action_LogOff";

            RegisterActions(ClosePeriodAction);
        }

        public PopupWindowShowAction ClosePeriodAction { get; }

        private void ClosePeriod(JournalEntry journalEntry, EquityAccount equityAccount, Account account, CriteriaOperator criteria)
        {
            account.JournalEntryLines.Criteria = criteria;

            var amount = account.JournalEntryLines.Sum(x => x.Amount);
            if (amount != 0)
            {
                journalEntry.AddLines(account, equityAccount, amount);
            }
        }

        private void ClosePeriodAction_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            var objectSpace = Application.CreateObjectSpace();
            var parameters = new ClosePeriodParameters();
            var detailView = Application.CreateDetailView(objectSpace, parameters, true);
            var query = objectSpace.GetObjectsQuery<JournalEntry>().Where(x => x.Type == JournalEntryType.Closure);

            parameters.ClosureDate = new DateTime(DateTime.Today.Year - 1, 12, 31);
            parameters.LastClosureDate = query.OrderByDescending(x => x.Date).Select(x => x.Date).FirstOrDefault();

            if (parameters.ClosureDate <= parameters.LastClosureDate.Date)
            {
                parameters.ClosureDate = parameters.LastClosureDate.AddYears(1);
            }

            detailView.ViewEditMode = ViewEditMode.Edit;
            e.View = detailView;
        }

        private void ClosePeriodAction_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            var parameters = (ClosePeriodParameters)e.PopupWindowViewCurrentObject;
            Validator.RuleSet.Validate(e.PopupWindowView.ObjectSpace, parameters, DefaultContexts.Save);

            using (var objectSpace = Application.CreateObjectSpace())
            {
                var journalEntry = objectSpace.CreateObject<JournalEntry>();
                journalEntry.Date = parameters.ClosureDate.AddSeconds(86399);
                journalEntry.Description = parameters.Description;
                journalEntry.Type = JournalEntryType.Closure;

                var criteria = CriteriaOperator.Parse("JournalEntry.Date <= ?", parameters.ClosureDate);
                if (parameters.LastClosureDate != default)
                {
                    criteria = CriteriaOperator.And(CriteriaOperator.Parse("JournalEntry.Date > ?", parameters.LastClosureDate), criteria);
                }

                var equityAccount = objectSpace.FindObject<EquityAccount>(null);
                var privateAccount = objectSpace.FindObject<PrivateAccount>(null);

                foreach (var account in objectSpace.GetObjects<Account>(CriteriaOperator.Parse("Category = 'Expense' Or Category = 'Income'")))
                {
                    ClosePeriod(journalEntry, equityAccount, account, criteria);
                }
                ClosePeriod(journalEntry, equityAccount, privateAccount, criteria);

                objectSpace.CommitChanges();
            }

            View.Refresh(true);
        }
    }
}