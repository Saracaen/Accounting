using DevExpress.Data.Filtering;
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
            set => SetPropertyValue(nameof(Date), value);
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
            set => SetPropertyValue(nameof(IsCorrected), value);
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "VAT Included")]
        [ModelDefault("CaptionForFalse", "No")]
        [ModelDefault("CaptionForTrue", "Yes")]
        public bool IsVatIncluded
        {
            get => GetPropertyValue<bool>(nameof(IsVatIncluded));
            set => SetPropertyValue(nameof(IsVatIncluded), value);
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
            set => SetPropertyValue(nameof(PaymentTerm), value);
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
            set => SetPropertyValue(nameof(SubTotal), value);
        }

        [ModelDefault("AllowEdit", "False")]
        public decimal Total
        {
            get => GetPropertyValue<decimal>(nameof(Total));
            set => SetPropertyValue(nameof(Total), value);
        }

        [ImmediatePostData]
        public InvoiceType Type
        {
            get => GetPropertyValue<InvoiceType>(nameof(Type));
            set => SetPropertyValue(nameof(Type), value);
        }

        [ImmediatePostData]
        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("Caption", "VAT")]
        public decimal Vat
        {
            get => GetPropertyValue<decimal>(nameof(Vat));
            set => SetPropertyValue(nameof(Vat), value);
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            Date = DateTime.Today;
            PaymentTerm = Session.FindObject<PaymentTerm>(new BinaryOperator("Term", 30));
        }

        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);

            if (IsLoading)
                return;

            switch (propertyName)
            {
                case nameof(Date):
                case nameof(PaymentTerm):
                    UpdateDueDate();
                    break;

                case nameof(IsCorrected):
                    UpdateCorrection();
                    break;

                case nameof(IsVatIncluded):
                case nameof(Type):
                    UpdateSummary();
                    break;

                case nameof(SubTotal):
                case nameof(Vat):
                    UpdateTotal();
                    break;

                case nameof(Total):
                    UpdateDueAmount();
                    break;
            }
        }

        protected override void OnJournalEntriesListChanged()
        {
            base.OnJournalEntriesListChanged();

            UpdateDueAmount();
            UpdatePaidDate();
        }

        private void Lines_ListChanged(object sender, ListChangedEventArgs e)
        {
            switch (e.ListChangedType)
            {
                case ListChangedType.ItemAdded:
                case ListChangedType.ItemChanged:
                case ListChangedType.ItemDeleted:
                case ListChangedType.ItemMoved:
                    OnChanged(nameof(Lines));
                    UpdateSummary();
                    break;
            }
        }

        private void UpdateCorrection()
        {
            if (IsCorrected)
            {
                UpdateTotal();
            }
            else
            {
                UpdateSummary();
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

        private void UpdatePaidDate()
        {
            PaidDate = JournalEntries.Where(x => x.Type == JournalEntryType.Payment).Select(x => x.Date).DefaultIfEmpty().Max();
        }

        private void UpdateSummary()
        {
            SubTotal = Math.Round(Lines.Sum(x => x.SubTotal), 2, MidpointRounding.AwayFromZero);
            Vat = Math.Round(Lines.Sum(x => x.Vat), 2, MidpointRounding.AwayFromZero);

            UpdateTotal();
        }

        private void UpdateTotal()
        {
            Total = SubTotal + Vat;
        }
    }
}