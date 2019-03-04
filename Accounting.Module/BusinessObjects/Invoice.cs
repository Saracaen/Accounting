using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.ComponentModel;
using System.Linq;

namespace Accounting.Module.BusinessObjects
{
    [Appearance("Overdue", "IsPosted And DueAmount <> 0 And DateDiffDay(Now(), DueDate) < 0", TargetItems = "*", Context = "ListView", BackColor = "255, 128, 128")]
    [DefaultProperty("Identifier")]
    [RuleCriteria("Invoice_Lines_RuleCriteria", DefaultContexts.Save, "Lines[Account Is Not Null].Count() > 0", "An invoice must have at least one valid line.")]
    [RuleCriteria("Invoice_JournalEntries_RuleCriteria", DefaultContexts.Delete, "Not IsPosted", "An invoice must be unposted before it can be deleted.")]
    public abstract class Invoice : JournalEntryItem, ISupportDate, ISupportIdentifier
    {
        protected Invoice(Session session) : base(session)
        {
            Lines.ListChanged += Lines_ListChanged;
        }

        [ImmediatePostData]
        [RuleRequiredField("Invoice_Date_RuleRequiredField", DefaultContexts.Save)]
        public DateTime Date
        {
            get => GetPropertyValue<DateTime>(nameof(Date));
            set => SetDate(value);
        }

        [ModelDefault("AllowEdit", "False")]
        public decimal DueAmount
        {
            get => GetPropertyValue<decimal>(nameof(DueAmount));
            set => SetPropertyValue(nameof(DueAmount), value);
        }

        [ModelDefault("AllowEdit", "False")]
        public DateTime DueDate
        {
            get => GetPropertyValue<DateTime>(nameof(DueDate));
            set => SetPropertyValue(nameof(DueDate), value);
        }

        [ImmediatePostData]
        public string Identifier
        {
            get => GetPropertyValue<string>(nameof(Identifier));
            set => SetPropertyValue(nameof(Identifier), value);
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool IsCorrected
        {
            get => GetPropertyValue<bool>(nameof(IsCorrected));
            set => SetIsCorrected(value);
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "VAT Included")]
        [ModelDefault("CaptionForFalse", "No")]
        [ModelDefault("CaptionForTrue", "Yes")]
        public bool IsVatIncluded
        {
            get => GetPropertyValue<bool>(nameof(IsVatIncluded));
            set => SetIsVatIncluded(value);
        }

        [Aggregated]
        [Association]
        public XPCollection<InvoiceLine> Lines
        {
            get => GetCollection<InvoiceLine>(nameof(Lines));
        }

        [ModelDefault("AllowEdit", "False")]
        public DateTime PaidDate
        {
            get => GetPropertyValue<DateTime>(nameof(PaidDate));
            set => SetPropertyValue(nameof(PaidDate), value);
        }

        [ModelDefault("AllowClear", "False")]
        [ImmediatePostData]
        public PaymentTerm PaymentTerm
        {
            get => GetPropertyValue<PaymentTerm>(nameof(PaymentTerm));
            set => SetPaymentTerm(value);
        }

        public string Reference
        {
            get => GetPropertyValue<string>(nameof(Reference));
            set => SetPropertyValue(nameof(Reference), value);
        }

        [ImmediatePostData]
        [ModelDefault("AllowEdit", "False")]
        public decimal SubTotal
        {
            get => GetPropertyValue<decimal>(nameof(SubTotal));
            set => SetSubTotal(value);
        }

        [ModelDefault("AllowEdit", "False")]
        public decimal Total
        {
            get => GetPropertyValue<decimal>(nameof(Total));
            set => SetTotal(value);
        }

        [ImmediatePostData]
        public InvoiceType Type
        {
            get => GetPropertyValue<InvoiceType>(nameof(Type));
            set => SetType(value);
        }

        [ImmediatePostData]
        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("Caption", "VAT")]
        public decimal Vat
        {
            get => GetPropertyValue<decimal>(nameof(Vat));
            set => SetVat(value);
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            Date = DateTime.Today;
            PaymentTerm = Session.FindObject<Company>(null).PaymentTerm;
        }

        protected override void OnJournalEntriesListChanged()
        {
            base.OnJournalEntriesListChanged();

            UpdateDueAmount();
            UpdateIsCorrected();
            UpdatePaidDate();
        }

        private void Lines_ListChanged(object sender, ListChangedEventArgs e)
        {
            switch (e.ListChangedType)
            {
                case ListChangedType.ItemAdded:
                case ListChangedType.ItemChanged:
                case ListChangedType.ItemDeleted:
                    OnChanged(nameof(Lines));
                    UpdateSummary();
                    break;
            }
        }

        private void SetDate(DateTime value)
        {
            if (SetPropertyValue(nameof(Date), value))
            {
                if (IsLoading || IsSaving)
                    return;

                UpdateDueDate();
            }
        }

        private void SetIsCorrected(bool value)
        {
            if (SetPropertyValue(nameof(IsCorrected), value))
            {
                if (IsLoading || IsSaving || value)
                    return;

                UpdateSummary();
            }
        }

        private void SetIsVatIncluded(bool value)
        {
            if (SetPropertyValue(nameof(IsVatIncluded), value))
            {
                if (IsLoading || IsSaving)
                    return;

                UpdateSummary();
            }
        }

        private void SetPaymentTerm(PaymentTerm value)
        {
            if (SetPropertyValue(nameof(PaymentTerm), value))
            {
                if (IsLoading || IsSaving)
                    return;

                UpdateDueDate();
            }
        }

        private void SetSubTotal(decimal value)
        {
            if (SetPropertyValue(nameof(SubTotal), value))
            {
                if (IsLoading || IsSaving)
                    return;

                UpdateTotal();
            }
        }

        private void SetTotal(decimal value)
        {
            if (SetPropertyValue(nameof(Total), value))
            {
                if (IsLoading || IsSaving)
                    return;

                UpdateDueAmount();
            }
        }

        private void SetType(InvoiceType value)
        {
            if (SetPropertyValue(nameof(Type), value))
            {
                if (IsLoading || IsSaving)
                    return;

                UpdateSummary();
            }
        }

        private void SetVat(decimal value)
        {
            if (SetPropertyValue(nameof(Vat), value))
            {
                if (IsLoading || IsSaving)
                    return;

                UpdateTotal();
            }
        }

        private void UpdateDueAmount()
        {
            DueAmount = Total - Math.Sign(Total) * JournalEntries.Where(x => x.Type == JournalEntryType.Payment).Sum(x => x.Amount);
        }

        private void UpdateDueDate()
        {
            DueDate = PaymentTerm != null ? Date.AddDays(PaymentTerm.Term) : Date;
        }

        private void UpdateIsCorrected()
        {
            IsCorrected = JournalEntries.Any(x => x.Type == JournalEntryType.Correction);
        }

        private void UpdatePaidDate()
        {
            PaidDate = JournalEntries.Where(x => x.Type == JournalEntryType.Payment).Select(x => x.Date).DefaultIfEmpty().Max();
        }

        private void UpdateSummary()
        {
            SubTotal = Math.Round(Lines.Sum(x => x.SubTotal), 2, MidpointRounding.AwayFromZero);
            Vat = Math.Round(Lines.Sum(x => x.Vat), 2, MidpointRounding.AwayFromZero);
        }

        private void UpdateTotal()
        {
            Total = SubTotal + Vat;
        }
    }
}