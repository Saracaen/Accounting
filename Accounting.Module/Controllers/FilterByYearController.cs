using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Actions;
using System.ComponentModel;
using System.Linq;

namespace Accounting.Module.Controllers
{
    [DesignerCategory("Code")]
    public abstract class FilterObjectsByYearController<T> : FilterObjectsController<T> where T : ISupportDate
    {
        protected virtual IQueryable<T> GetObjectsQuery()
        {
            return ObjectSpace.GetObjectsQuery<T>();
        }

        protected override void OnActivated()
        {
            base.OnActivated();

            foreach (var obj in GetObjectsQuery().GroupBy(x => x.Date.Year).Select(x => x.First()))
            {
                FilterObjectsAction.Items.Add(new ChoiceActionItem(obj.Date.Year.ToString(), obj.Date.Year.ToString(), CriteriaOperator.Parse("GetYear(Date) = ?", obj.Date.Year)));
            }

            FilterObjectsAction.SelectedItem =
                FilterObjectsAction.Items.FirstOrDefault(x => x.Id == Application.Model.ActionDesign.Actions[FilterObjectsAction.Id].GetValue<string>("SelectedFilter")) ??
                FilterObjectsAction.Items.First(x => x.Id == "All");
        }
    }
}