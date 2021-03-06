﻿using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;

namespace Accounting.Module.BusinessObjects
{
    [ImageName("BO_Customer")]
    [MapInheritance(MapInheritanceType.ParentTable)]
    [RuleCriteria("Customer_Invoices_RuleCriteria", DefaultContexts.Delete, "Invoices.Count() = 0", "Customers with invoices cannot be deleted. Delete the invoices first, and then delete the customer.")]
    [VisibleInReports]
    public class Customer : Party
    {
        public Customer(Session session) : base(session)
        {
        }

        [Appearance("Invoices", "IsNewObject(This)", Visibility = ViewItemVisibility.Hide)]
        [Association]
        public XPCollection<SalesInvoice> Invoices
        {
            get => GetCollection<SalesInvoice>(nameof(Invoices));
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            Country = Session.FindObject<Company>(null).Country;
            PaymentTerm = Session.FindObject<Company>(null).PaymentTerm;
        }
    }
}