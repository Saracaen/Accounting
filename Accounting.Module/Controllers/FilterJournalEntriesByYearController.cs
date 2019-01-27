using Accounting.Module.BusinessObjects;
using System.Linq;

namespace Accounting.Module.Controllers
{
    public class FilterJournalEntriesByYearController : FilterObjectsByYearController<JournalEntry>
    {
        public FilterJournalEntriesByYearController()
        {
            FilterObjectsAction.TargetObjectsCriteria = "Type In ('Closure', 'Entry')";
        }

        protected override IQueryable<JournalEntry> GetObjectsQuery()
        {
            return base.GetObjectsQuery().Where(x => x.Type == JournalEntryType.Closure || x.Type == JournalEntryType.Entry);
        }
    }
}