using Accounting.Module.BusinessObjects;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Accounting.Module.Controllers
{
    [DesignerCategory("Code")]
    public class GenerateIdentifierController : ObjectViewController<ObjectView, ISupportIdentifier>
    {
        protected override void OnActivated()
        {
            base.OnActivated();
            ObjectSpace.Committing += ObjectSpace_Committing;
        }

        protected override void OnDeactivated()
        {
            base.OnDeactivated();
            ObjectSpace.Committing -= ObjectSpace_Committing;
        }

        private void ObjectSpace_Committing(object sender, CancelEventArgs e)
        {
            var objectsToSave = ObjectSpace.GetObjectsToSave(false).OfType<ISupportIdentifier>().ToList();
            var session = ((XPObjectSpace)ObjectSpace).Session;

            foreach (var objectToSave in objectsToSave.Where(x => string.IsNullOrEmpty(x.Identifier)))
            {
                var identifier = ObjectSpace.FindObject<Identifier>(new BinaryOperator("TargetType", objectToSave.GetType().FullName));
                var numbers = new List<int>();
                var view = new XPView(session, objectToSave.GetType());

                if (identifier.Prefix != null)
                {
                    view.Criteria = CriteriaOperator.Parse("StartsWith(Identifier, ?)", identifier.Prefix);
                }
                view.AddProperty("Identifier");

                foreach (ViewRecord record in view)
                {
                    var identifierString = (string)record["Identifier"];
                    var identifierPrefixLength = identifier.Prefix?.Length ?? 0;
                    var identifierNumber = identifierString.Substring(identifierPrefixLength, identifierString.Length - identifierPrefixLength);

                    if (int.TryParse(identifierNumber, out var number))
                    {
                        numbers.Add(number);
                    }
                }

                var availableNumber = Enumerable.Range(identifier.StartValue, int.MaxValue).Except(numbers).FirstOrDefault();
                objectToSave.Identifier = $"{identifier.Prefix}{availableNumber.ToString($"D{identifier.Decimals}")}";
            }
        }
    }
}