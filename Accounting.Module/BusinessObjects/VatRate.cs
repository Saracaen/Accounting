using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System.ComponentModel;

namespace Accounting.Module.BusinessObjects
{
    [Appearance("New", "IsNewObject(This) = False", TargetItems = "*;Name", Enabled = false)]
    [DefaultProperty("Name")]
    [ImageName("Action_Inline_New")]
    [ModelDefault("Caption", "VAT Rate")]
    [RuleCriteria("VatRate_ExpenseAccount_RuleCriteria", DefaultContexts.Delete, "[<ExpenseAccount>][DefaultVatRate = ^.Oid].Count() = 0", "VAT rates that are referenced by expense accounts cannot be deleted. Update the expense accounts first, and then delete the VAT rate.")]
    [RuleCriteria("VatRate_IncomeAccount_RuleCriteria", DefaultContexts.Delete, "[<IncomeAccount>][DefaultVatRate = ^.Oid].Count() = 0", "VAT rates that are referenced by income accounts cannot be deleted. Update the income accounts first, and then delete the VAT rate.")]
    [RuleCriteria("VatRate_InvoiceLine_RuleCriteria", DefaultContexts.Delete, "[<InvoiceLine>][VatRate = ^.Oid].Count() = 0", "VAT rates that are referenced by invoice lines cannot be deleted. Update the invoice lines first, and then delete the VAT rate.")]
    public class VatRate : BaseObject
    {
        public VatRate(Session session) : base(session)
        {
        }

        [RuleRequiredField("VatRate_Name_RuleRequiredField", DefaultContexts.Save)]
        [RuleUniqueValue("VatRate_Name_RuleUniqueValue", DefaultContexts.Save)]
        public string Name
        {
            get => GetPropertyValue<string>(nameof(Name));
            set => SetPropertyValue(nameof(Name), value);
        }

        [VisibleInReports(false)]
        public OutputVatAccount PayableAccount
        {
            get => GetPropertyValue<OutputVatAccount>(nameof(PayableAccount));
            set => SetPropertyValue(nameof(PayableAccount), value);
        }

        [VisibleInReports(false)]
        public VatCategory PayableCategory
        {
            get => GetPropertyValue<VatCategory>(nameof(PayableCategory));
            set => SetPropertyValue(nameof(PayableCategory), value);
        }

        [ModelDefault("DisplayFormat", "{0:N0}%")]
        [ModelDefault("EditMask", "P0")]
        [RuleRange("VatRate_Rate_RuleRange", DefaultContexts.Save, 0, 100)]
        public float Rate
        {
            get => GetPropertyValue<float>(nameof(Rate));
            set => SetPropertyValue(nameof(Rate), value);
        }

        [VisibleInReports(false)]
        public InputVatAccount ReceivableAccount
        {
            get => GetPropertyValue<InputVatAccount>(nameof(ReceivableAccount));
            set => SetPropertyValue(nameof(ReceivableAccount), value);
        }

        [VisibleInReports(false)]
        public VatCategory ReceivableCategory
        {
            get => GetPropertyValue<VatCategory>(nameof(ReceivableCategory));
            set => SetPropertyValue(nameof(ReceivableCategory), value);
        }
    }
}