using Accounting.Module.BusinessObjects;
using Accounting.Module.BusinessObjects.Parameters;
using Accounting.Module.Utils;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Accounting.Module.Controllers
{
    [DesignerCategory("Code")]
    public class NewVatDeclarationController : ObjectViewController<ObjectView, VatDeclaration>
    {
        public NewVatDeclarationController()
        {
            NewVatDeclarationAction = new PopupWindowShowAction(this, "NewVatDeclaration", PredefinedCategory.ObjectsCreation);
            NewVatDeclarationAction.Caption = "New";
            NewVatDeclarationAction.CustomizePopupWindowParams += NewVatDeclarationAction_CustomizePopupWindowParams;
            NewVatDeclarationAction.Execute += NewVatDeclarationAction_Execute;
            NewVatDeclarationAction.ImageName = "Action_New";
            NewVatDeclarationAction.Shortcut = "CtrlN";

            SaveAndNewVatDeclarationAction = new PopupWindowShowAction(this, "SaveAndNewVatDeclaration", PredefinedCategory.Save);
            SaveAndNewVatDeclarationAction.Caption = "Save and New";
            SaveAndNewVatDeclarationAction.CustomizePopupWindowParams += NewVatDeclarationAction_CustomizePopupWindowParams;
            SaveAndNewVatDeclarationAction.Execute += NewVatDeclarationAction_Execute;
            SaveAndNewVatDeclarationAction.Executing += SaveAndNewVatDeclarationAction_Executing;
            SaveAndNewVatDeclarationAction.ImageName = "Action_Save_New";
            SaveAndNewVatDeclarationAction.TargetViewType = ViewType.DetailView;

            RegisterActions(NewVatDeclarationAction, SaveAndNewVatDeclarationAction);
        }

        public PopupWindowShowAction NewVatDeclarationAction { get; }

        public PopupWindowShowAction SaveAndNewVatDeclarationAction { get; }

        protected override void OnActivated()
        {
            base.OnActivated();

            var newObjectViewController = Frame.GetController<NewObjectViewController>();
            if (newObjectViewController != null)
            {
                newObjectViewController.Active[Name] = false;
            }

            NewVatDeclarationAction.Active[Name] = View.AllowNew;
            SaveAndNewVatDeclarationAction.Active[Name] = View.AllowNew;
        }

        protected override void OnDeactivated()
        {
            base.OnDeactivated();
            Frame.GetController<NewObjectViewController>()?.Active.RemoveItem(Name);
        }

        private void NewVatDeclaration(VatDeclaration vatDeclaration, Account account, CriteriaOperator criteria, VatCategory vatCategory)
        {
            account.JournalEntryLines.Criteria = criteria;

            var vatDeclarationLine = vatDeclaration.Lines.First(x => x.Category == vatCategory);
            foreach (var journalEntryLine in account.JournalEntryLines)
            {
                switch (vatCategory)
                {
                    case VatCategory.None:
                        break;

                    case VatCategory.DeliveriesOrServicesTaxedAtHighRate:
                    case VatCategory.DeliveriesOrServicesTaxedAtLowRate:
                    case VatCategory.DeliveriesOrServicesTaxedAtOtherRates:
                    case VatCategory.PrivateUse:
                        foreach (var incomeAccountJournalEntryLine in journalEntryLine.JournalEntry.Lines.Where(x => x.Account is IncomeAccount))
                        {
                            vatDeclarationLine.Amount -= incomeAccountJournalEntryLine.Amount;
                        }
                        vatDeclarationLine.Vat -= journalEntryLine.Amount;
                        break;

                    case VatCategory.DeliveriesOrServicesUntaxed:
                    case VatCategory.DeliveriesToCountriesOutsideTheEuropeanUnion:
                    case VatCategory.DeliveriesOrServicesToCountriesWithinTheEuropeanUnion:
                    case VatCategory.InstallationOrDistanceSalesWithinTheEuropeanUnion:
                        foreach (var incomeAccountJournalEntryLine in journalEntryLine.JournalEntry.Lines.Where(x => x.Account is IncomeAccount))
                        {
                            vatDeclarationLine.Amount -= incomeAccountJournalEntryLine.Amount;
                        }
                        break;

                    case VatCategory.DeliveriesOrServicesReverseCharged:
                    case VatCategory.DeliveriesOrServicesFromCountriesOutsideTheEuropeanUnion:
                    case VatCategory.DeliveriesOrServicesFromCountriesWithinTheEuropeanUnion:
                        foreach (var expenseAccountJournalEntryLine in journalEntryLine.JournalEntry.Lines.Where(x => x.Account is AssetAccount || x.Account is ExpenseAccount))
                        {
                            vatDeclarationLine.Amount += expenseAccountJournalEntryLine.Amount;
                        }
                        vatDeclarationLine.Vat -= journalEntryLine.Amount;
                        break;

                    case VatCategory.InputVat:
                    case VatCategory.SmallBusinessScheme:
                        vatDeclarationLine.Vat += journalEntryLine.Amount;
                        break;

                    default:
                        throw new InvalidOperationException(CaptionHelper.GetLocalizedText(@"Exceptions\UserVisibleExceptions", "UnsupportedVatCategory"));
                }
            }
        }

        private void NewVatDeclarationAction_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            var company = ObjectSpace.FindObject<Company>(null);
            var journalEntryQuery = ObjectSpace.GetObjectsQuery<JournalEntry>();
            var journalEntries = journalEntryQuery.Where(x => x.Type == JournalEntryType.Entry || x.Type == JournalEntryType.Payment || x.Type == JournalEntryType.Posting);
            var listView = Application.CreateListView(typeof(NewVatDeclarationParameters), true);

            foreach (var year in journalEntries.GroupBy(x => x.Date.Year).Select(x => x.First().Date.Year))
            {
                var vatDeclarations = ObjectSpace.GetObjects<VatDeclaration>(new BinaryOperator("Year", year));
                var existingPeriods = vatDeclarations.Select(x => x.Period);
                var type = vatDeclarations.Count > 0 ? vatDeclarations[0].Type : company.VatDeclarationType;

                foreach (var period in VatDeclarationHelper.GetPeriods(type).Except(existingPeriods))
                {
                    if (year == DateTime.Now.Year && DateTime.Now <= VatDeclarationHelper.GetLastDayOfPeriod(year, period))
                        continue;

                    if (ViewCurrentObject != null && ViewCurrentObject.Year == year && ViewCurrentObject.Period == period)
                        continue;

                    listView.CollectionSource.Add(new NewVatDeclarationParameters { Year = year, Period = period, Type = type });
                }
            }

            e.View = listView;
        }

        private void NewVatDeclarationAction_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            if (e.PopupWindowViewCurrentObject is NewVatDeclarationParameters parameters)
            {
                var accounts = new Dictionary<Account, VatCategory>();
                var objectSpace = Application.CreateObjectSpace();
                var vatDeclaration = objectSpace.CreateObject<VatDeclaration>();
                var detailView = Application.CreateDetailView(objectSpace, vatDeclaration, View);
                var vatRates = ObjectSpace.GetObjects<VatRate>();

                vatDeclaration.Year = objectSpace.GetObject(parameters.Year);
                vatDeclaration.Period = parameters.Period;
                vatDeclaration.Type = parameters.Type;

                detailView.ViewEditMode = ViewEditMode.Edit;

                foreach (var vatRate in vatRates)
                {
                    if (vatRate.PayableAccount != null)
                    {
                        accounts[vatRate.PayableAccount] = vatRate.PayableCategory;
                    }

                    if (vatRate.ReceivableAccount != null)
                    {
                        accounts[vatRate.ReceivableAccount] = vatRate.ReceivableCategory;
                    }
                }

                var firstDayOfPeriod = VatDeclarationHelper.GetFirstDayOfPeriod(parameters.Year, parameters.Period);
                var lastDayOfPeriod = VatDeclarationHelper.GetLastDayOfPeriod(parameters.Year, parameters.Period);
                var criteria = CriteriaOperator.Parse("JournalEntry.Date >= ? And JournalEntry.Date <= ?", firstDayOfPeriod, lastDayOfPeriod);

                foreach (var account in accounts)
                {
                    NewVatDeclaration(vatDeclaration, account.Key, criteria, account.Value);
                }

                foreach (var vatDeclarationLine in vatDeclaration.Lines)
                {
                    switch (vatDeclarationLine.Category)
                    {
                        case VatCategory.DeliveriesOrServicesReverseCharged:
                        case VatCategory.DeliveriesOrServicesFromCountriesOutsideTheEuropeanUnion:
                        case VatCategory.DeliveriesOrServicesFromCountriesWithinTheEuropeanUnion:
                        case VatCategory.InputVat:
                        case VatCategory.SmallBusinessScheme:
                            vatDeclarationLine.Amount = Math.Ceiling(vatDeclarationLine.Amount);
                            vatDeclarationLine.Vat = Math.Ceiling(vatDeclarationLine.Vat);
                            break;

                        default:
                            vatDeclarationLine.Amount = Math.Floor(vatDeclarationLine.Amount);
                            vatDeclarationLine.Vat = Math.Floor(vatDeclarationLine.Vat);
                            break;
                    }
                }

                Application.ShowViewStrategy.ShowView(new ShowViewParameters(detailView), new ShowViewSource(Frame, NewVatDeclarationAction));
            }
        }

        private void SaveAndNewVatDeclarationAction_Executing(object sender, CancelEventArgs e)
        {
            ObjectSpace.CommitChanges();
        }
    }
}