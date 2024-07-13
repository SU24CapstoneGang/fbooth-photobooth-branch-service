using System.Linq.Expressions;
using System.Reflection;

namespace PhotoboothBranchService.Domain.Common.Helper
{
    public static class LinQHelper
    {
        public static List<TEntity>? AutoFilter<TEntity, TFilter>(this List<TEntity> query, TFilter filterObject)
        {
            Func<TEntity, TFilter, bool> isContain = (TEntity e, TFilter filterObject) =>
            {
                bool result = true;
                if (filterObject != null)
                {
                    foreach (PropertyInfo property in filterObject.GetType().GetProperties())
                    {
                        if (!property.Name.Contains("Expression"))
                        {
                            var filterValue = property.GetValue(filterObject);
                            if (e != null)
                            {
                                var objectType = e.GetType();
                                if (objectType != null)
                                {
                                    var objectProp = e.GetType().GetProperty(property.Name);
                                    if (objectProp != null)
                                    {
                                        var value = objectProp.GetValue(e);
                                        if ((value != null) && (filterValue != null))
                                        {
                                            var valueString = value.ToString();
                                            var filterValueString = filterValue.ToString();
                                            if ((filterValueString != null) && (valueString != null))
                                            {
                                                var expression = filterObject.GetType().GetMethod(property.Name + "Expression");
                                                if (expression != null)
                                                {
                                                    var expressionResult = expression.Invoke(filterObject, new[] { valueString, filterValueString });
                                                    if (expressionResult != null)
                                                    {
                                                        result &= (bool)expressionResult;
                                                    }
                                                }
                                                else
                                                {
                                                    if (property.PropertyType == typeof(int))
                                                    {
                                                        result &= valueString.Equals(filterValueString);
                                                    }
                                                    else
                                                        result &= valueString.Contains(filterValueString);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                return result;
            };

            return query.Where(e => isContain(e, filterObject)).ToList();
        }
        public static Expression<Func<T, bool>> AndAlso<T>(Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {
            var parameter = Expression.Parameter(typeof(T));
            var body = Expression.AndAlso(
                Expression.Invoke(expr1, parameter),
                Expression.Invoke(expr2, parameter)
            );
            return Expression.Lambda<Func<T, bool>>(body, parameter);
        }
        public static IQueryable<TEntity> AutoPaging<TEntity>(this IQueryable<TEntity> query, int pagingSize, int pagingIndex)
        {
            if (pagingIndex <= 0)
            {
                return query;
            }
            return query.Skip((pagingIndex - 1) * pagingSize).Take(pagingSize);
        }
    }
}
