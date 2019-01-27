using DevExpress.ExpressApp.Model;

namespace Accounting.Module.Model
{
    public interface IModelFilterObjects : IModelNode
    {
        string SelectedFilter { get; set; }
    }
}