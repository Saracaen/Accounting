using DevExpress.ExpressApp.ConditionalAppearance;
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
    [Appearance("Delete", AppearanceItemType.Action, "IsExactType(This, 'Accounting.Module.BusinessObjects.CustomerAccount') Or IsExactType(This, 'Accounting.Module.BusinessObjects.EquityAccount') Or IsExactType(This, 'Accounting.Module.BusinessObjects.PrivateAccount') Or IsExactType(This, 'Accounting.Module.BusinessObjects.RoundingDifferencesAccount') Or IsExactType(This, 'Accounting.Module.BusinessObjects.SupplierAccount') Or IsExactType(This, 'Accounting.Module.BusinessObjects.VatPaymentAccount') Or IsExactType(This, 'Accounting.Module.BusinessObjects.VatToPayAccount')", TargetItems = "Delete", Enabled = false)]
    [DefaultProperty("Name")]
    [ImageName("BO_Category")]
    [ModelDefault("DefaultLookupEditorMode", "AllItems")]
    [RuleCriteria("Account_InvoiceLine_RuleCriteria", DefaultContexts.Delete, "[<InvoiceLine>][Account = ^.Oid].Count() = 0", "Accounts that are referenced by invoice lines cannot be deleted. Update the invoice lines first, and then delete the account.")]
    [RuleCriteria("Account_JournalEntryLines_RuleCriteria", DefaultContexts.Delete, "JournalEntryLines.Count() = 0", "Accounts that are part of journal entries cannot be deleted. Delete the journal entries first, and then delete the account.")]
    [RuleCriteria("Account_VatRate_RuleCriteria", DefaultContexts.Delete, "[<VatRate>][PayableAccount = ^.Oid Or ReceivableAccount = ^.Oid].Count() = 0", "Accounts that are referenced by VAT rates cannot be deleted. Update the VAT rates first, and then delete the account.")]
    [VisibleInReports]
    public abstract class Account : BaseObject
    {
        protected Account(Session session) : base(session)
        {
            JournalEntryLines.ListChanged += JournalEntryLines_ListChanged;
        }

        [ModelDefault("AllowEdit", "False")]
        public decimal Balance
        {
            get => GetPropertyValue<decimal>(nameof(Balance));
            set => SetPropertyValue(nameof(Balance), value);
        }

        [ModelDefault("AllowEdit", "False")]
        public AccountCategory Category
        {
            get => GetPropertyValue<AccountCategory>(nameof(Category));
            set => SetPropertyValue(nameof(Category), value);
        }

        [Association]
        [ModelDefault("AllowEdit", "False")]
        public XPCollection<JournalEntryLine> JournalEntryLines
        {
            get => GetCollection<JournalEntryLine>(nameof(JournalEntryLines));
        }

        [RuleRequiredField("Account_Name_RuleRequiredField", DefaultContexts.Save)]
        [RuleUniqueValue("Account_Name_RuleUniqueValue", DefaultContexts.Save)]
        public string Name
        {
            get => GetPropertyValue<string>(nameof(Name));
            set => SetPropertyValue(nameof(Name), value);
        }

        [ModelDefault("AllowEdit", "False")]
        public AccountType Type
        {
            get => GetPropertyValue<AccountType>(nameof(Type));
            set => SetPropertyValue(nameof(Type), value);
        }

        private void JournalEntryLines_ListChanged(object sender, ListChangedEventArgs e)
        {
            switch (e.ListChangedType)
            {
                case ListChangedType.ItemAdded:
                case ListChangedType.ItemChanged:
                case ListChangedType.ItemDeleted:
                    OnChanged(nameof(JournalEntryLines));
                    UpdateBalance();
                    break;
            }
        }

        private void UpdateBalance()
        {
            switch (Type)
            {
                case AccountType.Credit:
                    Balance = -JournalEntryLines.Sum(x => x.Amount);
                    break;

                case AccountType.Debit:
                    Balance = JournalEntryLines.Sum(x => x.Amount);
                    break;

                default:
                    throw new InvalidOperationException(CaptionHelper.GetLocalizedText(@"Exceptions\UserVisibleExceptions", "UnsupportedAccountType"));
            }
        }
    }
}