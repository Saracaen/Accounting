using DevExpress.ExpressApp.Model;
using System.ComponentModel;

namespace Accounting.Module.Model
{
    public interface IModelShowDetailView : IModelNode
    {
        [DefaultValue(true)]
        bool ShowDetailView { get; set; }
    }
}