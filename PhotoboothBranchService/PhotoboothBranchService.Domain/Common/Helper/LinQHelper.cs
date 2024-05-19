using System.Reflection;

namespace Beanbox.Business.Commons.Helpers
{
    public static class LinQHelper
    {
        public static IEnumerable<TEntity>? AutoFilter<TEntity, TFilter>(this IEnumerable<TEntity> query, TFilter filterObject)
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
