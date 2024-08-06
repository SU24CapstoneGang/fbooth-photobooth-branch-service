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
                    return ValidationResult.Success;
                }
            }
            return new ValidationResult("The time must be a valid TimeSpan in 'hh:mm' format and less than 24 hours.");
        }
    }
}
