using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.ComponentModel;

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
        private decimal _balance = decimal.MinValue;

        protected Account(Session session) : base(session)
        {
        }

        [PersistentAlias("IsNull(JournalEntryLines.Sum(Amount), 0)")]
        public decimal Balance
        {
            get
            {
                if (_balance == decimal.MinValue)
                {
                    var criteria = new BinaryOperator("Oid", Oid);
                    var expression = CriteriaOperator.Parse("Iif(Type = 'Credit', -JournalEntryLines.Sum(Amount), JournalEntryLines.Sum(Amount))");

                    _balance = Convert.ToDecimal(Session.Evaluate<Account>(expression, criteria));
                }

                return _balance;
            }
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
    }
}