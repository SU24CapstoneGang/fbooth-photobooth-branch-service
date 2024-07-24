using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.Common.Helpers
{
    public class DictionaryValueGreaterThanZeroAttribute : ValidationAttribute
    {
        public DictionaryValueGreaterThanZeroAttribute()
            : base("All values in the dictionary must be greater than 0.")
        {
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is Dictionary<Guid, short> dictionary)
            {
                foreach (var kvp in dictionary)
                {
                    if (kvp.Value <= 0)
                    {
                        return new ValidationResult(ErrorMessage);
                    }
                }
            }
            return ValidationResult.Success;
        }
    }
}
