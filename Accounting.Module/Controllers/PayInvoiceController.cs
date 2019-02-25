using Accounting.Module.BusinessObjects;
using Accounting.Module.BusinessObjects.Parameters;
using Accounting.Module.Extensions;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System;
using System.ComponentModel;

namespace Accounting.Module.Controllers
{
    [DesignerCategory("Code")]
    public class PayInvoiceController : ObjectViewController<ObjectView, Invoice>
    {
        public PayInvoiceController()
        {
            PayInvoiceAction = new PopupWindowShowAction(this, "PayInvoice", PredefinedCategory.RecordEdit);
            PayInvoiceAction.Caption = "Pay";
            PayInvoiceAction.CustomizePopupWindowParams += PayInvoiceAction_CustomizePopupWindowParams;
            PayInvoiceAction.Execute += PayInvoiceAction_Execute;
            PayInvoiceAction.ImageName = "BO_Sale";
            PayInvoiceAction.SelectionDependencyType = SelectionDependencyType.RequireSingleObject;
            PayInvoiceAction.TargetObjectsCriteria = "IsPosted And DueAmount <> 0";

            RegisterActions(PayInvoiceAction);
        }

        public PopupWindowShowAction PayInvoiceAction { get; }

        private string GetJournalEntryDescription()
        {
            switch (ViewCurrentObject.Type)
            {
                case InvoiceType.CreditNote:
                    return string.Format(CaptionHelper.GetLocalizedText("Texts", "PayCreditNote"), ViewCurrentObject.Identifier);

                case InvoiceType.Invoice:
                    return string.Format(CaptionHelper.GetLocalizedText("Texts", $"Pay{ViewCurrentObject.GetType().Name}"), ViewCurrentObject.Identifier);

                default:
                    throw new InvalidOperationException(CaptionHelper.GetLocalizedText(@"Exceptions\UserVisibleExceptions", "UnsupportedInvoiceType"));
            }
        }

        private void PayInvoiceAction_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            var objectSpace = Application.CreateObjectSpace();
            var parameters = new PayInvoiceParameters();
            var detailView = Application.CreateDetailView(objectSpace, parameters);

            parameters.Account = objectSpace.FindObject<BankAccount>(null);
            parameters.Amount = ViewCurrentObject.DueAmount;
            parameters.Description = GetJournalEntryDescription();
            parameters.Invoice = ViewCurrentObject;

            detailView.ViewEditMode = ViewEditMode.Edit;
            e.View = detailView;
        }

        private void PayInvoiceAction_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            var parameters = (PayInvoiceParameters)e.PopupWindowViewCurrentObject;
            Validator.RuleSet.Validate(e.PopupWindowView.ObjectSpace, parameters, DefaultContexts.Save);

            var journalEntry = ObjectSpace.CreateObject<JournalEntry>();
            journalEntry.Date = parameters.Date;
            journalEntry.Description = parameters.Description;
            journalEntry.Item = ViewCurrentObject;
            journalEntry.Type = JournalEntryType.Payment;

            switch (ViewCurrentObject)
            {
                case PurchaseInvoice _:
                    journalEntry.AddLines(ObjectSpace.GetObject(parameters.Account), ObjectSpace.FindObject<SupplierAccount>(null), parameters.Amount);
                    break;

                case SalesInvoice _:
                    journalEntry.AddLines(ObjectSpace.GetObject(parameters.Account), ObjectSpace.FindObject<CustomerAccount>(null), parameters.Amount);
                    break;

                default:
                    throw new InvalidOperationException(CaptionHelper.GetLocalizedText(@"Exceptions\UserVisibleExceptions", "UnsupportedInvoiceClass"));
            }

            if (View is ListView)
            {
                ObjectSpace.CommitChanges();
            }
        }
    }
}