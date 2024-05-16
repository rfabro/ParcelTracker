using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace ParcelTracker.Common.Models;

public class BaseResult<T>
{
    public T Result { get; set; }
    public IEnumerable<T> Results { get; set; }
    public ICollection<ValidationResult> Errors { get; set; }

    public BaseResult(T obj)
    {
        Result = obj;
    }

    public BaseResult(IEnumerable<T> obj)
    {
        Results = obj;
    }

    public BaseResult(ICollection<ValidationResult> results)
    {
        Errors = results;
    }

    public BaseResult(ValidationResult result)
    {
        Errors = new Collection<ValidationResult>
        {
            result
        };
    }

    public BaseResult(T obj, ValidationResult result)
    {
        Result = obj;
        Errors = new Collection<ValidationResult>
        {
            result
        };
    }

    public BaseResult(T obj, IEnumerable<ValidationResult> results)
    {
        Result = obj;
        Errors = results?.ToList();
    }

    public BaseResult(string errorMsg)
    {
        Errors = new Collection<ValidationResult>
        {
            new ValidationResult(errorMsg)
        };
    }
}