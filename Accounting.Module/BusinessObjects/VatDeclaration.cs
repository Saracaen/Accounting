using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.ComponentModel;
using System.Linq;

namespace Accounting.Module.BusinessObjects
{
    [ImageName("BO_Audit_ChangeHistory")]
    [ModelDefault("Caption", "VAT Declaration")]
    [ModelDefault("ObjectCaptionFormat", "{0:Year} - {0:Period}")]
    [RuleCriteria("VatDeclaration_JournalEntries_RuleCriteria", DefaultContexts.Delete, "Not IsPosted", "A VAT declaration must be unposted before it can be deleted.")]
    [VisibleInReports]
    public class VatDeclaration : JournalEntryItem
    {
        public VatDeclaration(Session session) : base(session)
        {
            Lines.ListChanged += Lines_ListChanged;
        }

        [ModelDefault("AllowEdit", "False")]
        public decimal InputVat
        {
            get => GetPropertyValue<decimal>(nameof(InputVat));
            set => SetPropertyValue(nameof(InputVat), value);
        }

        [Aggregated]
        [Association]
        public XPCollection<VatDeclarationLine> Lines
        {
            get => GetCollection<VatDeclarationLine>(nameof(Lines));
        }

        [ModelDefault("AllowEdit", "False")]
        public decimal OutputVat
        {
            get => GetPropertyValue<decimal>(nameof(OutputVat));
            set => SetPropertyValue(nameof(OutputVat), value);
        }

        [ModelDefault("AllowEdit", "False")]
        public DateTime PaidDate
        {
            get => GetPropertyValue<DateTime>(nameof(PaidDate));
            set => SetPropertyValue(nameof(PaidDate), value);
        }

        [ModelDefault("AllowEdit", "False")]
        public VatDeclarationPeriod Period
        {
            get => GetPropertyValue<VatDeclarationPeriod>(nameof(Period));
            set => SetPropertyValue(nameof(Period), value);
        }

        [ModelDefault("AllowEdit", "False")]
        public decimal SmallBusinessScheme
        {
            get => GetPropertyValue<decimal>(nameof(SmallBusinessScheme));
            set => SetPropertyValue(nameof(SmallBusinessScheme), value);
        }

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

        [ModelDefault("AllowEdit", "False")]
        public VatDeclarationType Type
        {
            get => GetPropertyValue<VatDeclarationType>(nameof(Type));
            set => SetPropertyValue(nameof(Type), value);
        }

        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("DisplayFormat", "{0:D0}")]
        [ModelDefault("EditMask", "D0")]
        public int Year { get; set; }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            foreach (VatCategory value in Enum.GetValues(typeof(VatCategory)))
            {
                if (value > VatCategory.None)
                {
                    Lines.Add(new VatDeclarationLine(Session) { Category = value });
                }
            }
        }

        public override string ToString()
        {
            return $"{Year} - {CaptionHelper.GetDisplayText(Period)}";
        }

        protected override void OnJournalEntriesListChanged()
        {
            base.OnJournalEntriesListChanged();
            PaidDate = JournalEntries.Where(x => x.Type == JournalEntryType.Payment).Select(x => x.Date).DefaultIfEmpty().Max();
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

        private void UpdateSummary()
        {
            InputVat = Lines.Where(x => x.Category == VatCategory.InputVat).Sum(x => x.Vat);
            OutputVat = Lines.Where(x => x.Category != VatCategory.InputVat && x.Category != VatCategory.SmallBusinessScheme).Sum(x => x.Vat);
            SmallBusinessScheme = Lines.Where(x => x.Category == VatCategory.SmallBusinessScheme).Sum(x => x.Vat);
            SubTotal = OutputVat - InputVat;
            Total = SubTotal - SmallBusinessScheme;
        }
    }
}