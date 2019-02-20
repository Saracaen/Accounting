using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;

namespace Accounting.Module.BusinessObjects
{
    [ImageName("BO_Vendor")]
    [MapInheritance(MapInheritanceType.ParentTable)]
    [RuleCriteria("Supplier_Invoices_RuleCriteria", DefaultContexts.Delete, "Invoices.Count() = 0", "Suppliers with invoices cannot be deleted. Delete the invoices first, and then delete the supplier.")]
    [VisibleInReports]
    public class Supplier : Party
    {
        public Supplier(Session session) : base(session)
        {
        }

        [Appearance("Invoices", "IsNewObject(This)", Visibility = ViewItemVisibility.Hide)]
        [Association]
        public XPCollection<PurchaseInvoice> Invoices
        {
            get => GetCollection<PurchaseInvoice>(nameof(Invoices));
        }
    }
}