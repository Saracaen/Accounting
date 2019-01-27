using DevExpress.ExpressApp.Model;
using DevExpress.Xpo;

namespace Accounting.Module.BusinessObjects
{
    [MapInheritance(MapInheritanceType.ParentTable)]
    [ModelDefault("Caption", "VAT Payment Account")]
    public class VatPaymentAccount : Account
    {
        public VatPaymentAccount(Session session) : base(session)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            Category = AccountCategory.Vat;
            Type = AccountType.Debit;
        }
    }
}