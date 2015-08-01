using System.ComponentModel.DataAnnotations;

namespace MvvmCommon
{
    public class RangeAttribute : ValidationAttribute
    {
        private readonly double _minimum;
        private readonly double _maximum;
        
        public RangeAttribute(double minimum, double maximum)
        {
            _minimum = minimum;
            _maximum = maximum;
        }

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

            if (result < _minimum || result > _maximum)
            {
                return new ValidationResult(string.Format("Number must be between {0} - {1}", _minimum, _maximum));
            }

            return null;
        }
    }
}