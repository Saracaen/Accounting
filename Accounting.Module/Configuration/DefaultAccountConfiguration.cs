using System.Collections.Generic;
using System.Xml.Serialization;

namespace Accounting.Module.Configuration
{
    public class DefaultAccountConfiguration
    {
        [XmlArray("bankAccounts")]
        [XmlArrayItem("bankAccount")]
        public List<DefaultAccount> BankAccounts { get; set; } = new List<DefaultAccount>();

        [XmlArray("cashAccounts")]
        [XmlArrayItem("cashAccount")]
        public List<DefaultAccount> CashAccounts { get; set; } = new List<DefaultAccount>();

        [XmlArray("creditCardAccounts")]
        [XmlArrayItem("creditCardAccount")]
        public List<DefaultAccount> CreditCardAccounts { get; set; } = new List<DefaultAccount>();

        [XmlArray("customerAccounts")]
        [XmlArrayItem("customerAccount")]
        public List<DefaultAccount> CustomerAccounts { get; set; } = new List<DefaultAccount>();

        [XmlArray("depreciationExpenseAccounts")]
        [XmlArrayItem("depreciationExpenseAccount")]
        public List<DefaultAccount> DepreciationExpenseAccounts { get; set; } = new List<DefaultAccount>();

        [XmlArray("equityAccounts")]
        [XmlArrayItem("equityAccount")]
        public List<DefaultAccount> EquityAccounts { get; set; } = new List<DefaultAccount>();

        [XmlArray("expenseAccounts")]
        [XmlArrayItem("expenseAccount")]
        public List<DefaultExpenseAccount> ExpenseAccounts { get; set; } = new List<DefaultExpenseAccount>();

        [XmlArray("incomeAccounts")]
        [XmlArrayItem("incomeAccount")]
        public List<DefaultIncomeAccount> IncomeAccounts { get; set; } = new List<DefaultIncomeAccount>();

        [XmlArray("inputVatAccounts")]
        [XmlArrayItem("inputVatAccount")]
        public List<DefaultAccount> InputVatAccounts { get; set; } = new List<DefaultAccount>();

        [XmlArray("outputVatAccounts")]
        [XmlArrayItem("outputVatAccount")]
        public List<DefaultAccount> OutputVatAccounts { get; set; } = new List<DefaultAccount>();

        [XmlArray("privateAccounts")]
        [XmlArrayItem("privateAccount")]
        public List<DefaultAccount> PrivateAccounts { get; set; } = new List<DefaultAccount>();

        [XmlArray("roundingDifferencesAccounts")]
        [XmlArrayItem("roundingDifferencesAccount")]
        public List<DefaultAccount> RoundingDifferencesAccounts { get; set; } = new List<DefaultAccount>();

        [XmlArray("supplierAccounts")]
        [XmlArrayItem("supplierAccount")]
        public List<DefaultAccount> SupplierAccounts { get; set; } = new List<DefaultAccount>();

        [XmlArray("vatPaymentAccounts")]
        [XmlArrayItem("vatPaymentAccount")]
        public List<DefaultAccount> VatPaymentAccounts { get; set; } = new List<DefaultAccount>();

        [XmlArray("vatToPayAccounts")]
        [XmlArrayItem("vatToPayAccount")]
        public List<DefaultAccount> VatToPayAccounts { get; set; } = new List<DefaultAccount>();
    }
}