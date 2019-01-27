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

            ChangeDatabaseAction = new SimpleAction(this, "ChangeDatabase", "About");
            ChangeDatabaseAction.Caption = "Change Database...";
            ChangeDatabaseAction.Execute += ChangeDatabaseAction_Execute;
            ChangeDatabaseAction.ImageName = "Action_Security_ChangePassword";

            RegisterActions(ChangeDatabaseAction);
        }

        public SimpleAction ChangeDatabaseAction { get; }

        private void ChangeDatabaseAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            using (var dialog = new SaveFileDialog { DefaultExt = "db", Filter = CaptionHelper.GetLocalizedText("Texts", "DatabaseFiles"), OverwritePrompt = false })
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    var configurationMap = new ExeConfigurationFileMap { ExeConfigFilename = Path.ChangeExtension(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile, ".config") };
                    var configuration = ConfigurationManager.OpenMappedExeConfiguration(configurationMap, ConfigurationUserLevel.None);
                    var connectionString = configuration.ConnectionStrings.ConnectionStrings["ConnectionString"];

                    if (connectionString != null)
                    {
                        connectionString.ConnectionString = $"XpoProvider=SQLite;Data Source={GetRelativeFileName(dialog.FileName)};DateTimeKind=Utc";
                        configuration.Save(ConfigurationSaveMode.Modified);
                        ConfigurationManager.RefreshSection("connectionStrings");
                    }

                    Application.LogOff();
                }
            }
        }

        private string GetRelativeFileName(string fileName)
        {
            var applicationUri = new Uri(Assembly.GetEntryAssembly().Location);
            var fileUri = new Uri(fileName);

            return Uri.UnescapeDataString(applicationUri.MakeRelativeUri(fileUri).ToString());
        }
    }
}