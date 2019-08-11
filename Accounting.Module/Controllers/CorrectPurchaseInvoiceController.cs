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
using System.Linq;

namespace Accounting.Module.Controllers
{
    [DesignerCategory("Code")]
    public class CorrectPurchaseInvoiceController : ObjectViewController<DetailView, PurchaseInvoice>
    {
        public CorrectPurchaseInvoiceController()
        {
            CorrectPurchaseInvoiceAction = new PopupWindowShowAction(this, "CorrectPurchaseInvoice", PredefinedCategory.RecordEdit);
            CorrectPurchaseInvoiceAction.Caption = "Correct";
            CorrectPurchaseInvoiceAction.ConfirmationMessage = "You are about to manually correct this purchase invoice. Do you want to proceed?";
            CorrectPurchaseInvoiceAction.CustomizePopupWindowParams += CorrectPurchaseInvoiceAction_CustomizePopupWindowParams;
            CorrectPurchaseInvoiceAction.Execute += CorrectPurchaseInvoiceAction_Execute;
            CorrectPurchaseInvoiceAction.ImageName = "Action_Clear";
            CorrectPurchaseInvoiceAction.SelectionDependencyType = SelectionDependencyType.RequireSingleObject;
            CorrectPurchaseInvoiceAction.TargetObjectsCriteria = "IsPosted And DueAmount = Total";

            RestorePurchaseInvoiceAction = new SimpleAction(this, "RestorePurchaseInvoice", PredefinedCategory.RecordEdit);
            RestorePurchaseInvoiceAction.Caption = "Restore";
            RestorePurchaseInvoiceAction.Execute += RestorePurchaseInvoiceAction_Execute;
            RestorePurchaseInvoiceAction.ImageName = "Action_Reload";
            RestorePurchaseInvoiceAction.SelectionDependencyType = SelectionDependencyType.RequireSingleObject;
            RestorePurchaseInvoiceAction.TargetObjectsCriteria = "IsCorrected And DueAmount = Total";

            RegisterActions(CorrectPurchaseInvoiceAction, RestorePurchaseInvoiceAction);
        }

        public PopupWindowShowAction CorrectPurchaseInvoiceAction { get; }

        public SimpleAction RestorePurchaseInvoiceAction { get; }

        private void CorrectPurchaseInvoiceAction_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            var objectSpace = Application.CreateObjectSpace();
            var parameters = new CorrectPurchaseInvoiceParameters();
            var detailView = Application.CreateDetailView(objectSpace, parameters, true);

            parameters.Description = GetJournalEntryDescription();

            detailView.ViewEditMode = ViewEditMode.Edit;
            e.View = detailView;
        }

        private void CorrectPurchaseInvoiceAction_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            var parameters = (CorrectPurchaseInvoiceParameters)e.PopupWindowViewCurrentObject;
            Validator.RuleSet.Validate(e.PopupWindowView.ObjectSpace, parameters, DefaultContexts.Save);

            var journalEntry = ObjectSpace.CreateObject<JournalEntry>();
            journalEntry.Date = ViewCurrentObject.Date;
            journalEntry.Description = parameters.Description;
            journalEntry.Item = ViewCurrentObject;
            journalEntry.Type = JournalEntryType.Correction;

            var supplierAccount = ObjectSpace.FindObject<SupplierAccount>(null);

            if (parameters.Account != null && parameters.Amount != 0)
            {
                journalEntry.AddLines(ObjectSpace.GetObject(parameters.Account), supplierAccount, -parameters.Amount);
            }

            if (parameters.VatRate != null && parameters.Vat != 0)
            {
                if (parameters.VatRate.PayableAccount != null)
                {
                    journalEntry.AddLines(supplierAccount, ObjectSpace.GetObject(parameters.VatRate.PayableAccount), -parameters.Vat);
                }

                if (parameters.VatRate.ReceivableAccount != null)
                {
                    journalEntry.AddLines(supplierAccount, ObjectSpace.GetObject(parameters.VatRate.ReceivableAccount), -parameters.Vat);
                }
            }

            if (journalEntry.Lines.Count > 1)
            {
                ViewCurrentObject.SubTotal += parameters.Amount;
                ViewCurrentObject.Vat += parameters.Vat;
            }
            else
            {
                ObjectSpace.Delete(journalEntry);
            }
        }

        private string GetJournalEntryDescription()
        {
            switch (ViewCurrentObject.Type)
            {
                case InvoiceType.CreditNote:
                    return string.Format(CaptionHelper.GetLocalizedText("Texts", "CorrectCreditNote"), ViewCurrentObject.Identifier);

                case InvoiceType.Invoice:
                    return string.Format(CaptionHelper.GetLocalizedText("Texts", $"Correct{ViewCurrentObject.GetType().Name}"), ViewCurrentObject.Identifier);

                default:
                    throw new InvalidOperationException(CaptionHelper.GetLocalizedText(@"Exceptions\UserVisibleExceptions", "UnsupportedInvoiceType"));
            }
        }

        private void RestorePurchaseInvoiceAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            ObjectSpace.Delete(ViewCurrentObject.JournalEntries.Where(x => x.Type == JournalEntryType.Correction).ToList());
        }
    }
}