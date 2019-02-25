using Accounting.Module.BusinessObjects;
using Accounting.Module.Extensions;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using System;
using System.ComponentModel;
using System.Linq;

namespace Accounting.Module.Controllers
{
    [DesignerCategory("Code")]
    public class PostInvoiceController : ObjectViewController<ObjectView, Invoice>
    {
        public PostInvoiceController()
        {
            PostInvoiceAction = new SimpleAction(this, "PostInvoice", PredefinedCategory.RecordEdit);
            PostInvoiceAction.Caption = "Post";
            PostInvoiceAction.Execute += PostInvoiceAction_Execute;
            PostInvoiceAction.ImageName = "Action_LinkUnlink_Link";
            PostInvoiceAction.SelectionDependencyType = SelectionDependencyType.RequireSingleObject;
            PostInvoiceAction.TargetObjectsCriteria = "Not IsPosted And Not IsNullOrEmpty(Trim(Identifier))";

            UnpostInvoiceAction = new SimpleAction(this, "UnpostInvoice", PredefinedCategory.RecordEdit);
            UnpostInvoiceAction.Caption = "Unpost";
            UnpostInvoiceAction.ConfirmationMessage = "You are about to unpost the selected invoice(s). Do you want to proceed?";
            UnpostInvoiceAction.Execute += UnpostInvoiceAction_Execute;
            UnpostInvoiceAction.ImageName = "Action_LinkUnlink_Unlink";
            UnpostInvoiceAction.SelectionDependencyType = SelectionDependencyType.RequireSingleObject;
            UnpostInvoiceAction.TargetObjectsCriteria = "IsPosted";

            RegisterActions(PostInvoiceAction, UnpostInvoiceAction);
        }

        public SimpleAction PostInvoiceAction { get; set; }

        public SimpleAction UnpostInvoiceAction { get; set; }

        private void PostInvoice(JournalEntry journalEntry, Account account)
        {
            foreach (var invoiceLineGroup in ViewCurrentObject.Lines.Where(x => x.Account != null).GroupBy(x => x.Account))
            {
                if (invoiceLineGroup.Key is ExpenseAccount expenseAccount && expenseAccount.PercentageDeductible < 100)
                {
                    var percentageDeductible = (decimal)expenseAccount.PercentageDeductible / 100;

                    journalEntry.AddLines(expenseAccount, account, percentageDeductible * -invoiceLineGroup.Sum(x => x.SubTotal));
                    journalEntry.AddLines(ObjectSpace.FindObject<PrivateAccount>(null), account, (1 - percentageDeductible) * -invoiceLineGroup.Sum(x => x.SubTotal));
                }
                else
                {
                    journalEntry.AddLines(invoiceLineGroup.Key, account, -invoiceLineGroup.Sum(x => x.SubTotal));
                }
            }

            var subTotal = journalEntry.Lines.Where(x => !Equals(x.Account, account)).Sum(x => x.Amount);
            if (Math.Sign(ViewCurrentObject.SubTotal) * Math.Abs(subTotal) != ViewCurrentObject.SubTotal)
            {
                journalEntry.AddLines(ObjectSpace.FindObject<RoundingDifferencesAccount>(null), account, ViewCurrentObject.SubTotal - subTotal);
            }

            foreach (var invoiceLineGroup in ViewCurrentObject.Lines.Where(x => x.VatRate != null).GroupBy(x => x.VatRate))
            {
                var vatRate = (decimal)invoiceLineGroup.Key.Rate / 100;

                if (invoiceLineGroup.Key.PayableAccount != null)
                {
                    journalEntry.AddLines(account, invoiceLineGroup.Key.PayableAccount, -invoiceLineGroup.Sum(x => x.SubTotal * vatRate));
                }

                if (invoiceLineGroup.Key.ReceivableAccount != null)
                {
                    journalEntry.AddLines(account, invoiceLineGroup.Key.ReceivableAccount, -invoiceLineGroup.Sum(x => x.SubTotal * vatRate));
                }
            }
        }

        private void PostInvoiceAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var journalEntry = ObjectSpace.CreateObject<JournalEntry>();
            journalEntry.Date = ViewCurrentObject.Date;
            journalEntry.Item = ViewCurrentObject;
            journalEntry.Type = JournalEntryType.Posting;

            switch (ViewCurrentObject)
            {
                case PurchaseInvoice purchaseInvoice:
                    switch (purchaseInvoice.Type)
                    {
                        case InvoiceType.Invoice:
                            journalEntry.Description = string.Format(CaptionHelper.GetLocalizedText("Texts", "PostPurchaseInvoice"), purchaseInvoice.Identifier);
                            break;

                        case InvoiceType.CreditNote:
                            journalEntry.Description = string.Format(CaptionHelper.GetLocalizedText("Texts", "PostCreditNote"), purchaseInvoice.Identifier);
                            break;

                        default:
                            throw new InvalidOperationException(CaptionHelper.GetLocalizedText(@"Exceptions\UserVisibleExceptions", "UnsupportedInvoiceType"));
                    }

                    PostInvoice(journalEntry, ObjectSpace.FindObject<SupplierAccount>(null));
                    break;

                case SalesInvoice salesInvoice:
                    switch (salesInvoice.Type)
                    {
                        case InvoiceType.Invoice:
                            journalEntry.Description = string.Format(CaptionHelper.GetLocalizedText("Texts", "PostSalesInvoice"), salesInvoice.Identifier);
                            break;

                        case InvoiceType.CreditNote:
                            journalEntry.Description = string.Format(CaptionHelper.GetLocalizedText("Texts", "PostCreditNote"), salesInvoice.Identifier);
                            break;

                        default:
                            throw new InvalidOperationException(CaptionHelper.GetLocalizedText(@"Exceptions\UserVisibleExceptions", "UnsupportedInvoiceType"));
                    }

                    PostInvoice(journalEntry, ObjectSpace.FindObject<CustomerAccount>(null));
                    break;

                default:
                    throw new InvalidOperationException(CaptionHelper.GetLocalizedText(@"Exceptions\UserVisibleExceptions", "UnsupportedInvoiceClass"));
            }

            if (View is ListView)
            {
                ObjectSpace.CommitChanges();
            }
        }

        private void UnpostInvoiceAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            ObjectSpace.Delete(ViewCurrentObject.JournalEntries);
            ViewCurrentObject.IsCorrected = false;

            if (View is ListView)
            {
                ObjectSpace.CommitChanges();
            }
        }
    }
}