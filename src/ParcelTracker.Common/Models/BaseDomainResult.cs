using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace ParcelTracker.Common.Models;

public class BaseDomainResult<T>
{
    public T Result { get; set; }
    public IEnumerable<T> ResultList { get; set; }

    public ICollection<ValidationResult> Errors { get; set; }

    public BaseDomainResult(T obj)
    {
        Result = obj;
    }

    public BaseDomainResult(IEnumerable<T> obj)
    {
        ResultList = obj;
    }

    public BaseDomainResult(ICollection<ValidationResult> results)
    {
        Errors = results;
    }

    public BaseDomainResult(ValidationResult result)
    {
        Errors = new Collection<ValidationResult>
        {
            result
        };
    }

    public BaseDomainResult(T obj, ValidationResult result)
    {
        Result = obj;
        Errors = new Collection<ValidationResult>
        {
            result
        };
    }

    public BaseDomainResult(T obj, IEnumerable<ValidationResult> results)
    {
        Result = obj;
        Errors = results?.ToList();
    }

    public BaseDomainResult(string errorMsg)
    {
        Errors = new Collection<ValidationResult>
        {
            new ValidationResult(errorMsg)
        };
    }
}