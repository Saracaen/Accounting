using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.ComponentModel;

namespace Accounting.Module.BusinessObjects
{
    [Appearance("Enabled", "Type <> 'Entry'", TargetItems = "*", Enabled = false)]
    [DefaultProperty("Description")]
    [ImageName("BO_Note")]
    [RuleCriteria("JournalEntry_Lines_Count_RuleCriteria", DefaultContexts.Save, "Lines.Count() > 1", "A journal entry must have at least two lines.")]
    [RuleCriteria("JournalEntry_Lines_Sum_RuleCriteria", DefaultContexts.Save, "Lines.Sum(Amount) = 0", "The sum of all journal entry lines must be zero.")]
    [VisibleInReports]
    public class JournalEntry : BaseObject, ISupportDate
    {
        public JournalEntry(Session session) : base(session)
        {
            Lines.ListChanged += Lines_ListChanged;
        }

        [PersistentAlias("IsNull(Lines[Amount > 0].Sum(Amount), 0)")]
        public decimal Amount
        {
            get => Convert.ToDecimal(EvaluateAlias(nameof(Amount)));
        }

        [RuleRequiredField("JournalEntry_Date_RuleRequiredField", DefaultContexts.Save)]
        public DateTime Date
        {
            get => GetPropertyValue<DateTime>(nameof(Date));
            set => SetPropertyValue(nameof(Date), value);
        }

        [RuleRequiredField("JournalEntry_Description_RuleRequiredField", DefaultContexts.Save)]
        public string Description
        {
            get => GetPropertyValue<string>(nameof(Description));
            set => SetPropertyValue(nameof(Description), value);
        }

        [Association]
        public JournalEntryItem Item
        {
            get => GetPropertyValue<JournalEntryItem>(nameof(Item));
            set => SetPropertyValue(nameof(Item), value);
        }

        [Aggregated]
        [Association]
        public XPCollection<JournalEntryLine> Lines
        {
            get => GetCollection<JournalEntryLine>(nameof(Lines));
        }

        [ModelDefault("AllowEdit", "False")]
        public JournalEntryType Type
        {
            get => GetPropertyValue<JournalEntryType>(nameof(Type));
            set => SetPropertyValue(nameof(Type), value);
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            Date = DateTime.Today;
            Type = JournalEntryType.Entry;
        }

        private void Lines_ListChanged(object sender, ListChangedEventArgs e)
        {
            switch (e.ListChangedType)
            {
                case ListChangedType.ItemAdded:
                case ListChangedType.ItemChanged:
                case ListChangedType.ItemDeleted:
                case ListChangedType.ItemMoved:
                    OnChanged(nameof(Lines));
                    break;
            }
        }
    }
}