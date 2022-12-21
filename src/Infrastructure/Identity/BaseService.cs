using FluentValidation;

namespace Jorda.Server.Infrastructure.Identity;

public abstract class BaseService
{
    public async Task ValidateAsync<TRequest>(IEnumerable<IValidator<TRequest>> validators,TRequest request) 
    {
        if (validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);
            var validationResults = await Task.WhenAll(
            validators.Select(v =>
                    v.ValidateAsync(context)));
            var failures = validationResults
                .Where(r => r.Errors.Any())
                .SelectMany(r => r.Errors)
                .ToList();
            if (failures.Any())
                throw new Jorda.Application.Common.Exceptions.ValidationException(failures);
        }
    }
}
