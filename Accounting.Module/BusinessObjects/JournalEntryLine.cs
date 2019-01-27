using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;

namespace Accounting.Module.BusinessObjects
{
    [VisibleInReports]
    public class JournalEntryLine : BaseObject
    {
        public JournalEntryLine(Session session) : base(session)
        {
        }

        [Association]
        [RuleRequiredField("JournalEntryLine_Account_RuleRequiredField", DefaultContexts.Save)]
        public Account Account
        {
            get => GetPropertyValue<Account>(nameof(Account));
            set => SetPropertyValue(nameof(Account), value);
        }

        public decimal Amount
        {
            get => GetPropertyValue<decimal>(nameof(Amount));
            set => SetPropertyValue(nameof(Amount), value);
        }

        [ImmediatePostData]
        [PersistentAlias("Iif(Amount < 0, -Amount, 0)")]
        [RuleRange("JournalEntryLine_Credit_RuleRange", DefaultContexts.Save, 0, long.MaxValue)]
        public decimal Credit
        {
            get => Convert.ToDecimal(EvaluateAlias(nameof(Credit)));
            set => Amount = -value;
        }

        [ImmediatePostData]
        [PersistentAlias("Iif(Amount > 0, Amount, 0)")]
        [RuleRange("JournalEntryLine_Debit_RuleRange", DefaultContexts.Save, 0, long.MaxValue)]
        public decimal Debit
        {
            get => Convert.ToDecimal(EvaluateAlias(nameof(Debit)));
            set => Amount = value;
        }

        [Association]
        public JournalEntry JournalEntry
        {
            get => GetPropertyValue<JournalEntry>(nameof(JournalEntry));
            set => SetPropertyValue(nameof(JournalEntry), value);
        }
    }
}