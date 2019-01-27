using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System.ComponentModel;

namespace Accounting.Module.BusinessObjects
{
    [DefaultProperty("Name")]
    [ImageName("BO_Customer")]
    [ModelDefault("DefaultLookupEditorMode", "AllItems")]
    [RuleCriteria("Party_Invoices_RuleCriteria", DefaultContexts.Delete, "PurchaseInvoices.Count() = 0 And SalesInvoices.Count() = 0", "Parties with invoices cannot be deleted. Delete the invoices first, and then delete the party.")]
    [VisibleInReports]
    public class Party : Organization
    {
        public Party(Session session) : base(session)
        {
        }

        [ModelDefault("Caption", "VAT Included")]
        [ModelDefault("CaptionForFalse", "No")]
        [ModelDefault("CaptionForTrue", "Yes")]
        public bool IsVatIncluded
        {
            get => GetPropertyValue<bool>(nameof(IsVatIncluded));
            set => SetPropertyValue(nameof(IsVatIncluded), value);
        }

        [ModelDefault("AllowClear", "False")]
        public PaymentTerm PaymentTerm
        {
            get => GetPropertyValue<PaymentTerm>(nameof(PaymentTerm));
            set => SetPropertyValue(nameof(PaymentTerm), value);
        }

        [Appearance("PurchaseInvoices", "Role <> 'None' And Role <> 'Supplier'", Visibility = ViewItemVisibility.Hide)]
        [Association]
        public XPCollection<PurchaseInvoice> PurchaseInvoices
        {
            get => GetCollection<PurchaseInvoice>(nameof(PurchaseInvoices));
        }

        [ImmediatePostData]
        public PartyRole Role { get; set; }

        [Appearance("SalesInvoices", "Role <> 'None' And Role <> 'Customer'", Visibility = ViewItemVisibility.Hide)]
        [Association]
        public XPCollection<SalesInvoice> SalesInvoices
        {
            get => GetCollection<SalesInvoice>(nameof(SalesInvoices));
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            PaymentTerm = Session.FindObject<PaymentTerm>(new BinaryOperator("Term", 30));
            Role = PartyRole.None;
        }
    }
}