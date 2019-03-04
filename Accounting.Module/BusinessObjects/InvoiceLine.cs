using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.ComponentModel;
using System.Linq;

namespace Accounting.Module.BusinessObjects
{
    [DefaultProperty("Description")]
    public class InvoiceLine : BaseObject
    {
        public InvoiceLine(Session session) : base(session)
        {
        }

        [DataSourceCriteriaProperty("AccountCriteria")]
        [ImmediatePostData]
        [RuleRequiredField("InvoiceLine_Account_RuleRequiredField", DefaultContexts.Save, TargetCriteria = "Quantity <> 0 Or UnitPrice <> 0 Or VatRate Is Not Null")]
        public Account Account
        {
            get => GetPropertyValue<Account>(nameof(Account));
            set => SetAccount(value);
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public CriteriaOperator AccountCriteria
        {
            get
            {
                if (Invoice != null)
                {
                    switch (Invoice)
                    {
                        case PurchaseInvoice _:
                            return CriteriaOperator.Parse("Category = 'Expense' Or IsExactType(This, ?)", typeof(AssetAccount).FullName);

                        case SalesInvoice _:
                            return CriteriaOperator.Parse("Category = 'Income'");

                        default:
                            throw new InvalidOperationException(CaptionHelper.GetLocalizedText(@"Exceptions\UserVisibleExceptions", "UnsupportedInvoiceClass"));
                    }
                }
                return null;
            }
        }

        [ImmediatePostData]
        [RuleRequiredField("InvoiceLine_Description_RuleRequiredField", DefaultContexts.Save)]
        public string Description
        {
            get => GetPropertyValue<string>(nameof(Description));
            set => SetPropertyValue(nameof(Description), value);
        }

        [Association]
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public Invoice Invoice
        {
            get => GetPropertyValue<Invoice>(nameof(Invoice));
            set => SetInvoice(value);
        }

        [ImmediatePostData]
        public float Quantity
        {
            get => GetPropertyValue<float>(nameof(Quantity));
            set => SetPropertyValue(nameof(Quantity), value);
        }

        [PersistentAlias("Iif(Invoice.IsVatIncluded, (Iif(Invoice.Type = 'CreditNote', -UnitPrice, UnitPrice) * ToDecimal(Quantity)) / ((ToDecimal(VatRate.Rate) / 100) + 1), Iif(Invoice.Type = 'CreditNote', -UnitPrice, UnitPrice) * ToDecimal(Quantity))")]
        public decimal SubTotal
        {
            get => Convert.ToDecimal(EvaluateAlias(nameof(SubTotal)));
        }

        [PersistentAlias("SubTotal + Vat")]
        public decimal Total
        {
            get => Convert.ToDecimal(EvaluateAlias(nameof(Total)));
        }

        [ImmediatePostData]
        public decimal UnitPrice
        {
            get => GetPropertyValue<decimal>(nameof(UnitPrice));
            set => SetPropertyValue(nameof(UnitPrice), value);
        }

        [ModelDefault("Caption", "VAT")]
        [PersistentAlias("Iif(VatRate.PayableAccount Is Null Or VatRate.ReceivableAccount Is Null, SubTotal * (ToDecimal(VatRate.Rate) / 100), 0)")]
        public decimal Vat
        {
            get => Convert.ToDecimal(EvaluateAlias(nameof(Vat)));
        }

        [DataSourceCriteriaProperty("VatRateCriteria")]
        [ImmediatePostData]
        [ModelDefault("Caption", "Default VAT Rate")]
        [RuleRequiredField("InvoiceLine_VatRate_RuleRequiredField", DefaultContexts.Save, TargetCriteria = "Account Is Not Null Or Quantity <> 0 Or UnitPrice <> 0")]
        public VatRate VatRate
        {
            get => GetPropertyValue<VatRate>(nameof(VatRate));
            set => SetPropertyValue(nameof(VatRate), value);
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public CriteriaOperator VatRateCriteria
        {
            get
            {
                if (Invoice != null)
                {
                    switch (Invoice)
                    {
                        case PurchaseInvoice _:
                            return CriteriaOperator.Parse("ReceivableAccount Is Not Null And ReceivableCategory <> 'SmallBusinessScheme' Or Rate = 0 And PayableAccount Is Null And ReceivableAccount Is Null");

                        case SalesInvoice _:
                            return CriteriaOperator.Parse("PayableAccount Is Not Null And ReceivableAccount Is Null Or Rate = 0 And PayableAccount Is Null And ReceivableAccount Is Null");

                        default:
                            throw new InvalidOperationException(CaptionHelper.GetLocalizedText(@"Exceptions\UserVisibleExceptions", "UnsupportedInvoiceClass"));
                    }
                }
                return null;
            }
        }

        private void SetAccount(Account value)
        {
            if (SetPropertyValue(nameof(Account), value))
            {
                if (IsLoading || IsSaving)
                    return;

                if (value is ISupportDefaultVatRate supportDefaultVatRate)
                {
                    VatRate = supportDefaultVatRate.DefaultVatRate;
                }
            }
        }

        private void SetInvoice(Invoice value)
        {
            if (SetPropertyValue(nameof(Invoice), value))
            {
                if (IsLoading || IsSaving || value == null)
                    return;

                if (value.Lines.Count > 0)
                {
                    Account = value.Lines[value.Lines.Count - 1].Account;
                }
                else if (value is SalesInvoice)
                {
                    Account = Session.Query<IncomeAccount>().OrderBy(x => x.Name).FirstOrDefault();
                }
            }
        }
    }
}