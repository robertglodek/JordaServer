﻿using FluentValidation.Results;
namespace Jorda.Application.Common.Exceptions;

public class ValidationException : Exception
{
    public ValidationException(IEnumerable<ValidationFailure> failures)
        : base("One or more validation failures have occurred.")
    {  
        Errors = failures
            .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
            .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
    }
    public IDictionary<string, string[]> Errors { get; }
}
