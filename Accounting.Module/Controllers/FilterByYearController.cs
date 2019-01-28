using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using System;
using System.ComponentModel;
using System.Linq;

namespace Accounting.Module.Controllers
{
    [DesignerCategory("Code")]
    public abstract class FilterObjectsByYearController<T> : ObjectViewController<ListView, T> where T : ISupportDate
    {
        protected FilterObjectsByYearController()
        {
            FilterObjectsAction = new SingleChoiceAction(this, "Filter" + typeof(T).Name, PredefinedCategory.FullTextSearch);
            FilterObjectsAction.Caption = "Filter";
            FilterObjectsAction.SelectedItemChanged += FilterAction_SelectedItemChanged;

            RegisterActions(FilterObjectsAction);
        }

        public SingleChoiceAction FilterObjectsAction { get; }

        protected virtual IQueryable<T> GetObjectsQuery()
        {
            return ObjectSpace.GetObjectsQuery<T>();
        }

        protected override void OnActivated()
        {
            base.OnActivated();

            FilterObjectsAction.Items.Add(new ChoiceActionItem("All", CaptionHelper.GetLocalizedText("Texts", "All"), null));

            foreach (var obj in GetObjectsQuery().GroupBy(x => x.Date.Year).Select(x => x.First()))
            {
                FilterObjectsAction.Items.Add(new ChoiceActionItem(obj.Date.Year.ToString(), obj.Date.Year.ToString(), CriteriaOperator.Parse("GetYear(Date) = ?", obj.Date.Year)));
            }

            FilterObjectsAction.SelectedItem =
                FilterObjectsAction.Items.FirstOrDefault(x => x.Id == Application.Model.ActionDesign.Actions[FilterObjectsAction.Id].GetValue<string>("SelectedFilter")) ??
                FilterObjectsAction.Items.First(x => x.Id == "All");
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