﻿using Accounting.Module.BusinessObjects;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System;
using System.ComponentModel;

namespace Accounting.Module.Controllers.Parameters
{
    [DomainComponent]
    [ModelDefault("Caption", "Pay Purchase Invoice")]
    public class PayPurchaseInvoiceParameters
    {
        [DataSourceCriteriaProperty("AccountCriteria")]
        [RuleRequiredField("PayPurchaseInvoiceParameters_Account_RuleRequiredField", DefaultContexts.Save)]
        public Account Account { get; set; }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public CriteriaOperator AccountCriteria
        {
            get => CriteriaOperator.Parse("IsExactType(This, ?) Or IsExactType(This, ?) Or IsExactType(This, ?) Or IsExactType(This, ?)", typeof(BankAccount).FullName, typeof(CashAccount).FullName, typeof(CreditCardAccount).FullName, typeof(PrivateAccount).FullName);
        }

        [Appearance("Amount", "Invoice Is Null", Visibility = ViewItemVisibility.Hide)]
        [RuleRange("PayPurchaseInvoiceParameters_Amount_RuleRange", DefaultContexts.Save, "Iif(Invoice.DueAmount < 0, Invoice.DueAmount, 0)", "Iif(Invoice.DueAmount < 0, 0, Invoice.DueAmount)", ParametersMode.Expression, TargetCriteria = "Invoice Is Not Null")]
        public decimal Amount { get; set; }

        [RuleRequiredField("PayPurchaseInvoiceParameters_Date_RuleRequiredField", DefaultContexts.Save)]
        public DateTime Date { get; set; } = DateTime.UtcNow.Date;

        [Appearance("Description", "Invoice Is Null", Visibility = ViewItemVisibility.Hide)]
        [RuleRequiredField("PayPurchaseInvoiceParameters_Description_RuleRequiredField", DefaultContexts.Save, TargetCriteria = "Invoice Is Not Null")]
        public string Description { get; set; }

        public PurchaseInvoice Invoice { get; set; }
    }
}