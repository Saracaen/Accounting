using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;

namespace Accounting.Module.BusinessObjects
{
    [Appearance("Actions", AppearanceItemType.Action, "True", TargetItems = "Delete;New;Save;SaveAndClose;SaveAndNew", Visibility = ViewItemVisibility.Hide)]
    [ModelDefault("DefaultLookupEditorMode", "AllItems")]
    public class Country : BaseObject
    {
        public Country(Session session) : base(session)
        {
        }

        [ModelDefault("AllowEdit", "False")]
        public string Code
        {
            get => GetPropertyValue<string>(nameof(Code));
            set => SetPropertyValue(nameof(Code), value);
        }

        [ModelDefault("AllowEdit", "False")]
        public string Name
        {
            get => GetPropertyValue<string>(nameof(Name));
            set => SetPropertyValue(nameof(Name), value);
        }
    }
}