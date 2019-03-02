using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using System.ComponentModel;

namespace Accounting.Module.BusinessObjects.Parameters
{
    [Appearance("Actions", AppearanceItemType.Action, "True", TargetItems = "Delete;New;Save;SaveAndClose;SaveAndNew", Visibility = ViewItemVisibility.Hide)]
    [DomainComponent]
    [ModelDefault("Caption", "New VAT Declaration")]
    public class NewVatDeclarationParameters
    {
        public VatDeclarationPeriod Period { get; set; }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public VatDeclarationType Type { get; set; }

        [ModelDefault("DisplayFormat", "{0:D0}")]
        [ModelDefault("EditMask", "D0")]
        public int Year { get; set; }
    }
}