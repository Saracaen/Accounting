using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.Persistent.Base;
using System;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace Accounting.Win
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            AboutInfo.Instance.Version = $"Versie {FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductVersion}";
            EditModelPermission.AlwaysGranted = Debugger.IsAttached;
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("nl-NL");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Tracing.Initialize();

            using (var winApplication = new AccountingWindowsFormsApplication())
            {
                if (ConfigurationManager.ConnectionStrings["ConnectionString"] != null)
                {
                    winApplication.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                }

                if (Debugger.IsAttached && winApplication.CheckCompatibilityType == CheckCompatibilityType.DatabaseSchema)
                {
                    winApplication.DatabaseUpdateMode = DatabaseUpdateMode.UpdateDatabaseAlways;
                }

                try
                {
                    winApplication.Setup();
                    winApplication.Start();
                }
                catch (Exception e)
                {
                    winApplication.HandleException(e);
                }
            }
        }
    }
}