using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;

namespace Accounting.Module.BusinessObjects
{
    [ModelDefault("DefaultLookupEditorMode", "AllItems")]
    public class Country : BaseObject
    {
        public Country(Session session) : base(session)
        {
        }

        public string Code
        {
            get => GetPropertyValue<string>(nameof(Code));
            set => SetPropertyValue(nameof(Code), value);
        }

        public string Name
        {
            get => GetPropertyValue<string>(nameof(Name));
            set => SetPropertyValue(nameof(Name), value);
        }
    }
}