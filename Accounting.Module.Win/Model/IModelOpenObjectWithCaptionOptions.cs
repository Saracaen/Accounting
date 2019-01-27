using DevExpress.ExpressApp.Model;
using System.ComponentModel;

namespace Accounting.Module.Win.Model
{
    public interface IModelOpenObjectWithCaptionOptions : IModelNode
    {
        [Localizable(true)]
        string UseLowerCaseClassCaptions { get; set; }
    }
}