using Accounting.Module.BusinessObjects;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.SystemModule;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Accounting.Module.Controllers
{
    [DesignerCategory("Code")]
    public class CustomizeAccountCreationController : ObjectViewController<ObjectView, Account>
    {
        protected override void OnActivated()
        {
            base.OnActivated();

            var newObjectViewController = Frame.GetController<NewObjectViewController>();
            if (newObjectViewController != null)
            {
                newObjectViewController.CollectCreatableItemTypes += NewObjectViewController_CollectCreatableItemTypes;
                newObjectViewController.CollectDescendantTypes += NewObjectViewController_CollectDescendantTypes;

                if (newObjectViewController.Active)
                {
                    newObjectViewController.UpdateNewObjectAction();
                }
            }
        }

        protected override void OnDeactivated()
        {
            base.OnDeactivated();

            var newObjectViewController = Frame.GetController<NewObjectViewController>();
            if (newObjectViewController != null)
            {
                newObjectViewController.CollectCreatableItemTypes -= NewObjectViewController_CollectCreatableItemTypes;
                newObjectViewController.CollectDescendantTypes -= NewObjectViewController_CollectDescendantTypes;
            }
        }

        private void CustomizeList(ICollection<Type> types)
        {
            var unusableTypes =
                from type in types
                where
                    type == typeof(CustomerAccount) ||
                    type == typeof(EquityAccount) ||
                    type == typeof(PrivateAccount) ||
                    type == typeof(RoundingDifferencesAccount) ||
                    type == typeof(SupplierAccount) ||
                    type == typeof(VatPaymentAccount) ||
                    type == typeof(VatToPayAccount)
                select type;

            foreach (var unusableType in unusableTypes.ToList())
            {
                types.Remove(unusableType);
            }
        }

        private void NewObjectViewController_CollectCreatableItemTypes(object sender, CollectTypesEventArgs e)
        {
            CustomizeList(e.Types);
        }

        private void NewObjectViewController_CollectDescendantTypes(object sender, CollectTypesEventArgs e)
        {
            CustomizeList(e.Types);
        }
    }
}