using Accounting.Module.BusinessObjects;
using Accounting.Module.BusinessObjects.Parameters;
using Accounting.Module.Controllers;
using Accounting.Module.Model;
using Accounting.Module.Reports;
using Accounting.Module.Reports.Parameters;
using Accounting.Module.Updaters;
using Accounting.Module.Utils;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ReportsV2;
using DevExpress.ExpressApp.Updating;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.BaseImpl;
using System;
using System.Collections.Generic;

namespace Accounting.Module
{
    public sealed partial class AccountingModule : ModuleBase
    {
        public AccountingModule()
        {
            InitializeComponent();

            BaseObject.OidInitializationMode = OidInitializationMode.AfterConstruction;
            DeferredDeletionHelper.DisableDeferredDeletion();
        }

        public override void CustomizeTypesInfo(ITypesInfo typesInfo)
        {
            base.CustomizeTypesInfo(typesInfo);
            CalculatedPersistentAliasHelper.CustomizeTypesInfo(typesInfo);
        }

        public override void ExtendModelInterfaces(ModelInterfaceExtenders extenders)
        {
            base.ExtendModelInterfaces(extenders);
            extenders.Add<IModelAction, IModelFilterObjects>();
        }

        public override IEnumerable<ModuleUpdater> GetModuleUpdaters(IObjectSpace objectSpace, Version versionFromDB)
        {
            var dataUpdater = new DataUpdater(objectSpace, versionFromDB);
            var predefinedReportsUpdater = new PredefinedReportsUpdater(Application, objectSpace, versionFromDB);

            predefinedReportsUpdater.AddPredefinedReport<ProfitAndLossStatementReport>("Winst- en verliesrekening (Ingebouwd)", typeof(JournalEntryLine), typeof(ProfitAndLossStatementParameters));
            predefinedReportsUpdater.AddPredefinedReport<SalesInvoiceReport>("Verkoopfactuur (Ingebouwd)", typeof(SalesInvoice), true);

            return new ModuleUpdater[] { dataUpdater, predefinedReportsUpdater };
        }

        protected override IEnumerable<Type> GetDeclaredControllerTypes()
        {
            return new[]
            {
                typeof(ClosePeriodController),
                typeof(CorrectInvoiceController),
                typeof(CustomizeAccountCreationController),
                typeof(CustomizeWindowCaptionController),
                typeof(FilterJournalEntriesByYearController),
                typeof(FilterPurchaseInvoicesByYearController),
                typeof(FilterSalesInvoicesByYearController),
                typeof(GenerateIdentifierController),
                typeof(NewDepreciationController),
                typeof(NewVatDeclarationController),
                typeof(PayInvoiceController),
                typeof(PayVatDeclarationController),
                typeof(PostDepreciationController),
                typeof(PostInvoiceController),
                typeof(PostVatDeclarationController),
                typeof(RestoreListViewSelectionController),
                typeof(ShowDetailViewController),
                typeof(ShowStartupNavigationItemController)
            };
        }

        protected override IEnumerable<Type> GetDeclaredExportedTypes()
        {
            return new[]
            {
                typeof(AssetAccount),
                typeof(BankAccount),
                typeof(CashAccount),
                typeof(ClosePeriodParameters),
                typeof(Company),
                typeof(CorrectInvoiceParameters),
                typeof(CreditCardAccount),
                typeof(CustomerAccount),
                typeof(Depreciation),
                typeof(DepreciationExpenseAccount),
                typeof(DepreciationLine),
                typeof(EquityAccount),
                typeof(ExpenseAccount),
                typeof(Identifier),
                typeof(IncomeAccount),
                typeof(InputVatAccount),
                typeof(InvoiceLine),
                typeof(LiabilityAccount),
                typeof(NewDepreciationParameters),
                typeof(NewVatDeclarationParameters),
                typeof(OutputVatAccount),
                typeof(PayInvoiceParameters),
                typeof(PaymentTerm),
                typeof(PayVatDeclarationParameters),
                typeof(PrivateAccount),
                typeof(PostDepreciationParameters),
                typeof(ProfitAndLossStatementParameters),
                typeof(PurchaseInvoice),
                typeof(RoundingDifferencesAccount),
                typeof(SalesInvoice),
                typeof(SupplierAccount),
                typeof(JournalEntry),
                typeof(JournalEntryLine),
                typeof(VatDeclaration),
                typeof(VatPaymentAccount),
                typeof(VatRate),
                typeof(VatToPayAccount)
            };
        }

        protected override IEnumerable<Type> GetRegularTypes()
        {
            return Type.EmptyTypes;
        }
    }
}