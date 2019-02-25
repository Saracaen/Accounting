using Accounting.Module.BusinessObjects;
using System;
using System.Linq;

namespace Accounting.Module.BusinessObjects
{
    public static class JournalEntryExtensions
    {
        public static void AddLines(this JournalEntry journalEntry, Account fromAccount, Account toAccount, decimal amount)
        {
            if (journalEntry == null)
                throw new ArgumentNullException(nameof(journalEntry));
            if (fromAccount == null)
                throw new ArgumentNullException(nameof(fromAccount));
            if (toAccount == null)
                throw new ArgumentNullException(nameof(toAccount));

            amount = Math.Round(amount, 2, MidpointRounding.AwayFromZero);
            if (toAccount.Type == AccountType.Credit)
            {
                amount = -amount;
            }

            var fromAccountLine = journalEntry.Lines.FirstOrDefault(x => Equals(x.Account, fromAccount));
            if (fromAccountLine != null)
            {
                fromAccountLine.Amount += amount;
            }
            else
            {
                journalEntry.Lines.Add(new JournalEntryLine(journalEntry.Session) { Account = fromAccount, Amount = amount });
            }

            var toAccountLine = journalEntry.Lines.FirstOrDefault(x => Equals(x.Account, toAccount));
            if (toAccountLine != null)
            {
                toAccountLine.Amount += -amount;
            }
            else
            {
                journalEntry.Lines.Add(new JournalEntryLine(journalEntry.Session) { Account = toAccount, Amount = -amount });
            }
        }
    }
}