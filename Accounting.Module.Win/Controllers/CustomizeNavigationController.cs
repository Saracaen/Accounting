using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.XtraBars.Navigation;
using System.ComponentModel;
using System.Windows.Forms;

namespace Accounting.Module.Win.Controllers
{
    [DesignerCategory("Code")]
    public class CustomizeNavigationController : WindowController
    {
        private Control control;
        private SingleChoiceAction showNavigationItemAction;

        public CustomizeNavigationController()
        {
            TargetWindowType = WindowType.Main;
        }

        protected override void OnActivated()
        {
            base.OnActivated();

            this.control = null;
            this.showNavigationItemAction = Frame.GetController<ShowNavigationItemController>()?.ShowNavigationItemAction;

            if (this.showNavigationItemAction != null)
            {
                this.showNavigationItemAction.Changed += ShowNavigationItemAction_Changed;
                this.showNavigationItemAction.CustomizeControl += ShowNavigationItemAction_CustomizeControl;
            }
        }

        protected override void OnDeactivated()
        {
            base.OnDeactivated();

            if (this.showNavigationItemAction != null)
            {
                this.showNavigationItemAction.Changed -= ShowNavigationItemAction_Changed;
                this.showNavigationItemAction.CustomizeControl -= ShowNavigationItemAction_CustomizeControl;
            }
        }

        private void ShowNavigationItemAction_Changed(object sender, ActionChangedEventArgs e)
        {
            if (this.control != null)
            {
                if (e.ChangedPropertyType == ActionChangedType.Active)
                {
                    this.control.Visible = this.showNavigationItemAction.Active;
                }
                else if (e.ChangedPropertyType == ActionChangedType.Enabled)
                {
                    this.control.Enabled = this.showNavigationItemAction.Enabled;
                }
            }
        }

        private void ShowNavigationItemAction_CustomizeControl(object sender, CustomizeControlEventArgs e)
        {
            this.control = e.Control as Control;

            if (this.control != null)
            {
                if (this.control is AccordionControl accordionControl)
                {
                    accordionControl.ExpandGroupOnHeaderClick = false;
                    accordionControl.GroupHeight = 40;
                    accordionControl.ItemHeight = 40;
                    accordionControl.ShowFilterControl = ShowFilterControl.Never;
                    accordionControl.ShowGroupExpandButtons = false;

                    accordionControl.ExpandAll();
                }
            }
        }
    }
}