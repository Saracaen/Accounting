using Accounting.Module.Model;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.SystemModule;
using System.ComponentModel;

namespace Accounting.Module.Controllers
{
    [DesignerCategory("Code")]
    public class ShowDetailViewController : ViewController<ListView>, IModelExtender
    {
        protected override void OnActivated()
        {
            base.OnActivated();

            var listViewProcessCurrentObjectController = Frame.GetController<ListViewProcessCurrentObjectController>();
            if (listViewProcessCurrentObjectController != null)
            {
                listViewProcessCurrentObjectController.ProcessCurrentObjectAction.Enabled[Name] = ((IModelShowDetailView)View.Model).ShowDetailView;
            }
        }

        #region IModelExtender

        void IModelExtender.ExtendModelInterfaces(ModelInterfaceExtenders extenders)
        {
            extenders.Add<IModelListView, IModelShowDetailView>();
        }

        #endregion IModelExtender
    }
}