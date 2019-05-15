using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTeamApp.App.UWPHelper.Extensions
{
    public static class ExpressionExtensions
    {
        internal static string GetPropertyFullName(this Expression propertyExpression)
        {
            if (propertyExpression is MemberExpression)
                return GetPropertyName(propertyExpression as MemberExpression);
            else if (propertyExpression is UnaryExpression)
                return GetPropertyName((propertyExpression as UnaryExpression).Operand as MemberExpression);
            else if (propertyExpression is LambdaExpression)
            {
                return GetPropertyFullName((propertyExpression as LambdaExpression).Body);
            }
            else
                throw new ArgumentException(string.Format("Expression: {0} is not MemberExpression", propertyExpression));
        }
        static string GetPropertyName(MemberExpression me)
        {
            string propertyName = me.Member.Name;

            if (me.Expression.NodeType != ExpressionType.Parameter
                && me.Expression.NodeType != ExpressionType.TypeAs
                && me.Expression.NodeType != ExpressionType.Constant)
            {
                propertyName = GetPropertyName(me.Expression as MemberExpression) + "." + propertyName;
            }

            return propertyName;
        }
    }
}
