using System.Collections.Generic;
using System.Xml.Serialization;

namespace Accounting.Module.Configuration
{
    public class DefaultAccountConfiguration
    {
        [XmlArrayItem("BankAccount")]
        public List<DefaultAccount> BankAccounts { get; set; } = new List<DefaultAccount>();

        [XmlArrayItem("CashAccount")]
        public List<DefaultAccount> CashAccounts { get; set; } = new List<DefaultAccount>();

        [XmlArrayItem("CreditCardAccount")]
        public List<DefaultAccount> CreditCardAccounts { get; set; } = new List<DefaultAccount>();

        [XmlArrayItem("CustomerAccount")]
        public List<DefaultAccount> CustomerAccounts { get; set; } = new List<DefaultAccount>();

        [XmlArrayItem("DepreciationExpenseAccount")]
        public List<DefaultAccount> DepreciationExpenseAccounts { get; set; } = new List<DefaultAccount>();

        [XmlArrayItem("EquityAccount")]
        public List<DefaultAccount> EquityAccounts { get; set; } = new List<DefaultAccount>();

        [XmlArrayItem("ExpenseAccount")]
        public List<DefaultExpenseAccount> ExpenseAccounts { get; set; } = new List<DefaultExpenseAccount>();

        [XmlArrayItem("CustomerAccount")]
        public List<DefaultIncomeAccount> IncomeAccounts { get; set; } = new List<DefaultIncomeAccount>();

        [XmlArrayItem("InputVatAccount")]
        public List<DefaultAccount> InputVatAccounts { get; set; } = new List<DefaultAccount>();

        [XmlArrayItem("OutputVatAccount")]
        public List<DefaultAccount> OutputVatAccounts { get; set; } = new List<DefaultAccount>();

        [XmlArrayItem("PrivateAccount")]
        public List<DefaultAccount> PrivateAccounts { get; set; } = new List<DefaultAccount>();

        [XmlArrayItem("RoundingDifferencesAccount")]
        public List<DefaultAccount> RoundingDifferencesAccounts { get; set; } = new List<DefaultAccount>();

        [XmlArrayItem("SupplierAccount")]
        public List<DefaultAccount> SupplierAccounts { get; set; } = new List<DefaultAccount>();

        [XmlArrayItem("VatPaymentAccount")]
        public List<DefaultAccount> VatPaymentAccounts { get; set; } = new List<DefaultAccount>();

        [XmlArrayItem("VatToPayAccount")]
        public List<DefaultAccount> VatToPayAccounts { get; set; } = new List<DefaultAccount>();
    }
}