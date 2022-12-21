using FluentValidation;
using Jorda.Application.Common.Interfaces;
using Jorda.Server.Application.Common.Resources.Validation.Goal;
using Microsoft.EntityFrameworkCore;

namespace Jorda.Application.CQRS.Goal.Commands.CreateGoal;

public class CreateGoalCommandValidator : AbstractValidator<CreateGoalCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public CreateGoalCommandValidator(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
        RuleFor(v => v.Name)
            .NotEmpty().WithMessage(CreateUpdateGoalCommandValidator.NameRequired)
            .MaximumLength(30).WithMessage(string.Format(CreateUpdateGoalCommandValidator.NameMaximumLength, 30))
            .MustAsync(BeUniqueName).WithMessage(CreateUpdateGoalCommandValidator.MustHaveUniqueName);
        RuleFor(v => v.Description)
           .MaximumLength(200).WithMessage(string.Format(CreateUpdateGoalCommandValidator.DescriptionMaximumLength, 200)); 
    }

    public async Task<bool> BeUniqueName(string name, CancellationToken cancellationToken)
    {
        return await _context.Goals.Where(n=>n.UserId==_currentUserService.UserId!)
            .AllAsync(l => l.Name != name, cancellationToken);
    }
}
