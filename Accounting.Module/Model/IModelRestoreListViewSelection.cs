using System;

namespace Accounting.Module.Model
{
    public interface IModelRestoreListViewSelection
    {
        Guid SelectedItem { get; set; }
    }
}