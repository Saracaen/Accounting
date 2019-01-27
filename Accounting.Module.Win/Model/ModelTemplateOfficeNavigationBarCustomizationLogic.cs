using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Win.SystemModule;

namespace Accounting.Module.Win.Model
{
    [DomainLogic(typeof(IModelTemplateOfficeNavigationBarCustomization))]
    public static class ModelTemplateOfficeNavigationBarCustomizationLogic
    {
        public static bool Get_Compact(IModelTemplateOfficeNavigationBarCustomization instance)
        {
            return false;
        }
    }
}