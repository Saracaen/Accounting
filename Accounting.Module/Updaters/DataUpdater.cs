using Accounting.Module.BusinessObjects;
using Accounting.Module.Configuration;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Updating;
using System;

namespace Accounting.Module.Updaters
{
    internal class DataUpdater : ModuleUpdater
    {
        public DataUpdater(IObjectSpace objectSpace, Version currentDBVersion) : base(objectSpace, currentDBVersion)
        {
        }

        public override void UpdateDatabaseAfterUpdateSchema()
        {
            base.UpdateDatabaseAfterUpdateSchema();

            if (ObjectSpace.GetObjectsCount(typeof(Account), null) == 0)
            {
                foreach (var defaultBankAccount in DefaultConfiguration.Instance.Accounts.BankAccounts)
                {
                    var bankAccount = ObjectSpace.CreateObject<BankAccount>();
                    bankAccount.Name = defaultBankAccount.Name;
                }

                foreach (var defaultCashAccount in DefaultConfiguration.Instance.Accounts.CashAccounts)
                {
                    var cashAccount = ObjectSpace.CreateObject<CashAccount>();
                    cashAccount.Name = defaultCashAccount.Name;
                }

                foreach (var defaultCreditCardAccount in DefaultConfiguration.Instance.Accounts.CreditCardAccounts)
                {
                    var creditCardAccount = ObjectSpace.CreateObject<CreditCardAccount>();
                    creditCardAccount.Name = defaultCreditCardAccount.Name;
                }

                foreach (var defaultCustomerAccount in DefaultConfiguration.Instance.Accounts.CustomerAccounts)
                {
                    var customerAccount = ObjectSpace.CreateObject<CustomerAccount>();
                    customerAccount.Name = defaultCustomerAccount.Name;
                }

                foreach (var defaultDepreciationExpenseAccount in DefaultConfiguration.Instance.Accounts.DepreciationExpenseAccounts)
                {
                    var depreciationExpenseAccount = ObjectSpace.CreateObject<DepreciationExpenseAccount>();
                    depreciationExpenseAccount.Name = defaultDepreciationExpenseAccount.Name;
                }

                foreach (var defaultEquityAccount in DefaultConfiguration.Instance.Accounts.EquityAccounts)
                {
                    var equityAccount = ObjectSpace.CreateObject<EquityAccount>();
                    equityAccount.Name = defaultEquityAccount.Name;
                }

                foreach (var defaultInputVatAccount in DefaultConfiguration.Instance.Accounts.InputVatAccounts)
                {
                    var inputVatAccount = ObjectSpace.CreateObject<InputVatAccount>();
                    inputVatAccount.Name = defaultInputVatAccount.Name;
                }

                foreach (var defaultOutputVatAccount in DefaultConfiguration.Instance.Accounts.OutputVatAccounts)
                {
                    var outputVatAccount = ObjectSpace.CreateObject<OutputVatAccount>();
                    outputVatAccount.Name = defaultOutputVatAccount.Name;
                }

                foreach (var defaultPrivateAccount in DefaultConfiguration.Instance.Accounts.PrivateAccounts)
                {
                    var privateAccount = ObjectSpace.CreateObject<PrivateAccount>();
                    privateAccount.Name = defaultPrivateAccount.Name;
                }

                foreach (var defaultRoundingDifferencesAccount in DefaultConfiguration.Instance.Accounts.RoundingDifferencesAccounts)
                {
                    var roundingDifferencesAccount = ObjectSpace.CreateObject<RoundingDifferencesAccount>();
                    roundingDifferencesAccount.Name = defaultRoundingDifferencesAccount.Name;
                }

                foreach (var defaultSupplierAccount in DefaultConfiguration.Instance.Accounts.SupplierAccounts)
                {
                    var supplierAccount = ObjectSpace.CreateObject<SupplierAccount>();
                    supplierAccount.Name = defaultSupplierAccount.Name;
                }

                foreach (var defaultVatPaymentAccount in DefaultConfiguration.Instance.Accounts.VatPaymentAccounts)
                {
                    var vatPaymentAccount = ObjectSpace.CreateObject<VatPaymentAccount>();
                    vatPaymentAccount.Name = defaultVatPaymentAccount.Name;
                }

                if (ObjectSpace.GetObjectsCount(typeof(VatRate), null) == 0)
                {
                    foreach (var defaultVatRate in DefaultConfiguration.Instance.VatRates)
                    {
                        var vatRate = ObjectSpace.CreateObject<VatRate>();
                        vatRate.Name = defaultVatRate.Name;
                        vatRate.PayableAccount = ObjectSpace.FindObject<OutputVatAccount>(new BinaryOperator("Name", defaultVatRate.PayableAccount));
                        vatRate.PayableCategory = defaultVatRate.PayableCategory;
                        vatRate.Rate = defaultVatRate.Rate;
                        vatRate.ReceivableAccount = ObjectSpace.FindObject<InputVatAccount>(new BinaryOperator("Name", defaultVatRate.ReceivableAccount));
                        vatRate.ReceivableCategory = defaultVatRate.ReceivableCategory;
                    }
                }

                foreach (var defaultVatToPayAccount in DefaultConfiguration.Instance.Accounts.VatToPayAccounts)
                {
                    var vatToPayAccount = ObjectSpace.CreateObject<VatToPayAccount>();
                    vatToPayAccount.Name = defaultVatToPayAccount.Name;
                }

                foreach (var defaultExpenseAccount in DefaultConfiguration.Instance.Accounts.ExpenseAccounts)
                {
                    var expenseAccount = ObjectSpace.CreateObject<ExpenseAccount>();
                    expenseAccount.DefaultVatRate = ObjectSpace.FindObject<VatRate>(new BinaryOperator("Name", defaultExpenseAccount.DefaultVatRate));
                    expenseAccount.Name = defaultExpenseAccount.Name;
                    expenseAccount.PercentageDeductible = defaultExpenseAccount.PercentageDeductible;
                }

                foreach (var defaultIncomeAccount in DefaultConfiguration.Instance.Accounts.IncomeAccounts)
                {
                    var expenseAccount = ObjectSpace.CreateObject<IncomeAccount>();
                    expenseAccount.DefaultVatRate = ObjectSpace.FindObject<VatRate>(new BinaryOperator("Name", defaultIncomeAccount.DefaultVatRate));
                    expenseAccount.Name = defaultIncomeAccount.Name;
                }
            }

            if (ObjectSpace.GetObjectsCount(typeof(Country), null) == 0)
            {
                foreach (var defaultCountry in DefaultConfiguration.Instance.Countries.Countries)
                {
                    var country = ObjectSpace.CreateObject<Country>();
                    country.Code = defaultCountry.Code;
                    country.Name = defaultCountry.Name;
                }
            }

            if (ObjectSpace.GetObjectsCount(typeof(Identifier), null) == 0)
            {
                var identifier = ObjectSpace.CreateObject<Identifier>();
                identifier.TargetType = typeof(PurchaseInvoice).FullName;

                identifier = ObjectSpace.CreateObject<Identifier>();
                identifier.TargetType = typeof(SalesInvoice).FullName;
            }

            if (ObjectSpace.GetObjectsCount(typeof(PaymentTerm), null) == 0)
            {
                foreach (var defaultPaymentTerm in DefaultConfiguration.Instance.PaymentTerms)
                {
                    var paymentTerm = ObjectSpace.CreateObject<PaymentTerm>();
                    paymentTerm.Name = defaultPaymentTerm.Name;
                    paymentTerm.Term = defaultPaymentTerm.Term;
                }
            }

            ObjectSpace.CommitChanges();
        }
    }
}