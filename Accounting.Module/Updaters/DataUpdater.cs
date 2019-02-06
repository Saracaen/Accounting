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

            if (ObjectSpace.GetObjectsCount(typeof(Company), null) == 0 && ObjectSpace.GetObjectsCount(typeof(Account), null) == 0)
            {
                var defaultConfiguration = DefaultConfiguration.Load("DefaultConfiguration.xml");

                foreach (var defaultBankAccount in defaultConfiguration.Accounts.BankAccounts)
                {
                    var bankAccount = ObjectSpace.CreateObject<BankAccount>();
                    bankAccount.Name = defaultBankAccount.Name;
                }

                foreach (var defaultCashAccount in defaultConfiguration.Accounts.CashAccounts)
                {
                    var cashAccount = ObjectSpace.CreateObject<CashAccount>();
                    cashAccount.Name = defaultCashAccount.Name;
                }

                foreach (var defaultCreditCardAccount in defaultConfiguration.Accounts.CreditCardAccounts)
                {
                    var creditCardAccount = ObjectSpace.CreateObject<CreditCardAccount>();
                    creditCardAccount.Name = defaultCreditCardAccount.Name;
                }

                foreach (var defaultCustomerAccount in defaultConfiguration.Accounts.CustomerAccounts)
                {
                    var customerAccount = ObjectSpace.CreateObject<CustomerAccount>();
                    customerAccount.Name = defaultCustomerAccount.Name;
                }

                foreach (var defaultDepreciationExpenseAccount in defaultConfiguration.Accounts.DepreciationExpenseAccounts)
                {
                    var depreciationExpenseAccount = ObjectSpace.CreateObject<DepreciationExpenseAccount>();
                    depreciationExpenseAccount.Name = defaultDepreciationExpenseAccount.Name;
                }

                foreach (var defaultEquityAccount in defaultConfiguration.Accounts.EquityAccounts)
                {
                    var equityAccount = ObjectSpace.CreateObject<EquityAccount>();
                    equityAccount.Name = defaultEquityAccount.Name;
                }

                foreach (var defaultInputVatAccount in defaultConfiguration.Accounts.InputVatAccounts)
                {
                    var inputVatAccount = ObjectSpace.CreateObject<InputVatAccount>();
                    inputVatAccount.Name = defaultInputVatAccount.Name;
                }

                foreach (var defaultOutputVatAccount in defaultConfiguration.Accounts.OutputVatAccounts)
                {
                    var outputVatAccount = ObjectSpace.CreateObject<OutputVatAccount>();
                    outputVatAccount.Name = defaultOutputVatAccount.Name;
                }

                foreach (var defaultPaymentTerm in defaultConfiguration.PaymentTerms)
                {
                    var paymentTerm = ObjectSpace.CreateObject<PaymentTerm>();
                    paymentTerm.Name = defaultPaymentTerm.Name;
                    paymentTerm.Term = defaultPaymentTerm.Term;
                }

                foreach (var defaultPrivateAccount in defaultConfiguration.Accounts.PrivateAccounts)
                {
                    var privateAccount = ObjectSpace.CreateObject<PrivateAccount>();
                    privateAccount.Name = defaultPrivateAccount.Name;
                }

                foreach (var defaultRoundingDifferencesAccount in defaultConfiguration.Accounts.RoundingDifferencesAccounts)
                {
                    var roundingDifferencesAccount = ObjectSpace.CreateObject<RoundingDifferencesAccount>();
                    roundingDifferencesAccount.Name = defaultRoundingDifferencesAccount.Name;
                }

                foreach (var defaultSupplierAccount in defaultConfiguration.Accounts.SupplierAccounts)
                {
                    var supplierAccount = ObjectSpace.CreateObject<SupplierAccount>();
                    supplierAccount.Name = defaultSupplierAccount.Name;
                }

                foreach (var defaultVatPaymentAccount in defaultConfiguration.Accounts.VatPaymentAccounts)
                {
                    var vatPaymentAccount = ObjectSpace.CreateObject<VatPaymentAccount>();
                    vatPaymentAccount.Name = defaultVatPaymentAccount.Name;
                }

                foreach (var defaultVatRate in defaultConfiguration.VatRates)
                {
                    var vatRate = ObjectSpace.CreateObject<VatRate>();
                    vatRate.Name = defaultVatRate.Name;
                    vatRate.PayableAccount = ObjectSpace.FindObject<OutputVatAccount>(new BinaryOperator("Name", defaultVatRate.PayableAccount));
                    vatRate.PayableCategory = defaultVatRate.PayableCategory;
                    vatRate.Rate = defaultVatRate.Rate;
                    vatRate.ReceivableAccount = ObjectSpace.FindObject<InputVatAccount>(new BinaryOperator("Name", defaultVatRate.ReceivableAccount));
                    vatRate.ReceivableCategory = defaultVatRate.ReceivableCategory;
                }

                foreach (var defaultVatToPayAccount in defaultConfiguration.Accounts.VatToPayAccounts)
                {
                    var vatToPayAccount = ObjectSpace.CreateObject<VatToPayAccount>();
                    vatToPayAccount.Name = defaultVatToPayAccount.Name;
                }

                foreach (var defaultExpenseAccount in defaultConfiguration.Accounts.ExpenseAccounts)
                {
                    var expenseAccount = ObjectSpace.CreateObject<ExpenseAccount>();
                    expenseAccount.DefaultVatRate = ObjectSpace.FindObject<VatRate>(new BinaryOperator("Name", defaultExpenseAccount.DefaultVatRate));
                    expenseAccount.Name = defaultExpenseAccount.Name;
                    expenseAccount.PercentageDeductible = defaultExpenseAccount.PercentageDeductible;
                }

                foreach (var defaultIncomeAccount in defaultConfiguration.Accounts.IncomeAccounts)
                {
                    var expenseAccount = ObjectSpace.CreateObject<IncomeAccount>();
                    expenseAccount.DefaultVatRate = ObjectSpace.FindObject<VatRate>(new BinaryOperator("Name", defaultIncomeAccount.DefaultVatRate));
                    expenseAccount.Name = defaultIncomeAccount.Name;
                }
            }

            if (ObjectSpace.GetObjectsCount(typeof(Identifier), null) == 0)
            {
                var identifier = ObjectSpace.CreateObject<Identifier>();
                identifier.TargetType = typeof(PurchaseInvoice).FullName;
                identifier.Type = IdentifierType.PurchaseInvoice;

                identifier = ObjectSpace.CreateObject<Identifier>();
                identifier.TargetType = typeof(SalesInvoice).FullName;
                identifier.Type = IdentifierType.SalesInvoice;
            }

            ObjectSpace.CommitChanges();
        }
    }
}