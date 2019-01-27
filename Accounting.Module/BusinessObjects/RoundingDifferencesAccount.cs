using DevExpress.Xpo;

namespace Accounting.Module.BusinessObjects
{
    [MapInheritance(MapInheritanceType.ParentTable)]
    public class RoundingDifferencesAccount : Account
    {
        public RoundingDifferencesAccount(Session session) : base(session)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            Category = AccountCategory.Liability;
            Type = AccountType.Credit;
        }
    }
}