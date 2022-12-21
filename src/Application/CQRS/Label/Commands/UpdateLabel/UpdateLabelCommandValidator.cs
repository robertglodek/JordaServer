using FluentValidation;
using Jorda.Application.Common.Interfaces;
using Jorda.Application.CQRS.Goal.Commands.UpdateGoal;
using Jorda.Server.Application.Common.Resources.Validation.Goal;
using Jorda.Server.Application.Common.Resources.Validation.Label;
using Jorda.Server.Application.CQRS.Label.Commands.CreateLabel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jorda.Server.Application.CQRS.Label.Commands.UpdateLabel
{
    public class UpdateLabelCommandValidator : AbstractValidator<UpdateLabelCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;

        public UpdateLabelCommandValidator(IApplicationDbContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;

            RuleFor(v => v.Name)
                .NotEmpty().WithMessage(CreateUpdateLabelCommandValidator.NameRequired)
                .MaximumLength(30).WithMessage(string.Format(CreateUpdateLabelCommandValidator.NameMaximumLength, 30));
             RuleFor(v => v).MustAsync(BeUniqueName).WithMessage(CreateUpdateGoalCommandValidator.MustHaveUniqueName)
            .WithName("Name");

        }

        public async Task<bool> BeUniqueName(UpdateLabelCommand command, CancellationToken cancellationToken)
        {
            return await _context.Goals.Where(n => (n.UserId == _currentUserService.UserId! && command.Id != n.Id))
                .AllAsync(l => l.Name != command.Name, cancellationToken);
        }
    }
}
