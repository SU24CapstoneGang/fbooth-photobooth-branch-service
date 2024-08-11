using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.Common.Helpers
{
    public class TimeSpanValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is TimeSpan timeSpan)
            {
                if (timeSpan >= TimeSpan.Zero && timeSpan < TimeSpan.FromDays(1))
                {
                    if (timeSpan.Minutes % 15 == 0)
                    {
                        return ValidationResult.Success;
                    }
                    else
                    {
                        return new ValidationResult("The time must be on the hour (xx:00), half-hour (xx:30), or in 15-minute increments (xx:15, xx:45).");
                    }
                }
            }
            return new ValidationResult("The time must be a valid TimeSpan in 'hh:mm' format and less than 24 hours.");
        }
    }
}
