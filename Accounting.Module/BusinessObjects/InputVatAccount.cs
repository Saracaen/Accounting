using DevExpress.ExpressApp.Model;
using DevExpress.Xpo;

namespace Accounting.Module.BusinessObjects
{
    [MapInheritance(MapInheritanceType.ParentTable)]
    [ModelDefault("Caption", "Input VAT Account")]
    public class InputVatAccount : Account
    {
        public InputVatAccount(Session session) : base(session)
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