using DevExpress.Xpo;

namespace Accounting.Module.BusinessObjects
{
    [MapInheritance(MapInheritanceType.ParentTable)]
    public class DepreciationExpenseAccount : Account
    {
        public DepreciationExpenseAccount(Session session) : base(session)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            Category = AccountCategory.Expense;
            Type = AccountType.Debit;
        }
    }
}