using Accounting.Module.BusinessObjects;
using DevExpress.ExpressApp;
using System;
using System.ComponentModel;

namespace Accounting.Module.Controllers
{
    [DesignerCategory("Code")]
    public class UpdateCompanyNameController : ObjectViewController<DetailView, Company>
    {
        protected override void OnActivated()
        {
            base.OnActivated();
            ObjectSpace.Committed += ObjectSpace_Committed;
        }

        protected override void OnDeactivated()
        {
            base.OnDeactivated();
            ObjectSpace.Committed -= ObjectSpace_Committed;
        }

        private void ObjectSpace_Committed(object sender, EventArgs e)
        {
            Application.Title = ViewCurrentObject.Name;
        }
    }
}