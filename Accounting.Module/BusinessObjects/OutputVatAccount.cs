using DevExpress.ExpressApp.Model;
using DevExpress.Xpo;

namespace Accounting.Module.BusinessObjects
{
    [MapInheritance(MapInheritanceType.ParentTable)]
    [ModelDefault("Caption", "Output VAT Account")]
    public class OutputVatAccount : Account
    {
        public OutputVatAccount(Session session) : base(session)
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