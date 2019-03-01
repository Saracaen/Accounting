using System.Xml.Serialization;

namespace Accounting.Module.Configuration
{
    public class DefaultAccountConfiguration
    {
        [XmlArray("bankAccounts")]
        [XmlArrayItem("bankAccount")]
        public DefaultAccount[] BankAccounts { get; set; }

        [XmlArray("cashAccounts")]
        [XmlArrayItem("cashAccount")]
        public DefaultAccount[] CashAccounts { get; set; }

        [XmlArray("creditCardAccounts")]
        [XmlArrayItem("creditCardAccount")]
        public DefaultAccount[] CreditCardAccounts { get; set; }

        [XmlArray("customerAccounts")]
        [XmlArrayItem("customerAccount")]
        public DefaultAccount[] CustomerAccounts { get; set; }

        [XmlArray("depreciationExpenseAccounts")]
        [XmlArrayItem("depreciationExpenseAccount")]
        public DefaultAccount[] DepreciationExpenseAccounts { get; set; }

        [XmlArray("equityAccounts")]
        [XmlArrayItem("equityAccount")]
        public DefaultAccount[] EquityAccounts { get; set; }

        [XmlArray("expenseAccounts")]
        [XmlArrayItem("expenseAccount")]
        public DefaultExpenseAccount[] ExpenseAccounts { get; set; }

        [XmlArray("incomeAccounts")]
        [XmlArrayItem("incomeAccount")]
        public DefaultIncomeAccount[] IncomeAccounts { get; set; }

        [XmlArray("inputVatAccounts")]
        [XmlArrayItem("inputVatAccount")]
        public DefaultAccount[] InputVatAccounts { get; set; }

        [XmlArray("outputVatAccounts")]
        [XmlArrayItem("outputVatAccount")]
        public DefaultAccount[] OutputVatAccounts { get; set; }

        [XmlArray("privateAccounts")]
        [XmlArrayItem("privateAccount")]
        public DefaultAccount[] PrivateAccounts { get; set; }

        [XmlArray("roundingDifferencesAccounts")]
        [XmlArrayItem("roundingDifferencesAccount")]
        public DefaultAccount[] RoundingDifferencesAccounts { get; set; }

        [XmlArray("supplierAccounts")]
        [XmlArrayItem("supplierAccount")]
        public DefaultAccount[] SupplierAccounts { get; set; }

        [XmlArray("vatPaymentAccounts")]
        [XmlArrayItem("vatPaymentAccount")]
        public DefaultAccount[] VatPaymentAccounts { get; set; }

        [XmlArray("vatToPayAccounts")]
        [XmlArrayItem("vatToPayAccount")]
        public DefaultAccount[] VatToPayAccounts { get; set; }
    }
}