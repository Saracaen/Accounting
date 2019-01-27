using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;

namespace Accounting.Module.BusinessObjects
{
    public class DepreciationLine : BaseObject
    {
        public DepreciationLine(Session session) : base(session)
        {
        }

        [RuleRange("DepreciationLine_Amount_RuleRange", DefaultContexts.Save, 0, long.MaxValue)]
        public decimal Amount
        {
            get => GetPropertyValue<decimal>(nameof(Amount));
            set => SetPropertyValue(nameof(Amount), value);
        }

        [RuleRequiredField("DepreciationLine_Date_RuleRequiredField", DefaultContexts.Save)]
        public DateTime Date
        {
            get => GetPropertyValue<DateTime>(nameof(Date));
            set => SetPropertyValue(nameof(Date), value);
        }

        [Association]
        public Depreciation Depreciation
        {
            get => GetPropertyValue<Depreciation>(nameof(Depreciation));
            set => SetPropertyValue(nameof(Depreciation), value);
        }
    }
}