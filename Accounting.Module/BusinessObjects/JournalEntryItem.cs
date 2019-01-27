using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System.ComponentModel;

namespace Accounting.Module.BusinessObjects
{
    [Appearance("Posted", "IsPosted", TargetItems = "*", Enabled = false)]
    public abstract class JournalEntryItem : BaseObject
    {
        protected JournalEntryItem(Session session) : base(session)
        {
            JournalEntries.ListChanged += JournalEntries_ListChanged;
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool IsPosted
        {
            get => GetPropertyValue<bool>(nameof(IsPosted));
            set => SetPropertyValue(nameof(IsPosted), value);
        }

        [Association]
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public XPCollection<JournalEntry> JournalEntries
        {
            get => GetCollection<JournalEntry>(nameof(JournalEntries));
        }

        protected virtual void OnJournalEntriesListChanged()
        {
            IsPosted = JournalEntries.Count > 0;
        }

        private void JournalEntries_ListChanged(object sender, ListChangedEventArgs e)
        {
            switch (e.ListChangedType)
            {
                case ListChangedType.ItemAdded:
                case ListChangedType.ItemChanged:
                case ListChangedType.ItemDeleted:
                case ListChangedType.ItemMoved:
                    OnChanged(nameof(JournalEntries));
                    OnJournalEntriesListChanged();
                    break;
            }
        }
    }
}