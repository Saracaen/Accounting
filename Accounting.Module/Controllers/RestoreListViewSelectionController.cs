using Accounting.Module.Model;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using System;
using System.ComponentModel;

namespace Accounting.Module.Controllers
{
    [DesignerCategory("Code")]
    public class RestoreListViewSelectionController : ObjectViewController<ListView, BaseObject>, IModelExtender
    {
        private bool selectionRestored;

        public RestoreListViewSelectionController()
        {
            TargetViewNesting = Nesting.Root;
        }

        protected override void OnActivated()
        {
            base.OnActivated();

            this.selectionRestored = false;
            View.SelectionChanged += View_SelectionChanged;
        }

        protected override void OnDeactivated()
        {
            base.OnDeactivated();
            View.SelectionChanged -= View_SelectionChanged;
        }

        private void View_SelectionChanged(object sender, EventArgs e)
        {
            if (this.selectionRestored)
            {
                View.Model.SetValue("SelectedItem", ViewCurrentObject?.Oid ?? Guid.Empty);
            }
            else
            {
                this.selectionRestored = true;
                View.CurrentObject = ObjectSpace.GetObjectByKey(View.ObjectTypeInfo.Type, View.Model.GetValue<Guid>("SelectedItem"));
            }
        }

        #region IModelExtender

        void IModelExtender.ExtendModelInterfaces(ModelInterfaceExtenders extenders)
        {
            extenders.Add<IModelListView, IModelRestoreListViewSelection>();
        }

        #endregion IModelExtender
    }
}