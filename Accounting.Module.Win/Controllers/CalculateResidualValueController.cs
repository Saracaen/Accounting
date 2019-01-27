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
    public class CalculateResidualValueController : ObjectViewController<ListView, DepreciationLine>
    {
        public CalculateResidualValueController()
        {
            TargetViewId = "Depreciation_Lines_ListView";
        }

        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();

            if (View.Editor is GridListEditor gridListEditor)
            {
                var residualValueColumn = gridListEditor.GridView.Columns.AddField("ResidualValue");
                residualValueColumn.Caption = CaptionHelper.GetMemberCaption(typeof(Depreciation), "ResidualValue");
                residualValueColumn.DisplayFormat.FormatType = FormatType.Numeric;
                residualValueColumn.DisplayFormat.FormatString = "{0:C}";
                residualValueColumn.OptionsColumn.AllowEdit = false;
                residualValueColumn.UnboundType = UnboundColumnType.Decimal;
                residualValueColumn.Visible = true;

                gridListEditor.GridView.CustomUnboundColumnData += GridView_CustomUnboundColumnData;
                gridListEditor.GridView.OptionsCustomization.AllowSort = false;
                gridListEditor.GridView.PopupMenuShowing += GridView_PopupMenuShowing;
            }
        }

        private void GridView_CustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
        {
            if (sender is GridView gridView && e.Column.FieldName == "ResidualValue" && e.IsGetData)
            {
                var handle = gridView.GetRowHandle(e.ListSourceRowIndex);
                var depreciation = (Depreciation)gridView.GetRowCellValue(handle, "Depreciation");

                if (depreciation != null)
                {
                    var total = depreciation.Value;
                    for (var i = 0; i <= handle; i++)
                    {
                        total -= Convert.ToDecimal(gridView.GetRowCellValue(i, "Amount"));
                    }

                    e.Value = total;
                }
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