using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace MvvmCommon
{
    public static class ModelValidator
    {
        public static Task<bool> ValidateModel(ViewModelWithValidation model)
        {
            var task = new Task<bool>(() =>
            {
                model.ClearErrors();

                var properties = model.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                                .Where(x => x.GetCustomAttributes<ValidationAttribute>().Any());

                foreach (var propertyInfo in properties)
                {
                    var results = new List<ValidationResult>();

                    var context = new ValidationContext(model) { MemberName = propertyInfo.Name };

                    var value = propertyInfo.GetValue(model);
                    Validator.TryValidateProperty(value, context, results);

                    if (!results.Any())
                    {
                        continue;
                    }

                    foreach (var validationResult in results)
                    {
                        model.AddError(validationResult.ErrorMessage, propertyInfo.Name);
                    }
                }

                return !model.HasErrors;
            });

            task.Start();

            return task;
        }

        public static bool ValidateProperty(ViewModelWithValidation model, [CallerMemberName] string property = null)
        {
            if (property == null)
            {
                return false;
            }

            var propertyInfo = model.GetType().GetProperty(property);

            var results = new List<ValidationResult>();

            var context = new ValidationContext(model) { MemberName = property };

            var value = propertyInfo.GetValue(model);
            Validator.TryValidateProperty(value, context, results);

            if (!results.Any())
            {
                model.RemoveError(property);
                return true;
            }

            foreach (var validationResult in results)
            {
                model.AddError(validationResult.ErrorMessage, propertyInfo.Name);
            }

            return !model.HasErrors;
        }
    }
}