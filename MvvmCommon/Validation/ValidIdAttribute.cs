using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmCommon.Validation
{
    public class ValidIdAttribute : ValidationAttribute
    {
        public override bool RequiresValidationContext
        {
            get { return true; }
        }


        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var member = validationContext.MemberName;
            var propertyInfo = validationContext.ObjectType.GetProperty(member);
            if (propertyInfo == null)
            {
                return null;
            }

            var propertyValue = propertyInfo.GetValue(validationContext.ObjectInstance);
            if (propertyValue == null)
            {
                return null;
            }

            double result;
            if (!double.TryParse(propertyValue.ToString(), out result))
            {
                return new ValidationResult("Cannot convert value to number");
            }

            return result < 1 ? new ValidationResult(string.Format("Id must be greater than 1")) : null;
        }
    }
}
