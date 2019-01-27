using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Validation;
using System;
using System.ComponentModel;

namespace Accounting.Module.BusinessObjects.Parameters
{
    [DomainComponent]
    [ModelDefault("Caption", "Close Period")]
    public class ClosePeriodParameters
    {
        [RuleRange("ClosePeriodParameters_ClosureDate_RuleRange", DefaultContexts.Save, "AddDays(LastClosureJournalEntry.Date, 1)", "#12/31/9999#", ParametersMode.Expression)]
        public DateTime ClosureDate { get; set; }

        [RuleRequiredField("ClosePeriodParameters_Description_RuleRequiredField", DefaultContexts.Save)]
        public string Description { get; set; }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public JournalEntry LastClosureJournalEntry { get; set; }
    }
}