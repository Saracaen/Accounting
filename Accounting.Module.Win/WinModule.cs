using Accounting.Module.Win.Controllers;
using Accounting.Module.Win.Editors;
using Accounting.Module.Win.Model;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Updating;
using DevExpress.ExpressApp.Win.SystemModule;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Accounting.Module.Win
{
    [ToolboxItemFilter("Xaf.Platform.Win")]
    public sealed partial class AccountingWindowsFormsModule : ModuleBase
    {
        public AccountingWindowsFormsModule()
        {
            InitializeComponent();
        }

        public override void CustomizeLogics(CustomLogics customLogics)
        {
            base.CustomizeLogics(customLogics);
            customLogics.RegisterLogic(typeof(IModelTemplateOfficeNavigationBarCustomization), typeof(ModelTemplateOfficeNavigationBarCustomizationLogic));
        }

        public override IEnumerable<ModuleUpdater> GetModuleUpdaters(IObjectSpace objectSpace, Version versionFromDB)
        {
            return ModuleUpdater.EmptyModuleUpdaters;
        }

        protected override IEnumerable<Type> GetDeclaredControllerTypes()
        {
            return new[]
            {
                typeof(CalculateResidualValueController),
                typeof(CalculateRunningBalanceController),
                typeof(ChangeDatabaseController),
                typeof(CustomizeNavigationController),
                typeof(DisableControllersController),
                typeof(HideNavigationActionsController),
                typeof(OpenObjectWithCaptionController)
            };
        }

        protected override IEnumerable<Type> GetDeclaredExportedTypes()
        {
            return Type.EmptyTypes;
        }

        protected override IEnumerable<Type> GetRegularTypes()
        {
            return Type.EmptyTypes;
        }

        protected override void RegisterEditorDescriptors(EditorDescriptorsFactory editorDescriptorsFactory)
        {
            base.RegisterEditorDescriptors(editorDescriptorsFactory);

            editorDescriptorsFactory.RegisterPropertyEditor(typeof(DateTime), typeof(RangedDatePropertyEditor), true);
            editorDescriptorsFactory.RegisterPropertyEditor(typeof(decimal), typeof(RangedDecimalPropertyEditor), true);
            editorDescriptorsFactory.RegisterPropertyEditor(typeof(double), typeof(RangedDoublePropertyEditor), true);
            editorDescriptorsFactory.RegisterPropertyEditor(typeof(Enum), typeof(FilteringEnumPropertyEditor), true);
            editorDescriptorsFactory.RegisterPropertyEditor(typeof(float), typeof(RangedFloatPropertyEditor), true);
            editorDescriptorsFactory.RegisterPropertyEditor(typeof(int), typeof(RangedIntegerPropertyEditor), true);
            editorDescriptorsFactory.RegisterPropertyEditor(typeof(long), typeof(RangedLongPropertyEditor), true);
            editorDescriptorsFactory.RegisterPropertyEditor(typeof(short), typeof(RangedIntegerPropertyEditor), true);
            editorDescriptorsFactory.RegisterPropertyEditor(typeof(uint), typeof(RangedIntegerPropertyEditor), true);
            editorDescriptorsFactory.RegisterPropertyEditor(typeof(ulong), typeof(RangedIntegerPropertyEditor), true);
            editorDescriptorsFactory.RegisterPropertyEditor(typeof(ushort), typeof(RangedIntegerPropertyEditor), true);
        }
    }
}