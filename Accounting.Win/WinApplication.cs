using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Win;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.Base;
using System.Configuration;
using System.Globalization;
using System.Threading;

namespace Accounting.Win
{
    public partial class AccountingWindowsFormsApplication : WinApplication
    {
        static AccountingWindowsFormsApplication()
        {
            PasswordCryptographer.EnableRfc2898 = true;
            PasswordCryptographer.SupportLegacySha512 = false;
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("nl-NL");
        }

        public AccountingWindowsFormsApplication()
        {
            InitializeComponent();
            InitializeDefaults();
        }

        protected override void CreateDefaultObjectSpaceProvider(CreateCustomObjectSpaceProviderEventArgs args)
        {
            args.ObjectSpaceProviders.Add(new XPObjectSpaceProvider(XPObjectSpaceProvider.GetDataStoreProvider(args.ConnectionString, args.Connection, true), false));
            args.ObjectSpaceProviders.Add(new NonPersistentObjectSpaceProvider(TypesInfo, null));
        }

        protected override void OnCustomizeFormattingCulture(CustomizeFormattingCultureEventArgs args)
        {
            base.OnCustomizeFormattingCulture(args);
            args.FormattingCulture = CultureInfo.GetCultureInfo("nl-NL");
        }

        protected override void OnLoggingOn(LogonEventArgs args)
        {
            if (ConfigurationManager.ConnectionStrings["ConnectionString"] != null)
            {
                ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            }
            base.OnLoggingOn(args);
        }

        private void AccountingWindowsFormsApplication_CustomizeLanguagesList(object sender, CustomizeLanguagesListEventArgs e)
        {
            var userLanguageName = Thread.CurrentThread.CurrentUICulture.Name;
            if (userLanguageName != "en-US" && e.Languages.IndexOf(userLanguageName) == -1)
            {
                e.Languages.Add(userLanguageName);
            }
        }

        private void AccountingWindowsFormsApplication_DatabaseVersionMismatch(object sender, DatabaseVersionMismatchEventArgs e)
        {
            e.Updater.Update();
            e.Handled = true;
        }

        private void InitializeDefaults()
        {
            EnableModelCache = true;
            OptimizedControllersCreation = true;
            UseLightStyle = true;
        }
    }
}