using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ParcelTracker.Common.Helpers;

public static class ValidationHelper
{
    public static ICollection<ValidationResult> GetValidationErrors(ModelStateDictionary modelState)
    {
        var errors = new List<ValidationResult>();
        foreach (var values in modelState.Values)
        {
            foreach (var error in values.Errors)
            {
                var msg = !string.IsNullOrEmpty(error.ErrorMessage) ? error.ErrorMessage
                                                        : error.Exception.ToString();
                errors.Add(new ValidationResult(msg));
            }
        }
        return errors;
    }
}