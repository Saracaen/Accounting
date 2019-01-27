using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Utils;
using DevExpress.ExpressApp.Win.Utils;
using DevExpress.Persistent.Base;
using DevExpress.XtraEditors;
using System;
using System.Configuration;
using System.Diagnostics;
using System.Reflection;
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
            ImageLoader.Instance.UseSvgImages = true;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            WindowsFormsSettings.ScrollUIMode = ScrollUIMode.Fluent;
            WindowsFormsSettings.ForceDirectXPaint();

            Tracing.Initialize();

            using (var winApplication = new AccountingWindowsFormsApplication())
            {
                winApplication.SplashScreen = new DXSplashScreen();

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