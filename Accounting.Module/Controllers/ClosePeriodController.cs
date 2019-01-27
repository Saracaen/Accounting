using Accounting.Module.BusinessObjects;
using Accounting.Module.BusinessObjects.Parameters;
using Accounting.Module.Extensions;
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
            var objectSpace = Application.CreateObjectSpace(typeof(ClosePeriodParameters));
            var parameters = new ClosePeriodParameters();
            var detailView = Application.CreateDetailView(objectSpace, parameters);

            parameters.ClosureDate = new DateTime(DateTime.Today.Year - 1, 12, 31);
            parameters.LastClosureJournalEntry = ObjectSpace.GetObjectsQuery<JournalEntry>().Where(x => x.Type == JournalEntryType.Closure).OrderByDescending(x => x.Date).FirstOrDefault();

            if (parameters.LastClosureJournalEntry != null && parameters.ClosureDate <= parameters.LastClosureJournalEntry.Date)
            {
                parameters.ClosureDate = parameters.LastClosureJournalEntry.Date.AddYears(1);
            }

            detailView.ViewEditMode = ViewEditMode.Edit;
            e.View = detailView;
        }

        private void ClosePeriodAction_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            var parameters = (ClosePeriodParameters)e.PopupWindowViewCurrentObject;
            Validator.RuleSet.Validate(e.PopupWindowView.ObjectSpace, parameters, DefaultContexts.Save);

            var journalEntry = ObjectSpace.CreateObject<JournalEntry>();
            journalEntry.Date = parameters.ClosureDate.AddSeconds(86399);
            journalEntry.Description = parameters.Description;
            journalEntry.Type = JournalEntryType.Closure;

            var criteria = CriteriaOperator.Parse("JournalEntry.Date <= ?", parameters.ClosureDate);
            if (parameters.LastClosureJournalEntry != null)
            {
                criteria = CriteriaOperator.And(CriteriaOperator.Parse("JournalEntry.Date > ?", parameters.LastClosureJournalEntry.Date), criteria);
            }

            var equityAccount = ObjectSpace.FindObject<EquityAccount>(null);
            var privateAccount = ObjectSpace.FindObject<PrivateAccount>(null);

            foreach (var expenseAccount in ObjectSpace.GetObjects<Account>(new BinaryOperator("Category", AccountCategory.Expense)))
            {
                ClosePeriod(journalEntry, equityAccount, expenseAccount, criteria);
            }

            foreach (var incomeAccount in ObjectSpace.GetObjects<Account>(new BinaryOperator("Category", AccountCategory.Income)))
            {
                ClosePeriod(journalEntry, equityAccount, incomeAccount, criteria);
            }

            ClosePeriod(journalEntry, equityAccount, privateAccount, criteria);

            try
            {
                ObjectSpace.CommitChanges();
            }
            catch
            {
                ObjectSpace.Rollback();
            }
            ObjectSpace.Refresh();
        }
    }
}