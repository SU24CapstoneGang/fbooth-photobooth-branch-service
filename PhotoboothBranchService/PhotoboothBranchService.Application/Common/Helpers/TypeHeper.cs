using AutoMapper;
using System.Reflection;

namespace PhotoboothBranchService.Application.Common.Helpers;

public static class TypeHelper
{
    public static IMappingExpression<TSource, TDest> HandleNullProperty<TSource, TDest>(
        this IMappingExpression<TSource, TDest> query)
    {
        return query.BeforeMap((src, dest) =>
        {
            if (src != null && dest != null)
            {
                foreach (PropertyInfo property in src.GetType().GetProperties())
                {
                    var srcValue = property.GetValue(src);
                    if (srcValue == null)
                    {
                        var destType = dest.GetType();
                        if (destType != null)
                        {
                            var destProp = destType.GetProperty(property.Name);
                            if (destProp != null)
                            {
                                var destValue = destProp.GetValue(dest);
                                property.SetValue(src, destValue);
                            }
                        }
                    }
                }
            }
        });
    }
}
