using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Model;
using DevExpress.Xpo;
using System.ComponentModel;

namespace Accounting.Module.BusinessObjects
{
    [DefaultProperty("Name")]
    [ModelDefault("DefaultLookupEditorMode", "AllItems")]
    public abstract class Party : Organization
    {
        protected Party(Session session) : base(session)
        {
        }

        [ModelDefault("Caption", "VAT Included")]
        [ModelDefault("CaptionForFalse", "No")]
        [ModelDefault("CaptionForTrue", "Yes")]
        public bool IsVatIncluded
        {
            get => GetPropertyValue<bool>(nameof(IsVatIncluded));
            set => SetPropertyValue(nameof(IsVatIncluded), value);
        }

        [ModelDefault("AllowClear", "False")]
        public PaymentTerm PaymentTerm
        {
            get => GetPropertyValue<PaymentTerm>(nameof(PaymentTerm));
            set => SetPropertyValue(nameof(PaymentTerm), value);
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            PaymentTerm = Session.FindObject<PaymentTerm>(new BinaryOperator("Term", 30));
        }
    }
}