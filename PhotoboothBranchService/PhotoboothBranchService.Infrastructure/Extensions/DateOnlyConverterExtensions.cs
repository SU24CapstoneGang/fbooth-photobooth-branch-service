using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Infrastructure.Extensions
{
    public class DateOnlyConverterExtensions : ValueConverter<DateOnly, DateTime>
    {
        public DateOnlyConverterExtensions()
       : base(dateOnly => dateOnly.ToDateTime(TimeOnly.MinValue),
           dateTime => DateOnly.FromDateTime(dateTime))
        { }
    }
}
