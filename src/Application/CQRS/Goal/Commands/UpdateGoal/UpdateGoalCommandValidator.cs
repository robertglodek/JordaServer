using FluentValidation;
using Jorda.Application.Common.Interfaces;
using Jorda.Server.Application.Common.Resources.Validation.Goal;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Jorda.Application.CQRS.Goal.Commands.UpdateGoal;
public class UpdateGoalCommandValidator : AbstractValidator<UpdateGoalCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public UpdateGoalCommandValidator(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
        RuleFor(v => v.Name)
           .NotEmpty().WithMessage(CreateUpdateGoalCommandValidator.NameRequired)
           .MaximumLength(30).WithMessage(string.Format(CreateUpdateGoalCommandValidator.NameMaximumLength, 30));
        RuleFor(v => v).MustAsync(BeUniqueName).WithMessage(CreateUpdateGoalCommandValidator.MustHaveUniqueName)
            .WithName("Name");
        RuleFor(v => v.Description)
           .MaximumLength(200).WithMessage(string.Format(CreateUpdateGoalCommandValidator.DescriptionMaximumLength, 200));
    }

    public async Task<bool> BeUniqueName(UpdateGoalCommand command, CancellationToken cancellationToken)
    {
        return await _context.Goals.Where(n => (n.UserId == _currentUserService.UserId! && command.Id!=n.Id))
            .AllAsync(l => l.Name != command.Name, cancellationToken);
    }
}