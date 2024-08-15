using AutoMapper;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using PhotoboothBranchService.Application.DTOs;
using System.Reflection;
using System.Text.RegularExpressions;

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

    public static (UserNameInputType, string) DetectAndFormatInput(string input)
    {
        string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        string phonePattern = @"^(0|\+84)[0-9]{9}$";

        if (Regex.IsMatch(input, emailPattern))
        {
            return (UserNameInputType.Email, input);
        }
        else if (Regex.IsMatch(input, phonePattern))
        {
            if (input.StartsWith("+84"))
            {
                input = "0" + input.Substring(3);
            }
            return (UserNameInputType.PhoneNumber, input);
        }
        else
        {
            return (UserNameInputType.Unknown, input);
        }
    }
}

