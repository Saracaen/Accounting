using Accounting.Module.BusinessObjects;
using DevExpress.Data;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Utils;
using DevExpress.ExpressApp.Win.Editors;
using DevExpress.Utils;
using DevExpress.XtraGrid.Localization;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.ComponentModel;

namespace Accounting.Module.Win.Controllers
{
    [DesignerCategory("Code")]
    public class CalculateRunningBalanceController : ObjectViewController<ListView, JournalEntryLine>
    {
        public CalculateRunningBalanceController()
        {
            TargetViewId = "Account_JournalEntryLines_ListView";
        }

        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();

            if (View.Editor is GridListEditor gridListEditor)
            {
                var balanceColumn = gridListEditor.GridView.Columns.AddField("RunningBalance");
                balanceColumn.Caption = CaptionHelper.GetMemberCaption(typeof(Account), "Balance");
                balanceColumn.DisplayFormat.FormatType = FormatType.Numeric;
                balanceColumn.DisplayFormat.FormatString = "{0:C}";
                balanceColumn.OptionsColumn.AllowEdit = false;
                balanceColumn.UnboundType = UnboundColumnType.Decimal;
                balanceColumn.Visible = true;
                balanceColumn.Width = 50;

                gridListEditor.GridView.CustomUnboundColumnData += GridView_CustomUnboundColumnData;
                gridListEditor.GridView.OptionsCustomization.AllowSort = false;
                gridListEditor.GridView.PopupMenuShowing += GridView_PopupMenuShowing;
            }
        }

        private void GridView_CustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
        {
            if (sender is GridView gridView && e.Column.FieldName == "RunningBalance" && e.IsGetData)
            {
                var handle = gridView.GetRowHandle(e.ListSourceRowIndex);
                var account = (Account)gridView.GetRowCellValue(handle, "Account");
                var total = 0m;

                if (account != null)
                {
                    for (var i = 0; i <= handle; i++)
                    {
                        var debit = Convert.ToDecimal(gridView.GetRowCellValue(i, "Debit"));
                        var credit = Convert.ToDecimal(gridView.GetRowCellValue(i, "Credit"));

                        switch (account.Type)
                        {
                            case AccountType.Credit:
                                total += credit - debit;
                                break;

                            case AccountType.Debit:
                                total += debit - credit;
                                break;

                            default:
                                throw new InvalidOperationException(CaptionHelper.GetLocalizedText(@"Exceptions\UserVisibleExceptions", "UnsupportedAccountType"));
                        }
                    }
                }

                e.Value = total;
            }
        }

        private void GridView_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            if (e.MenuType == GridMenuType.Column)
            {
                e.Menu.Items[GridLocalizer.Active.GetLocalizedString(GridStringId.MenuColumnClearAllSorting)].Enabled = false;
            }
        }
    }
}