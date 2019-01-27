using Accounting.Module.BusinessObjects;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ReportsV2;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;

namespace Accounting.Module.Reports.Parameters
{
    [DomainComponent]
    [ModelDefault("Caption", "Profit And Loss Statement")]
    [RuleCriteria("ProfitAndLossStatementParameters_StartDateEndDate_RuleCriteria", "PreviewReport", "EndDate >= StartDate", "The end date must be greater than or equal to the start date.")]
    public class ProfitAndLossStatementParameters : ReportParametersObjectBase
    {
        public ProfitAndLossStatementParameters(IObjectSpaceCreator objectSpaceCreator) : base(objectSpaceCreator)
        {
        }

        [RuleRequiredField("ProfitAndLossStatementParameters_EndDate_RuleRequiredField", "PreviewReport")]
        public DateTime EndDate { get; set; } = new DateTime(DateTime.Now.Year, 12, 31);

        [RuleRequiredField("ProfitAndLossStatementParameters_StartDate_RuleRequiredField", "PreviewReport")]
        public DateTime StartDate { get; set; } = new DateTime(DateTime.Now.Year, 1, 1);

        public override CriteriaOperator GetCriteria()
        {
            return CriteriaOperator.Parse("JournalEntry.Date >= ? And JournalEntry.Date <= ? And Account.Category In ('Expense', 'Income')", StartDate, EndDate);
        }

        public override SortProperty[] GetSorting()
        {
            return Array.Empty<SortProperty>();
        }

        public override string ToString()
        {
            return $"{StartDate.ToShortDateString()} - {EndDate.ToShortDateString()}";
        }

        protected override IObjectSpace CreateObjectSpace()
        {
            return objectSpaceCreator.CreateObjectSpace(typeof(JournalEntryLine));
        }
    }
}