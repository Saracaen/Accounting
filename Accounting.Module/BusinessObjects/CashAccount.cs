using DevExpress.Xpo;

namespace Accounting.Module.BusinessObjects
{
    [MapInheritance(MapInheritanceType.ParentTable)]
    public class CashAccount : Account
    {
        public CashAccount(Session session) : base(session)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            Category = AccountCategory.Asset;
            Type = AccountType.Debit;
        }
    }
}