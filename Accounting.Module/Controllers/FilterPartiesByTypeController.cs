using Accounting.Module.BusinessObjects;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Utils;
using System.Linq;

namespace Accounting.Module.Controllers
{
    public class FilterPartiesByTypeController : FilterObjectsController<Party>
    {
        protected override void OnActivated()
        {
            base.OnActivated();

            FilterObjectsAction.Items.Add(new ChoiceActionItem("Customer", CaptionHelper.GetDisplayText(PartyRole.Customer), new BinaryOperator("Role", PartyRole.Customer)));
            FilterObjectsAction.Items.Add(new ChoiceActionItem("Supplier", CaptionHelper.GetDisplayText(PartyRole.Supplier), new BinaryOperator("Role", PartyRole.Supplier)));

            FilterObjectsAction.SelectedItem =
                FilterObjectsAction.Items.FirstOrDefault(x => x.Id == Application.Model.ActionDesign.Actions[FilterObjectsAction.Id].GetValue<string>("SelectedFilter")) ??
                FilterObjectsAction.Items.First(x => x.Id == "All");
        }
    }
}