using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banalyzer.Application.Helpers
{
    public static class ExceptionExtensions
    {
        public static String ToErrorMessage(this Exception ex)
        {
            var errorMessage = string.Empty;

            if (ex is DbEntityValidationException)
            {
                var exception = ex as DbEntityValidationException;
                foreach (var validationError in exception.EntityValidationErrors)
                {
                    errorMessage = validationError.ValidationErrors.Aggregate(errorMessage, (current, error) => current + ("\n" + error.ErrorMessage));
                }
            }
            else
            {
                errorMessage = ex.Message;
                while (ex.InnerException != null)
                {
                    errorMessage += "\n" + ex.InnerException.Message;
                    ex = ex.InnerException;
                }
            }
            
            return errorMessage;
        }
    }
}
