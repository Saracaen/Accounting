using DevExpress.ExpressApp.Model;
using DevExpress.Xpo;

namespace Accounting.Module.BusinessObjects
{
    [MapInheritance(MapInheritanceType.ParentTable)]
    [ModelDefault("Caption", "VAT To Pay Account")]
    public class VatToPayAccount : Account
    {
        public VatToPayAccount(Session session) : base(session)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            Category = AccountCategory.Vat;
            Type = AccountType.Credit;
        }
    }
}