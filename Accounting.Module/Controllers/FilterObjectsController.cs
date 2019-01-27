using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using System;
using System.ComponentModel;

namespace Accounting.Module.Controllers
{
    [DesignerCategory("Code")]
    public abstract class FilterObjectsController<T> : ObjectViewController<ListView, T>
    {
        protected FilterObjectsController()
        {
            FilterObjectsAction = new SingleChoiceAction(this, "Filter" + typeof(T).Name, PredefinedCategory.FullTextSearch);
            FilterObjectsAction.Caption = "Filter";
            FilterObjectsAction.SelectedItemChanged += FilterAction_SelectedItemChanged;

            RegisterActions(FilterObjectsAction);
        }

        public SingleChoiceAction FilterObjectsAction { get; }

        protected override void OnActivated()
        {
            base.OnActivated();
            FilterObjectsAction.Items.Add(new ChoiceActionItem("All", CaptionHelper.GetLocalizedText("Texts", "All"), null));
        }

        protected override void OnDeactivated()
        {
            base.OnDeactivated();
            FilterObjectsAction.Items.Clear();
        }

        private void FilterAction_SelectedItemChanged(object sender, EventArgs e)
        {
            if (FilterObjectsAction.SelectedItem != null)
            {
                View.CollectionSource.Criteria[Name] = (CriteriaOperator)FilterObjectsAction.SelectedItem.Data;
                Application.Model.ActionDesign.Actions["Filter" + typeof(T).Name].SetValue("SelectedFilter", FilterObjectsAction.SelectedItem.Id);
            }
        }
    }
}