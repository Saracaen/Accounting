using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.ComponentModel;

namespace Accounting.Module.BusinessObjects
{
    [DefaultProperty("Description")]
    [ImageName("BO_Transition")]
    [RuleCriteria("Depreciation_Lines_RuleCriteria", DefaultContexts.Save, "Lines.Sum(Amount) = Value - ResidualValue", "The sum of the amount must be equal to the depreciable value.")]
    [VisibleInReports]
    public class Depreciation : JournalEntryItem
    {
        public Depreciation(Session session) : base(session)
        {
        }

        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("DisplayFormat", "{0:D0}")]
        [ModelDefault("EditMask", "D0")]
        public int DepreciationTime
        {
            get => GetPropertyValue<int>(nameof(DepreciationTime));
            set => SetPropertyValue(nameof(DepreciationTime), value);
        }

        [RuleRequiredField("Depreciation_Description_RuleRequiredField", DefaultContexts.Save)]
        public string Description
        {
            get => GetPropertyValue<string>(nameof(Description));
            set => SetPropertyValue(nameof(Description), value);
        }

        [Aggregated]
        [Association]
        public XPCollection<DepreciationLine> Lines
        {
            get => GetCollection<DepreciationLine>(nameof(Lines));
        }

        [ModelDefault("AllowEdit", "False")]
        public decimal ResidualValue
        {
            get => GetPropertyValue<decimal>(nameof(ResidualValue));
            set => SetPropertyValue(nameof(ResidualValue), value);
        }

        [ModelDefault("AllowEdit", "False")]
        public DateTime StartDate
        {
            get => GetPropertyValue<DateTime>(nameof(StartDate));
            set => SetPropertyValue(nameof(StartDate), value);
        }

        [ModelDefault("AllowEdit", "False")]
        public DepreciationType Type
        {
            get => GetPropertyValue<DepreciationType>(nameof(Type));
            set => SetPropertyValue(nameof(Type), value);
        }

        [ModelDefault("AllowEdit", "False")]
        public decimal Value
        {
            get => GetPropertyValue<decimal>(nameof(Value));
            set => SetPropertyValue(nameof(Value), value);
        }
    }
}