using DevExpress.Xpo;

namespace Accounting.Module.BusinessObjects
{
    [MapInheritance(MapInheritanceType.ParentTable)]
    public class SupplierAccount : Account
    {
        public SupplierAccount(Session session) : base(session)
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