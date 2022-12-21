using FluentValidation;
using Jorda.Server.Application.Common.Resources.Validation.Note;

namespace Jorda.Server.Application.CQRS.Note.Queries.GetNotes;

public class GetNotesQueryValidator : AbstractValidator<GetNotesQuery>
{
    private int[] allowedPageSizes = new[] { 6, 12, 18 };

    private string[] allowedSortByColumnNames = new[]
        { nameof(Domain.Entities.Note.Created),
          nameof(Domain.Entities.Note.Name),
          nameof(Domain.Entities.Note.LastModified) };


    public GetNotesQueryValidator()
    {

        RuleFor(r => r.PageNumber).GreaterThanOrEqualTo(1)
            .WithMessage(string.Format(NotesQueryValidator.PageNumberGreaterThanOrEqualTo,1));

        RuleFor(r => r.PageSize).Custom((value, context) =>
        {
            if (!allowedPageSizes.Contains(value))
                context.AddFailure("PageSize", string.Format(NotesQueryValidator.PageSizeInValues, 
                    $"[{string.Join(",", allowedPageSizes)}]"));
        });

        RuleFor(r => r.SortBy)
            .Must(value => string.IsNullOrEmpty(value) || allowedSortByColumnNames.Contains(value))
            .WithMessage(string.Format(NotesQueryValidator.SortByOptionalOrInValues,
            $"[{string.Join(",", allowedSortByColumnNames)}]"));

    }
   
}
