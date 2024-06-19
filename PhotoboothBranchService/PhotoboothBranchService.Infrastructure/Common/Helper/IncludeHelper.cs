using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Infrastructure.Common.Helper
{
    public static class IncludeHelper
    {
        public static bool IsValidInclude<TEntity>(Expression<Func<TEntity, object>> includeProperty) where TEntity : class
        {
            if (includeProperty == null)
            {
                return false;
            }

            MemberExpression memberExpression = null;
            if (includeProperty.Body.NodeType == ExpressionType.Convert)
            {
                memberExpression = ((UnaryExpression)includeProperty.Body).Operand as MemberExpression;
            }
            else if (includeProperty.Body.NodeType == ExpressionType.MemberAccess)
            {
                memberExpression = includeProperty.Body as MemberExpression;
            }

            if (memberExpression == null)
            {
                return false;
            }

            string propertyName = memberExpression.Member.Name;
            var entityType = typeof(TEntity);
            var property = entityType.GetProperty(propertyName);

            return property != null;
        }
    }

}
