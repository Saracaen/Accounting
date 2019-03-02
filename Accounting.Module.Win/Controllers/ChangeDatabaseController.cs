using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Utils;
using System;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace Accounting.Module.Win.Controllers
{
    [DesignerCategory("Code")]
    public class ChangeDatabaseController : WindowController
    {
        public ChangeDatabaseController()
        {
            TargetWindowType = WindowType.Main;

            NewDatabaseAction = new SimpleAction(this, "NewDatabase", "File");
            NewDatabaseAction.Caption = "New Database...";
            NewDatabaseAction.Execute += NewDatabaseAction_Execute;
            NewDatabaseAction.ImageName = "NewDataSource";

            OpenDatabaseAction = new SimpleAction(this, "OpenDatabase", "File");
            OpenDatabaseAction.Caption = "Open Database...";
            OpenDatabaseAction.Execute += OpenDatabaseAction_Execute;
            OpenDatabaseAction.ImageName = "SelectDataSource";

            RegisterActions(NewDatabaseAction, OpenDatabaseAction);
        }

        public SimpleAction NewDatabaseAction { get; }

        public SimpleAction OpenDatabaseAction { get; }

        private string GetRelativeFileName(string fileName)
        {
            var applicationUri = new Uri(Assembly.GetEntryAssembly().Location);
            var fileUri = new Uri(fileName);

            return Uri.UnescapeDataString(applicationUri.MakeRelativeUri(fileUri).ToString());
        }

        private void NewDatabaseAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            using (var dialog = new SaveFileDialog { DefaultExt = "db", Filter = CaptionHelper.GetLocalizedText("Texts", "DatabaseFiles") })
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    UpdateConnectionString(dialog.FileName);
                }
            }
        }

        private void OpenDatabaseAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            using (var dialog = new OpenFileDialog { DefaultExt = "db", Filter = CaptionHelper.GetLocalizedText("Texts", "DatabaseFiles") })
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    UpdateConnectionString(dialog.FileName);
                }
            }
        }

        private void UpdateConnectionString(string fileName)
        {
            var configurationMap = new ExeConfigurationFileMap { ExeConfigFilename = Path.ChangeExtension(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile, ".config") };
            var configuration = ConfigurationManager.OpenMappedExeConfiguration(configurationMap, ConfigurationUserLevel.None);
            var connectionString = configuration.ConnectionStrings.ConnectionStrings["ConnectionString"];

            if (connectionString != null)
            {
                connectionString.ConnectionString = $"XpoProvider=SQLite;Data Source={GetRelativeFileName(fileName)};DateTimeKind=Utc";
                configuration.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("connectionStrings");
            }

            Application.LogOff();
        }
    }
}