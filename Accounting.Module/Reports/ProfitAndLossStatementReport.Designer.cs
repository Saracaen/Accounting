namespace Accounting.Module.Reports
{
    partial class ProfitAndLossStatementReport
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary2 = new DevExpress.XtraReports.UI.XRSummary();
            this.topMarginBand1 = new DevExpress.XtraReports.UI.TopMarginBand();
            this.detailBand1 = new DevExpress.XtraReports.UI.DetailBand();
            this.table2 = new DevExpress.XtraReports.UI.XRTable();
            this.tableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.tableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.tableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.bottomMarginBand1 = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.table1 = new DevExpress.XtraReports.UI.XRTable();
            this.tableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.tableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.tableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.label3 = new DevExpress.XtraReports.UI.XRLabel();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.table4 = new DevExpress.XtraReports.UI.XRTable();
            this.tableRow4 = new DevExpress.XtraReports.UI.XRTableRow();
            this.tableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
            this.tableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
            this.GroupFooter1 = new DevExpress.XtraReports.UI.GroupFooterBand();
            this.table3 = new DevExpress.XtraReports.UI.XRTable();
            this.tableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.tableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
            this.tableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.label2 = new DevExpress.XtraReports.UI.XRLabel();
            this.GroupHeader2 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.collectionDataSource1 = new DevExpress.Persistent.Base.ReportsV2.CollectionDataSource();
            ((System.ComponentModel.ISupportInitialize)(this.table2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.table1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.table4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.table3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.collectionDataSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // topMarginBand1
            // 
            this.topMarginBand1.HeightF = 75F;
            this.topMarginBand1.Name = "topMarginBand1";
            // 
            // detailBand1
            // 
            this.detailBand1.HeightF = 0F;
            this.detailBand1.Name = "detailBand1";
            // 
            // table2
            // 
            this.table2.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.table2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.table2.Name = "table2";
            this.table2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 5, 0, 100F);
            this.table2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.tableRow2});
            this.table2.SizeF = new System.Drawing.SizeF(667F, 35F);
            this.table2.StylePriority.UseFont = false;
            this.table2.StylePriority.UsePadding = false;
            // 
            // tableRow2
            // 
            this.tableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.tableCell3,
            this.tableCell4});
            this.tableRow2.Name = "tableRow2";
            this.tableRow2.Weight = 1D;
            // 
            // tableCell3
            // 
            this.tableCell3.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Account].[Name]")});
            this.tableCell3.Name = "tableCell3";
            this.tableCell3.StylePriority.UsePadding = false;
            this.tableCell3.Text = "tableCell3";
            this.tableCell3.Weight = 1.0523076923076924D;
            // 
            // tableCell4
            // 
            this.tableCell4.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "Iif([Account].[Type] = \'Credit\', -sumSum([Amount]), sumSum([Amount]))")});
            this.tableCell4.Name = "tableCell4";
            this.tableCell4.StylePriority.UsePadding = false;
            this.tableCell4.StylePriority.UseTextAlignment = false;
            xrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.tableCell4.Summary = xrSummary1;
            this.tableCell4.Text = "tableCell4";
            this.tableCell4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.tableCell4.TextFormatString = "{0:c}";
            this.tableCell4.Weight = 1D;
            // 
            // bottomMarginBand1
            // 
            this.bottomMarginBand1.HeightF = 40F;
            this.bottomMarginBand1.Name = "bottomMarginBand1";
            // 
            // GroupHeader1
            // 
            this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.table1,
            this.label3});
            this.GroupHeader1.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("Account.Category", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
            this.GroupHeader1.HeightF = 91F;
            this.GroupHeader1.Level = 1;
            this.GroupHeader1.Name = "GroupHeader1";
            // 
            // table1
            // 
            this.table1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(201)))), ((int)(((byte)(194)))));
            this.table1.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.table1.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.table1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(86)))), ((int)(((byte)(85)))));
            this.table1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 50F);
            this.table1.Name = "table1";
            this.table1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 5, 0, 100F);
            this.table1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.tableRow1});
            this.table1.SizeF = new System.Drawing.SizeF(667F, 32F);
            this.table1.StylePriority.UseBorderColor = false;
            this.table1.StylePriority.UseBorders = false;
            this.table1.StylePriority.UseFont = false;
            this.table1.StylePriority.UseForeColor = false;
            this.table1.StylePriority.UsePadding = false;
            // 
            // tableRow1
            // 
            this.tableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.tableCell1,
            this.tableCell2});
            this.tableRow1.Name = "tableRow1";
            this.tableRow1.Weight = 1.28D;
            // 
            // tableCell1
            // 
            this.tableCell1.Name = "tableCell1";
            this.tableCell1.StylePriority.UsePadding = false;
            this.tableCell1.Text = "NAAM";
            this.tableCell1.Weight = 1.707369230064979D;
            // 
            // tableCell2
            // 
            this.tableCell2.Name = "tableCell2";
            this.tableCell2.StylePriority.UsePadding = false;
            this.tableCell2.StylePriority.UseTextAlignment = false;
            this.tableCell2.Text = "BALANS";
            this.tableCell2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.tableCell2.Weight = 1.6225000007042518D;
            // 
            // label3
            // 
            this.label3.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Account].[Category]")});
            this.label3.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(86)))), ((int)(((byte)(85)))));
            this.label3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.label3.Name = "label3";
            this.label3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.label3.SizeF = new System.Drawing.SizeF(450F, 32F);
            this.label3.StylePriority.UseBorderColor = false;
            this.label3.StylePriority.UseFont = false;
            this.label3.StylePriority.UseForeColor = false;
            this.label3.StylePriority.UsePadding = false;
            this.label3.Text = "label3";
            // 
            // ReportFooter
            // 
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.table4});
            this.ReportFooter.HeightF = 51F;
            this.ReportFooter.Name = "ReportFooter";
            this.ReportFooter.PrintAtBottom = true;
            // 
            // table4
            // 
            this.table4.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(201)))), ((int)(((byte)(194)))));
            this.table4.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.table4.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.table4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(86)))), ((int)(((byte)(85)))));
            this.table4.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.table4.Name = "table4";
            this.table4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 5, 0, 100F);
            this.table4.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.tableRow4});
            this.table4.SizeF = new System.Drawing.SizeF(667F, 42F);
            this.table4.StylePriority.UseBorderColor = false;
            this.table4.StylePriority.UseBorders = false;
            this.table4.StylePriority.UseFont = false;
            this.table4.StylePriority.UseForeColor = false;
            this.table4.StylePriority.UsePadding = false;
            this.table4.StylePriority.UseTextAlignment = false;
            this.table4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // tableRow4
            // 
            this.tableRow4.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.tableCell7,
            this.tableCell8});
            this.tableRow4.Name = "tableRow4";
            this.tableRow4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 15, 0, 100F);
            this.tableRow4.StylePriority.UsePadding = false;
            this.tableRow4.Weight = 1.3125D;
            // 
            // tableCell7
            // 
            this.tableCell7.Name = "tableCell7";
            this.tableCell7.StylePriority.UsePadding = false;
            this.tableCell7.Text = "TOTAAL";
            this.tableCell7.Weight = 5.17D;
            // 
            // tableCell8
            // 
            this.tableCell8.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "-[][[Account].[Type] = \'Credit\'].Sum([Amount]) - [][[Account].[Type] = \'Debit\'].S" +
                    "um([Amount])")});
            this.tableCell8.ForeColor = System.Drawing.Color.Black;
            this.tableCell8.Name = "tableCell8";
            this.tableCell8.StylePriority.UseFont = false;
            this.tableCell8.StylePriority.UseForeColor = false;
            this.tableCell8.StylePriority.UsePadding = false;
            this.tableCell8.StylePriority.UseTextAlignment = false;
            this.tableCell8.Text = "tableCell6";
            this.tableCell8.TextFormatString = "{0:c}";
            this.tableCell8.Weight = 1.5D;
            // 
            // GroupFooter1
            // 
            this.GroupFooter1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.table3});
            this.GroupFooter1.HeightF = 60F;
            this.GroupFooter1.Level = 1;
            this.GroupFooter1.Name = "GroupFooter1";
            // 
            // table3
            // 
            this.table3.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(201)))), ((int)(((byte)(194)))));
            this.table3.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.table3.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.table3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(86)))), ((int)(((byte)(85)))));
            this.table3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 9F);
            this.table3.Name = "table3";
            this.table3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 15, 0, 100F);
            this.table3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.tableRow3});
            this.table3.SizeF = new System.Drawing.SizeF(667F, 42F);
            this.table3.StylePriority.UseBorderColor = false;
            this.table3.StylePriority.UseBorders = false;
            this.table3.StylePriority.UseFont = false;
            this.table3.StylePriority.UseForeColor = false;
            this.table3.StylePriority.UsePadding = false;
            this.table3.StylePriority.UseTextAlignment = false;
            this.table3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // tableRow3
            // 
            this.tableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.tableCell5,
            this.tableCell6});
            this.tableRow3.Name = "tableRow3";
            this.tableRow3.StylePriority.UsePadding = false;
            this.tableRow3.Weight = 1D;
            // 
            // tableCell5
            // 
            this.tableCell5.Name = "tableCell5";
            this.tableCell5.StylePriority.UsePadding = false;
            this.tableCell5.Text = "SUBTOTAAL";
            this.tableCell5.Weight = 1.5907692307692307D;
            // 
            // tableCell6
            // 
            this.tableCell6.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "Iif([Account].[Type] = \'Credit\', -sumSum([Amount]), sumSum([Amount]))")});
            this.tableCell6.ForeColor = System.Drawing.Color.Black;
            this.tableCell6.Name = "tableCell6";
            this.tableCell6.StylePriority.UseFont = false;
            this.tableCell6.StylePriority.UseForeColor = false;
            this.tableCell6.StylePriority.UsePadding = false;
            this.tableCell6.StylePriority.UseTextAlignment = false;
            xrSummary2.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.tableCell6.Summary = xrSummary2;
            this.tableCell6.Text = "tableCell6";
            this.tableCell6.TextFormatString = "{0:c}";
            this.tableCell6.Weight = 0.46153846153846156D;
            // 
            // ReportHeader
            // 
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel2,
            this.xrLabel1,
            this.label2});
            this.ReportHeader.HeightF = 130F;
            this.ReportHeader.Name = "ReportHeader";
            // 
            // xrLabel2
            // 
            this.xrLabel2.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "\'BEDRIJF: \' + [Company].[Name]")});
            this.xrLabel2.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.xrLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(86)))), ((int)(((byte)(85)))));
            this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 50F);
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 5, 0, 100F);
            this.xrLabel2.SizeF = new System.Drawing.SizeF(450F, 32F);
            this.xrLabel2.StylePriority.UseFont = false;
            this.xrLabel2.StylePriority.UseForeColor = false;
            this.xrLabel2.StylePriority.UsePadding = false;
            this.xrLabel2.Text = "xrLabel2";
            // 
            // xrLabel1
            // 
            this.xrLabel1.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "\'PERIODE: \' + [Parameters.XafReportParametersObject]")});
            this.xrLabel1.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.xrLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(86)))), ((int)(((byte)(85)))));
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 82F);
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 5, 0, 100F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(450F, 32F);
            this.xrLabel1.StylePriority.UseFont = false;
            this.xrLabel1.StylePriority.UseForeColor = false;
            this.xrLabel1.StylePriority.UsePadding = false;
            this.xrLabel1.Text = "xrLabel1";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Segoe UI", 26.25F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(86)))), ((int)(((byte)(85)))));
            this.label2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.label2.Name = "label2";
            this.label2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.label2.SizeF = new System.Drawing.SizeF(450F, 50F);
            this.label2.StylePriority.UseFont = false;
            this.label2.StylePriority.UseForeColor = false;
            this.label2.StylePriority.UsePadding = false;
            this.label2.Text = "Winst- en verliesrekening";
            // 
            // GroupHeader2
            // 
            this.GroupHeader2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.table2});
            this.GroupHeader2.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("Account.Name", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
            this.GroupHeader2.HeightF = 35F;
            this.GroupHeader2.Name = "GroupHeader2";
            // 
            // collectionDataSource1
            // 
            this.collectionDataSource1.Name = "collectionDataSource1";
            this.collectionDataSource1.ObjectTypeName = "Accounting.Module.BusinessObjects.JournalEntryLine";
            this.collectionDataSource1.TopReturnedRecords = 0;
            // 
            // ProfitAndLossStatementReport
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.topMarginBand1,
            this.detailBand1,
            this.bottomMarginBand1,
            this.GroupHeader1,
            this.ReportFooter,
            this.GroupFooter1,
            this.ReportHeader,
            this.GroupHeader2});
            this.ComponentStorage.AddRange(new System.ComponentModel.IComponent[] {
            this.collectionDataSource1});
            this.DataSource = this.collectionDataSource1;
            this.DisplayName = "Winst- en verliesrekening";
            this.Extensions.Add("DataSerializationExtension", "XtraReport");
            this.Extensions.Add("DataEditorExtension", "XtraReport");
            this.Extensions.Add("ParameterEditorExtension", "XtraReport");
            this.Margins = new System.Drawing.Printing.Margins(80, 80, 75, 40);
            this.PageHeight = 1169;
            this.PageWidth = 827;
            this.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.Version = "18.2";
            ((System.ComponentModel.ISupportInitialize)(this.table2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.table1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.table4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.table3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.collectionDataSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.TopMarginBand topMarginBand1;
        private DevExpress.XtraReports.UI.DetailBand detailBand1;
        private DevExpress.XtraReports.UI.XRTable table2;
        private DevExpress.XtraReports.UI.XRTableRow tableRow2;
        private DevExpress.XtraReports.UI.XRTableCell tableCell3;
        private DevExpress.XtraReports.UI.XRTableCell tableCell4;
        private DevExpress.XtraReports.UI.BottomMarginBand bottomMarginBand1;
        private DevExpress.XtraReports.UI.GroupHeaderBand GroupHeader1;
        private DevExpress.XtraReports.UI.XRTable table1;
        private DevExpress.XtraReports.UI.XRTableRow tableRow1;
        private DevExpress.XtraReports.UI.XRTableCell tableCell1;
        private DevExpress.XtraReports.UI.XRTableCell tableCell2;
        private DevExpress.XtraReports.UI.ReportFooterBand ReportFooter;
        private DevExpress.XtraReports.UI.XRTable table4;
        private DevExpress.XtraReports.UI.XRTableRow tableRow4;
        private DevExpress.XtraReports.UI.XRTableCell tableCell7;
        private DevExpress.XtraReports.UI.XRTableCell tableCell8;
        private DevExpress.XtraReports.UI.GroupFooterBand GroupFooter1;
        private DevExpress.XtraReports.UI.XRTable table3;
        private DevExpress.XtraReports.UI.XRTableRow tableRow3;
        private DevExpress.XtraReports.UI.XRTableCell tableCell5;
        private DevExpress.XtraReports.UI.XRTableCell tableCell6;
        private DevExpress.XtraReports.UI.ReportHeaderBand ReportHeader;
        private DevExpress.XtraReports.UI.XRLabel label2;
        private DevExpress.Persistent.Base.ReportsV2.CollectionDataSource collectionDataSource1;
        private DevExpress.XtraReports.UI.GroupHeaderBand GroupHeader2;
        private DevExpress.XtraReports.UI.XRLabel xrLabel1;
        private DevExpress.XtraReports.UI.XRLabel label3;
        private DevExpress.XtraReports.UI.XRLabel xrLabel2;
    }
}
