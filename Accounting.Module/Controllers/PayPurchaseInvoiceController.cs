using Accounting.Module.BusinessObjects;
using Accounting.Module.Controllers.Parameters;
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
    public class PayPurchaseInvoiceController : ObjectViewController<ObjectView, PurchaseInvoice>
    {
        public PayPurchaseInvoiceController()
        {
            PayPurchaseInvoiceAction = new PopupWindowShowAction(this, "PayPurchaseInvoice", PredefinedCategory.RecordEdit);
            PayPurchaseInvoiceAction.Caption = "Pay";
            PayPurchaseInvoiceAction.CustomizePopupWindowParams += PayPurchaseInvoiceAction_CustomizePopupWindowParams;
            PayPurchaseInvoiceAction.Execute += PayPurchaseInvoiceAction_Execute;
            PayPurchaseInvoiceAction.ImageName = "BO_Sale";
            PayPurchaseInvoiceAction.SelectionDependencyType = SelectionDependencyType.RequireMultipleObjects;
            PayPurchaseInvoiceAction.TargetObjectsCriteria = "IsPosted And DueAmount <> 0";

            RegisterActions(PayPurchaseInvoiceAction);
        }

        public PopupWindowShowAction PayPurchaseInvoiceAction { get; }

        private string GetJournalEntryDescription(Invoice invoice)
        {
            switch (ViewCurrentObject.Type)
            {
                case InvoiceType.CreditNote:
                    return string.Format(CaptionHelper.GetLocalizedText("Texts", "PayCreditNote"), invoice.Identifier);

                case InvoiceType.Invoice:
                    return string.Format(CaptionHelper.GetLocalizedText("Texts", $"Pay{invoice.GetType().Name}"), invoice.Identifier);

                default:
                    throw new InvalidOperationException(CaptionHelper.GetLocalizedText(@"Exceptions\UserVisibleExceptions", "UnsupportedInvoiceType"));
            }
        }

        private void PayPurchaseInvoiceAction_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            var objectSpace = Application.CreateObjectSpace();
            var parameters = new PayPurchaseInvoiceParameters();
            var detailView = Application.CreateDetailView(objectSpace, parameters);

            parameters.Account = objectSpace.FindObject<BankAccount>(null);

            if (ViewSelectedObjects.Count == 1)
            {
                parameters.Amount = ViewCurrentObject.DueAmount;
                parameters.Description = GetJournalEntryDescription(ViewCurrentObject);
                parameters.Invoice = ViewCurrentObject;
            }

            detailView.ViewEditMode = ViewEditMode.Edit;
            e.View = detailView;
        }

        private void PayPurchaseInvoiceAction_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            var parameters = (PayPurchaseInvoiceParameters)e.PopupWindowViewCurrentObject;
            Validator.RuleSet.Validate(e.PopupWindowView.ObjectSpace, parameters, DefaultContexts.Save);

            foreach (var invoice in ViewSelectedObjects)
            {
                var journalEntry = ObjectSpace.CreateObject<JournalEntry>();
                journalEntry.Date = parameters.Date;
                journalEntry.Description = parameters.Description ?? GetJournalEntryDescription(invoice);
                journalEntry.Item = invoice;
                journalEntry.Type = JournalEntryType.Payment;

                journalEntry.AddLines(ObjectSpace.GetObject(parameters.Account), ObjectSpace.FindObject<SupplierAccount>(null), parameters.Invoice != null ? parameters.Amount : invoice.DueAmount);
            }

            if (View is ListView)
            {
                ObjectSpace.CommitChanges();
            }
        }
    }
}