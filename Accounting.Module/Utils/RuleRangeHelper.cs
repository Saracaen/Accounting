using DevExpress.Data.Filtering;
using DevExpress.Data.Filtering.Helpers;
using System;

namespace Accounting.Module.Utils
{
    public static class RuleRangeHelper
    {
        public static DateTime GetDateTimeValue(object currentObject, object value, string valueExpression)
        {
            return Convert.ToDateTime(GetValue(currentObject, value, valueExpression));
        }

        public static decimal GetDecimalValue(object currentObject, object value, string valueExpression)
        {
            return Convert.ToDecimal(GetValue(currentObject, value, valueExpression));
        }

        private static object GetValue(object currentObject, object value, string valueExpression)
        {
            if (currentObject != null && valueExpression != null)
            {
                var criteria = CriteriaOperator.Parse(valueExpression);
                var evaluatorContextDescriptor = new EvaluatorContextDescriptorDefault(currentObject.GetType());
                var expressionEvaluator = new ExpressionEvaluator(evaluatorContextDescriptor, criteria);

                return expressionEvaluator.Evaluate(currentObject);
            }
            return value;
        }
    }
}