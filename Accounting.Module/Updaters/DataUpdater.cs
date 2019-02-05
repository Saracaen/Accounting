using Accounting.Module.BusinessObjects;
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

            if (ObjectSpace.GetObjectsCount(typeof(BankAccount), null) == 0)
            {
                var bankAccount = ObjectSpace.CreateObject<BankAccount>();
                bankAccount.Name = "Betaalrekening";
            }

            if (ObjectSpace.GetObjectsCount(typeof(CashAccount), null) == 0)
            {
                var cashAccount = ObjectSpace.CreateObject<CashAccount>();
                cashAccount.Name = "Kas";
            }

            if (ObjectSpace.GetObjectsCount(typeof(CreditCardAccount), null) == 0)
            {
                var creditCardAccount = ObjectSpace.CreateObject<CreditCardAccount>();
                creditCardAccount.Name = "Creditcard";
            }

            if (ObjectSpace.GetObjectsCount(typeof(CustomerAccount), null) == 0)
            {
                var customerAccount = ObjectSpace.CreateObject<CustomerAccount>();
                customerAccount.Name = "Klanten";
            }

            if (ObjectSpace.GetObjectsCount(typeof(DepreciationExpenseAccount), null) == 0)
            {
                var depreciationAccount = ObjectSpace.CreateObject<DepreciationExpenseAccount>();
                depreciationAccount.Name = "Afschrijvingen";
            }

            if (ObjectSpace.GetObjectsCount(typeof(EquityAccount), null) == 0)
            {
                var equityAccount = ObjectSpace.CreateObject<EquityAccount>();
                equityAccount.Name = "Eigen vermogen";
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

            if (ObjectSpace.GetObjectsCount(typeof(InputVatAccount), null) == 0)
            {
                var inputVatAccount = ObjectSpace.CreateObject<InputVatAccount>();
                inputVatAccount.Name = "BTW Voorbelasting";

                inputVatAccount = ObjectSpace.CreateObject<InputVatAccount>();
                inputVatAccount.Name = "BTW KOR";
            }

            if (ObjectSpace.GetObjectsCount(typeof(OutputVatAccount), null) == 0)
            {
                var outputVatAccount = ObjectSpace.CreateObject<OutputVatAccount>();
                outputVatAccount.Name = "BTW Installatie/afstandsverkopen binnen de EU";

                outputVatAccount = ObjectSpace.CreateObject<OutputVatAccount>();
                outputVatAccount.Name = "BTW Leveringen naar landen buiten de EU (uitvoer)";

                outputVatAccount = ObjectSpace.CreateObject<OutputVatAccount>();
                outputVatAccount.Name = "BTW Leveringen naar/diensten in landen binnen de EU";

                outputVatAccount = ObjectSpace.CreateObject<OutputVatAccount>();
                outputVatAccount.Name = "BTW Leveringen/diensten belast met 0% of niet bij u belast";

                outputVatAccount = ObjectSpace.CreateObject<OutputVatAccount>();
                outputVatAccount.Name = "BTW Leveringen/diensten belast met 21%";

                outputVatAccount = ObjectSpace.CreateObject<OutputVatAccount>();
                outputVatAccount.Name = "BTW Leveringen/diensten belast met 9%";

                outputVatAccount = ObjectSpace.CreateObject<OutputVatAccount>();
                outputVatAccount.Name = "BTW Leveringen/diensten belast met overige tarieven behalve 0%";

                outputVatAccount = ObjectSpace.CreateObject<OutputVatAccount>();
                outputVatAccount.Name = "BTW Leveringen/diensten uit landen binnen de EU";

                outputVatAccount = ObjectSpace.CreateObject<OutputVatAccount>();
                outputVatAccount.Name = "BTW Leveringen/diensten uit landen buiten de EU";

                outputVatAccount = ObjectSpace.CreateObject<OutputVatAccount>();
                outputVatAccount.Name = "BTW Leveringen/diensten waarbij de heffing van omzetbelasting naar u is verlegd";

                outputVatAccount = ObjectSpace.CreateObject<OutputVatAccount>();
                outputVatAccount.Name = "BTW Privé-gebruik";
            }

            if (ObjectSpace.GetObjectsCount(typeof(PaymentTerm), null) == 0)
            {
                var paymentTerm = ObjectSpace.CreateObject<PaymentTerm>();
                paymentTerm.Name = "Geen";
                paymentTerm.Term = 0;

                paymentTerm = ObjectSpace.CreateObject<PaymentTerm>();
                paymentTerm.Name = "7 dagen";
                paymentTerm.Term = 7;

                paymentTerm = ObjectSpace.CreateObject<PaymentTerm>();
                paymentTerm.Name = "14 dagen";
                paymentTerm.Term = 14;

                paymentTerm = ObjectSpace.CreateObject<PaymentTerm>();
                paymentTerm.Name = "30 dagen";
                paymentTerm.Term = 30;
            }

            if (ObjectSpace.GetObjectsCount(typeof(PrivateAccount), null) == 0)
            {
                var privateAccount = ObjectSpace.CreateObject<PrivateAccount>();
                privateAccount.Name = "Privé";
            }

            if (ObjectSpace.GetObjectsCount(typeof(RoundingDifferencesAccount), null) == 0)
            {
                var roundingDifferencesAccount = ObjectSpace.CreateObject<RoundingDifferencesAccount>();
                roundingDifferencesAccount.Name = "Afrondingsverschillen";
            }

            if (ObjectSpace.GetObjectsCount(typeof(SupplierAccount), null) == 0)
            {
                var supplierAccount = ObjectSpace.CreateObject<SupplierAccount>();
                supplierAccount.Name = "Leveranciers";
            }

            if (ObjectSpace.GetObjectsCount(typeof(VatPaymentAccount), null) == 0)
            {
                var vatPaymentAccount = ObjectSpace.CreateObject<VatPaymentAccount>();
                vatPaymentAccount.Name = "BTW Afdrachten";
            }

            if (ObjectSpace.GetObjectsCount(typeof(VatRate), null) == 0)
            {
                var vatRate = ObjectSpace.CreateObject<VatRate>();
                vatRate.Name = "Geen / Vrijgesteld";
                vatRate.Rate = 0;

                vatRate = ObjectSpace.CreateObject<VatRate>();
                vatRate.Name = "Privé-gebruik";
                vatRate.PayableAccount = ObjectSpace.FindObject<OutputVatAccount>(new BinaryOperator("Name", "BTW Privé-gebruik"));
                vatRate.PayableCategory = VatCategory.PrivateUse;
                vatRate.Rate = 0;

                vatRate = ObjectSpace.CreateObject<VatRate>();
                vatRate.Name = "Hoog 21% (af te dragen)";
                vatRate.PayableAccount = ObjectSpace.FindObject<OutputVatAccount>(new BinaryOperator("Name", "BTW Leveringen/diensten belast met 21%"));
                vatRate.PayableCategory = VatCategory.DeliveriesOrServicesTaxedAtHighRate;
                vatRate.Rate = 21;

                vatRate = ObjectSpace.CreateObject<VatRate>();
                vatRate.Name = "Hoog 21% (te vorderen)";
                vatRate.Rate = 21;
                vatRate.ReceivableAccount = ObjectSpace.FindObject<InputVatAccount>(new BinaryOperator("Name", "BTW Voorbelasting"));
                vatRate.ReceivableCategory = VatCategory.InputVat;

                vatRate = ObjectSpace.CreateObject<VatRate>();
                vatRate.Name = "Laag 9% (af te dragen)";
                vatRate.PayableAccount = ObjectSpace.FindObject<OutputVatAccount>(new BinaryOperator("Name", "BTW Leveringen/diensten belast met 9%"));
                vatRate.PayableCategory = VatCategory.DeliveriesOrServicesTaxedAtLowRate;
                vatRate.Rate = 9;

                vatRate = ObjectSpace.CreateObject<VatRate>();
                vatRate.Name = "Laag 9% (te vorderen)";
                vatRate.Rate = 9;
                vatRate.ReceivableAccount = ObjectSpace.FindObject<InputVatAccount>(new BinaryOperator("Name", "BTW Voorbelasting"));
                vatRate.ReceivableCategory = VatCategory.InputVat;

                vatRate = ObjectSpace.CreateObject<VatRate>();
                vatRate.Name = "Leveringen naar of diensten in landen binnen de EU";
                vatRate.PayableAccount = ObjectSpace.FindObject<OutputVatAccount>(new BinaryOperator("Name", "BTW Leveringen naar/diensten in landen binnen de EU"));
                vatRate.PayableCategory = VatCategory.DeliveriesOrServicesToCountriesWithinTheEuropeanUnion;

                vatRate = ObjectSpace.CreateObject<VatRate>();
                vatRate.Name = "Verwerving binnen de EU (21%)";
                vatRate.PayableAccount = ObjectSpace.FindObject<OutputVatAccount>(new BinaryOperator("Name", "BTW Leveringen/diensten uit landen binnen de EU"));
                vatRate.PayableCategory = VatCategory.DeliveriesOrServicesFromCountriesWithinTheEuropeanUnion;
                vatRate.Rate = 21;
                vatRate.ReceivableAccount = ObjectSpace.FindObject<InputVatAccount>(new BinaryOperator("Name", "BTW Voorbelasting"));
                vatRate.ReceivableCategory = VatCategory.InputVat;

                vatRate = ObjectSpace.CreateObject<VatRate>();
                vatRate.Name = "Verwerving binnen de EU (9%)";
                vatRate.PayableAccount = ObjectSpace.FindObject<OutputVatAccount>(new BinaryOperator("Name", "BTW Leveringen/diensten uit landen binnen de EU"));
                vatRate.PayableCategory = VatCategory.DeliveriesOrServicesFromCountriesWithinTheEuropeanUnion;
                vatRate.Rate = 9;
                vatRate.ReceivableAccount = ObjectSpace.FindObject<InputVatAccount>(new BinaryOperator("Name", "BTW Voorbelasting"));
                vatRate.ReceivableCategory = VatCategory.InputVat;

                vatRate = ObjectSpace.CreateObject<VatRate>();
                vatRate.Name = "Verwerving binnen de EU (0%)";
                vatRate.PayableAccount = ObjectSpace.FindObject<OutputVatAccount>(new BinaryOperator("Name", "BTW Leveringen/diensten uit landen binnen de EU"));
                vatRate.PayableCategory = VatCategory.DeliveriesOrServicesFromCountriesWithinTheEuropeanUnion;
                vatRate.ReceivableAccount = ObjectSpace.FindObject<InputVatAccount>(new BinaryOperator("Name", "BTW Voorbelasting"));
                vatRate.ReceivableCategory = VatCategory.InputVat;

                vatRate = ObjectSpace.CreateObject<VatRate>();
                vatRate.Name = "Levering naar landen buiten de EU (uitvoer)";
                vatRate.PayableAccount = ObjectSpace.FindObject<OutputVatAccount>(new BinaryOperator("Name", "BTW Leveringen naar landen buiten de EU (uitvoer)"));
                vatRate.PayableCategory = VatCategory.DeliveriesToCountriesOutsideTheEuropeanUnion;

                vatRate = ObjectSpace.CreateObject<VatRate>();
                vatRate.Name = "Verwerving buiten de EU (21%)";
                vatRate.PayableAccount = ObjectSpace.FindObject<OutputVatAccount>(new BinaryOperator("Name", "BTW Leveringen/diensten uit landen buiten de EU"));
                vatRate.PayableCategory = VatCategory.DeliveriesOrServicesFromCountriesOutsideTheEuropeanUnion;
                vatRate.Rate = 21;
                vatRate.ReceivableAccount = ObjectSpace.FindObject<InputVatAccount>(new BinaryOperator("Name", "BTW Voorbelasting"));
                vatRate.ReceivableCategory = VatCategory.InputVat;

                vatRate = ObjectSpace.CreateObject<VatRate>();
                vatRate.Name = "Verwerving buiten de EU (9%)";
                vatRate.PayableAccount = ObjectSpace.FindObject<OutputVatAccount>(new BinaryOperator("Name", "BTW Leveringen/diensten uit landen buiten de EU"));
                vatRate.PayableCategory = VatCategory.DeliveriesOrServicesFromCountriesOutsideTheEuropeanUnion;
                vatRate.Rate = 9;
                vatRate.ReceivableAccount = ObjectSpace.FindObject<InputVatAccount>(new BinaryOperator("Name", "BTW Voorbelasting"));
                vatRate.ReceivableCategory = VatCategory.InputVat;

                vatRate = ObjectSpace.CreateObject<VatRate>();
                vatRate.Name = "Verwerving buiten de EU (0%)";
                vatRate.PayableAccount = ObjectSpace.FindObject<OutputVatAccount>(new BinaryOperator("Name", "BTW Leveringen/diensten uit landen buiten de EU"));
                vatRate.PayableCategory = VatCategory.DeliveriesOrServicesFromCountriesOutsideTheEuropeanUnion;
                vatRate.ReceivableAccount = ObjectSpace.FindObject<InputVatAccount>(new BinaryOperator("Name", "BTW Voorbelasting"));
                vatRate.ReceivableCategory = VatCategory.InputVat;

                vatRate = ObjectSpace.CreateObject<VatRate>();
                vatRate.Name = "BTW KOR";
                vatRate.ReceivableAccount = ObjectSpace.FindObject<InputVatAccount>(new BinaryOperator("Name", "BTW KOR"));
                vatRate.ReceivableCategory = VatCategory.SmallBusinessScheme;
            }

            if (ObjectSpace.GetObjectsCount(typeof(VatToPayAccount), null) == 0)
            {
                var vatToPayAccount = ObjectSpace.CreateObject<VatToPayAccount>();
                vatToPayAccount.Name = "BTW Te betalen";
            }

            if (ObjectSpace.GetObjectsCount(typeof(ExpenseAccount), null) == 0)
            {
                var expenseAccount = ObjectSpace.CreateObject<ExpenseAccount>();
                expenseAccount.DefaultVatRate = ObjectSpace.FindObject<VatRate>(new BinaryOperator("Name", "Hoog 21% (te vorderen)"));
                expenseAccount.Name = "Accountants- en administratiekosten";

                expenseAccount = ObjectSpace.CreateObject<ExpenseAccount>();
                expenseAccount.DefaultVatRate = ObjectSpace.FindObject<VatRate>(new BinaryOperator("Name", "Hoog 21% (te vorderen)"));
                expenseAccount.Name = "Advertenties";

                expenseAccount = ObjectSpace.CreateObject<ExpenseAccount>();
                expenseAccount.DefaultVatRate = ObjectSpace.FindObject<VatRate>(new BinaryOperator("Name", "Hoog 21% (te vorderen)"));
                expenseAccount.Name = "Autokosten";

                expenseAccount = ObjectSpace.CreateObject<ExpenseAccount>();
                expenseAccount.DefaultVatRate = ObjectSpace.FindObject<VatRate>(new BinaryOperator("Name", "Hoog 21% (te vorderen)"));
                expenseAccount.Name = "Automatiseringskosten";

                expenseAccount = ObjectSpace.CreateObject<ExpenseAccount>();
                expenseAccount.DefaultVatRate = ObjectSpace.FindObject<VatRate>(new BinaryOperator("Name", "Geen / Vrijgesteld"));
                expenseAccount.Name = "Bankkosten";

                expenseAccount = ObjectSpace.CreateObject<ExpenseAccount>();
                expenseAccount.DefaultVatRate = ObjectSpace.FindObject<VatRate>(new BinaryOperator("Name", "Hoog 21% (te vorderen)"));
                expenseAccount.Name = "Beurskosten";

                expenseAccount = ObjectSpace.CreateObject<ExpenseAccount>();
                expenseAccount.DefaultVatRate = ObjectSpace.FindObject<VatRate>(new BinaryOperator("Name", "Hoog 21% (te vorderen)"));
                expenseAccount.Name = "Brandstof";

                expenseAccount = ObjectSpace.CreateObject<ExpenseAccount>();
                expenseAccount.DefaultVatRate = ObjectSpace.FindObject<VatRate>(new BinaryOperator("Name", "Hoog 21% (te vorderen)"));
                expenseAccount.Name = "Contributies en abonnementen";

                expenseAccount = ObjectSpace.CreateObject<ExpenseAccount>();
                expenseAccount.DefaultVatRate = ObjectSpace.FindObject<VatRate>(new BinaryOperator("Name", "Laag 9% (te vorderen)"));
                expenseAccount.Name = "Eten & Drinken (op kantoor)";
                expenseAccount.PercentageDeductible = 80;

                expenseAccount = ObjectSpace.CreateObject<ExpenseAccount>();
                expenseAccount.DefaultVatRate = ObjectSpace.FindObject<VatRate>(new BinaryOperator("Name", "Hoog 21% (te vorderen)"));
                expenseAccount.Name = "Gas, water en licht";

                expenseAccount = ObjectSpace.CreateObject<ExpenseAccount>();
                expenseAccount.DefaultVatRate = ObjectSpace.FindObject<VatRate>(new BinaryOperator("Name", "Geen / Vrijgesteld"));
                expenseAccount.Name = "Huur bedrijfspand";

                expenseAccount = ObjectSpace.CreateObject<ExpenseAccount>();
                expenseAccount.DefaultVatRate = ObjectSpace.FindObject<VatRate>(new BinaryOperator("Name", "Hoog 21% (te vorderen)"));
                expenseAccount.Name = "Kantoorbenodigdheden";

                expenseAccount = ObjectSpace.CreateObject<ExpenseAccount>();
                expenseAccount.DefaultVatRate = ObjectSpace.FindObject<VatRate>(new BinaryOperator("Name", "Geen / Vrijgesteld"));
                expenseAccount.Name = "Kilometervergoeding (€ 0,19 km)";

                expenseAccount = ObjectSpace.CreateObject<ExpenseAccount>();
                expenseAccount.DefaultVatRate = ObjectSpace.FindObject<VatRate>(new BinaryOperator("Name", "Geen / Vrijgesteld"));
                expenseAccount.Name = "Motorrijtuigenbelasting";

                expenseAccount = ObjectSpace.CreateObject<ExpenseAccount>();
                expenseAccount.DefaultVatRate = ObjectSpace.FindObject<VatRate>(new BinaryOperator("Name", "Geen / Vrijgesteld"));
                expenseAccount.Name = "Niet aftrekbare kosten (boetes)";

                expenseAccount = ObjectSpace.CreateObject<ExpenseAccount>();
                expenseAccount.DefaultVatRate = ObjectSpace.FindObject<VatRate>(new BinaryOperator("Name", "Hoog 21% (te vorderen)"));
                expenseAccount.Name = "Reclamekosten";

                expenseAccount = ObjectSpace.CreateObject<ExpenseAccount>();
                expenseAccount.DefaultVatRate = ObjectSpace.FindObject<VatRate>(new BinaryOperator("Name", "Hoog 21% (te vorderen)"));
                expenseAccount.Name = "Reis- en verblijfkosten";

                expenseAccount = ObjectSpace.CreateObject<ExpenseAccount>();
                expenseAccount.DefaultVatRate = ObjectSpace.FindObject<VatRate>(new BinaryOperator("Name", "Hoog 21% (te vorderen)"));
                expenseAccount.Name = "Relatiegeschenken";

                expenseAccount = ObjectSpace.CreateObject<ExpenseAccount>();
                expenseAccount.DefaultVatRate = ObjectSpace.FindObject<VatRate>(new BinaryOperator("Name", "Geen / Vrijgesteld"));
                expenseAccount.Name = "Rentekosten (betaalde debetrente)";

                expenseAccount = ObjectSpace.CreateObject<ExpenseAccount>();
                expenseAccount.DefaultVatRate = ObjectSpace.FindObject<VatRate>(new BinaryOperator("Name", "Hoog 21% (te vorderen)"));
                expenseAccount.Name = "Sponsorkosten";

                expenseAccount = ObjectSpace.CreateObject<ExpenseAccount>();
                expenseAccount.DefaultVatRate = ObjectSpace.FindObject<VatRate>(new BinaryOperator("Name", "Hoog 21% (te vorderen)"));
                expenseAccount.Name = "Studiekosten";

                expenseAccount = ObjectSpace.CreateObject<ExpenseAccount>();
                expenseAccount.DefaultVatRate = ObjectSpace.FindObject<VatRate>(new BinaryOperator("Name", "Hoog 21% (te vorderen)"));
                expenseAccount.Name = "Telefoon, fax en internet";

                expenseAccount = ObjectSpace.CreateObject<ExpenseAccount>();
                expenseAccount.DefaultVatRate = ObjectSpace.FindObject<VatRate>(new BinaryOperator("Name", "Laag 9% (te vorderen)"));
                expenseAccount.Name = "Vakliteratuur";

                expenseAccount = ObjectSpace.CreateObject<ExpenseAccount>();
                expenseAccount.DefaultVatRate = ObjectSpace.FindObject<VatRate>(new BinaryOperator("Name", "Geen / Vrijgesteld"));
                expenseAccount.Name = "Verzekeringen";

                expenseAccount = ObjectSpace.CreateObject<ExpenseAccount>();
                expenseAccount.DefaultVatRate = ObjectSpace.FindObject<VatRate>(new BinaryOperator("Name", "Hoog 21% (te vorderen)"));
                expenseAccount.Name = "Verzendkosten";

                expenseAccount = ObjectSpace.CreateObject<ExpenseAccount>();
                expenseAccount.DefaultVatRate = ObjectSpace.FindObject<VatRate>(new BinaryOperator("Name", "Geen / Vrijgesteld"));
                expenseAccount.Name = "Zakenlunch of -diner (in horeca)";
                expenseAccount.PercentageDeductible = 80;
            }

            if (ObjectSpace.GetObjectsCount(typeof(IncomeAccount), null) == 0)
            {
                var incomeAccount = ObjectSpace.CreateObject<IncomeAccount>();
                incomeAccount.DefaultVatRate = ObjectSpace.FindObject<VatRate>(new BinaryOperator("Name", "Hoog 21% (af te dragen)"));
                incomeAccount.Name = "Omzet";

                incomeAccount = ObjectSpace.CreateObject<IncomeAccount>();
                incomeAccount.DefaultVatRate = ObjectSpace.FindObject<VatRate>(new BinaryOperator("Name", "Hoog 21% (af te dragen)"));
                incomeAccount.Name = "Rentebaten (ontvangen creditrente)";
            }

            ObjectSpace.CommitChanges();
        }
    }
}