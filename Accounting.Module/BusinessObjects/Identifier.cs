using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System.ComponentModel;

namespace Accounting.Module.BusinessObjects
{
    [Appearance("Actions", AppearanceItemType.Action, "True", TargetItems = "Delete;New;SaveAndNew", Visibility = ViewItemVisibility.Hide)]
    [ImageName("Action_Security_ChangePassword")]
    public class Identifier : BaseObject
    {
        public Identifier(Session session) : base(session)
        {
        }

        [RuleRange("Identifier_Decimals_RuleRange", DefaultContexts.Save, 1, 20)]
        public int Decimals
        {
            get => GetPropertyValue<int>(nameof(Decimals));
            set => SetPropertyValue(nameof(Decimals), value);
        }

        public string Prefix
        {
            get => GetPropertyValue<string>(nameof(Prefix));
            set => SetPropertyValue(nameof(Prefix), value);
        }

        [RuleRange("Identifier_StartValue_RuleRange", DefaultContexts.Save, 1, int.MaxValue)]
        public int StartValue
        {
            get => GetPropertyValue<int>(nameof(StartValue));
            set => SetPropertyValue(nameof(StartValue), value);
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string TargetType
        {
            get => GetPropertyValue<string>(nameof(TargetType));
            set => SetPropertyValue(nameof(TargetType), value);
        }

        [ModelDefault("AllowEdit", "False")]
        public IdentifierType Type
        {
            get => GetPropertyValue<IdentifierType>(nameof(Type));
            set => SetPropertyValue(nameof(Type), value);
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            StartValue = 1;
            Decimals = 6;
        }
    }
}