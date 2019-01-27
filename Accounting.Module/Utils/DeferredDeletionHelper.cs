using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;
using System;

namespace Accounting.Module.Utils
{
    public static class DeferredDeletionHelper
    {
        public static void DisableDeferredDeletion(XPDictionary dictionary)
        {
            if (dictionary == null)
                throw new ArgumentNullException(nameof(dictionary));

            dictionary.ClassInfoChanged += XPDictionary_ClassInfoChanged;
        }

        public static void DisableDeferredDeletion()
        {
            DisableDeferredDeletion(XpoTypesInfoHelper.GetXpoTypeInfoSource().XPDictionary);
        }

        private static void XPDictionary_ClassInfoChanged(object sender, ClassInfoEventArgs e)
        {
            if (e.ClassInfo.ClassType == typeof(XPCustomObject))
            {
                var dictionary = (XPDictionary)sender;
                dictionary.ClassInfoChanged -= XPDictionary_ClassInfoChanged;

                var attribute = (DeferredDeletionAttribute)e.ClassInfo.GetAttributeInfo(typeof(DeferredDeletionAttribute));
                attribute.Enabled = false;
            }
        }
    }
}