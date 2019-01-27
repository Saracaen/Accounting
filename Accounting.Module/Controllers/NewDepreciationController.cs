using Accounting.Module.BusinessObjects;
using Accounting.Module.BusinessObjects.Parameters;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System;
using System.ComponentModel;

namespace Accounting.Module.Controllers
{
    [DesignerCategory("Code")]
    public class NewDepreciationController : ObjectViewController<ObjectView, Depreciation>
    {
        public NewDepreciationController()
        {
            NewDepreciationAction = new PopupWindowShowAction(this, "NewDepreciation", PredefinedCategory.ObjectsCreation);
            NewDepreciationAction.Caption = "New";
            NewDepreciationAction.CustomizePopupWindowParams += NewDepreciationAction_CustomizePopupWindowParams;
            NewDepreciationAction.Execute += NewDepreciationAction_Execute;
            NewDepreciationAction.ImageName = "Action_New";
            NewDepreciationAction.Shortcut = "CtrlN";

            SaveAndNewDepreciationAction = new PopupWindowShowAction(this, "SaveAndNewDepreciation", PredefinedCategory.Save);
            SaveAndNewDepreciationAction.Caption = "Save and New";
            SaveAndNewDepreciationAction.CustomizePopupWindowParams += NewDepreciationAction_CustomizePopupWindowParams;
            SaveAndNewDepreciationAction.Execute += NewDepreciationAction_Execute;
            SaveAndNewDepreciationAction.Executing += SaveAndNewDepreciationAction_Executing;
            SaveAndNewDepreciationAction.ImageName = "Action_Save_New";
            SaveAndNewDepreciationAction.TargetViewType = ViewType.DetailView;

            RegisterActions(NewDepreciationAction, SaveAndNewDepreciationAction);
        }

        public PopupWindowShowAction NewDepreciationAction { get; }

        public PopupWindowShowAction SaveAndNewDepreciationAction { get; }

        protected override void OnActivated()
        {
            base.OnActivated();

            var newObjectViewController = Frame.GetController<NewObjectViewController>();
            if (newObjectViewController != null)
            {
                newObjectViewController.Active[Name] = false;
            }

            NewDepreciationAction.Active[Name] = View.AllowNew;
            SaveAndNewDepreciationAction.Active[Name] = View.AllowNew;
        }

        protected override void OnDeactivated()
        {
            base.OnDeactivated();
            Frame.GetController<NewObjectViewController>()?.Active.RemoveItem(Name);
        }

        private void NewDepreciationAction_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            var objectSpace = Application.CreateObjectSpace(typeof(NewDepreciationParameters));
            var parameters = new NewDepreciationParameters();
            var detailView = Application.CreateDetailView(objectSpace, parameters);

            detailView.ViewEditMode = ViewEditMode.Edit;
            e.View = detailView;
        }

        private void NewDepreciationAction_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            var parameters = (NewDepreciationParameters)e.PopupWindowViewCurrentObject;
            Validator.RuleSet.Validate(e.PopupWindowView.ObjectSpace, parameters, DefaultContexts.Save);

            var objectSpace = Application.CreateObjectSpace();
            var depreciation = objectSpace.CreateObject<Depreciation>();
            var detailView = Application.CreateDetailView(objectSpace, depreciation, View);

            depreciation.DepreciationTime = parameters.DepreciationTime;
            depreciation.Description = parameters.Description;
            depreciation.Type = parameters.Type;
            depreciation.ResidualValue = parameters.ResidualValue;
            depreciation.StartDate = parameters.StartDate;
            depreciation.Value = parameters.Value;

            detailView.ViewEditMode = ViewEditMode.Edit;

            var depreciationDate = depreciation.StartDate;
            var valueToDepreciate = depreciation.Value - depreciation.ResidualValue;

            while (valueToDepreciate > 0)
            {
                var depreciationLine = objectSpace.CreateObject<DepreciationLine>();
                switch (depreciation.Type)
                {
                    case DepreciationType.Annual:
                        depreciationLine.Amount = (13 - depreciationDate.Month) / 12m * ((depreciation.Value - depreciation.ResidualValue) / depreciation.DepreciationTime);
                        depreciationDate = new DateTime(depreciationDate.Year + 1, 1, 1);
                        break;

                    case DepreciationType.Monthly:
                        depreciationLine.Amount = (depreciation.Value - depreciation.ResidualValue) / (depreciation.DepreciationTime * 12);
                        depreciationDate = depreciationDate.AddMonths(1).AddDays(1 - depreciationDate.Day);
                        break;

                    default:
                        throw new InvalidOperationException(CaptionHelper.GetLocalizedText(@"Exceptions\UserVisibleExceptions", "UnsupportedDepreciationType"));
                }

                depreciationLine.Amount = Math.Min(Math.Round(depreciationLine.Amount, 2), valueToDepreciate);
                depreciationLine.Depreciation = depreciation;
                depreciationLine.Date = depreciationDate.AddDays(-1);

                valueToDepreciate -= depreciationLine.Amount;
            }

            Application.ShowViewStrategy.ShowView(new ShowViewParameters(detailView), new ShowViewSource(Frame, NewDepreciationAction));
        }

        private void SaveAndNewDepreciationAction_Executing(object sender, CancelEventArgs e)
        {
            ObjectSpace.CommitChanges();
        }
    }
}