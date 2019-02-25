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
using System.Linq;

namespace Accounting.Module.Controllers
{
    [DesignerCategory("Code")]
    public class CorrectInvoiceController : ObjectViewController<DetailView, Invoice>
    {
        public CorrectInvoiceController()
        {
            CorrectInvoiceAction = new PopupWindowShowAction(this, "CorrectInvoice", PredefinedCategory.RecordEdit);
            CorrectInvoiceAction.Caption = "Correct";
            CorrectInvoiceAction.ConfirmationMessage = "You are about to manually correct this invoice. Do you want to proceed?";
            CorrectInvoiceAction.CustomizePopupWindowParams += CorrectInvoiceAction_CustomizePopupWindowParams;
            CorrectInvoiceAction.Execute += CorrectInvoiceAction_Execute;
            CorrectInvoiceAction.ImageName = "Action_Clear";
            CorrectInvoiceAction.SelectionDependencyType = SelectionDependencyType.RequireSingleObject;
            CorrectInvoiceAction.TargetObjectsCriteria = "IsPosted And DueAmount = Total";

            RestoreInvoiceAction = new SimpleAction(this, "RestoreInvoice", PredefinedCategory.RecordEdit);
            RestoreInvoiceAction.Caption = "Restore";
            RestoreInvoiceAction.Execute += RestoreInvoiceAction_Execute;
            RestoreInvoiceAction.ImageName = "Action_Reload";
            RestoreInvoiceAction.SelectionDependencyType = SelectionDependencyType.RequireSingleObject;
            RestoreInvoiceAction.TargetObjectsCriteria = "IsCorrected And DueAmount = Total";

            RegisterActions(CorrectInvoiceAction, RestoreInvoiceAction);
        }

        public PopupWindowShowAction CorrectInvoiceAction { get; }

        public SimpleAction RestoreInvoiceAction { get; }

        private void CorrectInvoice(JournalEntry journalEntry, CorrectInvoiceParameters parameters, Account account)
        {
            if (parameters.Account != null && parameters.Amount != 0)
            {
                journalEntry.AddLines(ObjectSpace.GetObject(parameters.Account), account, -parameters.Amount);
            }

            if (parameters.VatRate != null && parameters.Vat != 0)
            {
                if (parameters.VatRate.PayableAccount != null)
                {
                    journalEntry.AddLines(account, ObjectSpace.GetObject(parameters.VatRate.PayableAccount), -parameters.Vat);
                }

                if (parameters.VatRate.ReceivableAccount != null)
                {
                    journalEntry.AddLines(account, ObjectSpace.GetObject(parameters.VatRate.ReceivableAccount), -parameters.Vat);
                }
            }
        }

        private void CorrectInvoiceAction_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            var objectSpace = Application.CreateObjectSpace();
            var parameters = new CorrectInvoiceParameters();
            var detailView = Application.CreateDetailView(objectSpace, parameters);

            parameters.Description = GetJournalEntryDescription();
            parameters.Invoice = ViewCurrentObject;

            detailView.ViewEditMode = ViewEditMode.Edit;
            e.View = detailView;
        }

        private void CorrectInvoiceAction_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            var parameters = (CorrectInvoiceParameters)e.PopupWindowViewCurrentObject;
            Validator.RuleSet.Validate(e.PopupWindowView.ObjectSpace, parameters, DefaultContexts.Save);

            var journalEntry = ObjectSpace.CreateObject<JournalEntry>();
            journalEntry.Date = ViewCurrentObject.Date;
            journalEntry.Description = parameters.Description;
            journalEntry.Item = ViewCurrentObject;
            journalEntry.Type = JournalEntryType.Correction;

            switch (ViewCurrentObject)
            {
                case PurchaseInvoice _:
                    CorrectInvoice(journalEntry, parameters, ObjectSpace.FindObject<SupplierAccount>(null));
                    break;

                case SalesInvoice _:
                    CorrectInvoice(journalEntry, parameters, ObjectSpace.FindObject<CustomerAccount>(null));
                    break;

                default:
                    throw new InvalidOperationException(CaptionHelper.GetLocalizedText(@"Exceptions\UserVisibleExceptions", "UnsupportedInvoiceClass"));
            }

            if (journalEntry.Lines.Count > 1)
            {
                ViewCurrentObject.IsCorrected = true;
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

        private void RestoreInvoiceAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            ObjectSpace.Delete(ViewCurrentObject.JournalEntries.Where(x => x.Type == JournalEntryType.Correction).ToList());
            ViewCurrentObject.IsCorrected = false;
        }
    }
}