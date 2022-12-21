using FluentValidation;
using Jorda.Application.Common.Interfaces;
using Jorda.Server.Application.Common.Resources.Validation.Goal;
using Jorda.Server.Application.Common.Resources.Validation.Label;
using Jorda.Server.Application.Common.Resources.Validation.Note;
using Microsoft.EntityFrameworkCore;

namespace Jorda.Server.Application.CQRS.Label.Commands.CreateLabel;

public class CreateLabelCommandValidator : AbstractValidator<CreateLabelCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public CreateLabelCommandValidator(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;

        RuleFor(v => v.Name)
            .NotEmpty().WithMessage(CreateUpdateLabelCommandValidator.NameRequired)
            .MaximumLength(30).WithMessage(string.Format(CreateUpdateLabelCommandValidator.NameMaximumLength, 30))
        .MustAsync(BeUniqueName).WithMessage(CreateUpdateLabelCommandValidator.MustHaveUniqueName);
       
    }

    public async Task<bool> BeUniqueName(string name, CancellationToken cancellationToken)
    {
        return await _context.Labels.Where(n => n.UserId == Guid.Parse(_currentUserService.UserId!))
            .AllAsync(l => l.Name != name, cancellationToken);
    }
}
