using FluentValidation;
using Jorda.Server.Application.Common.Resources.Validation.Note;
using Jorda.Server.Application.Common.Resources.Validation.ToDoItem;

namespace Jorda.Server.Application.CQRS.ToDoItem.Queries.GetToDoItems;

public class GetToDoItemsQueryValidator : AbstractValidator<GetToDoItemsQuery>
{
    private int[] allowedPageSizes = new[] { 6, 12, 18 };

    private string[] allowedSortByColumnNames = new[]
    { nameof(Domain.Entities.ToDoItem.Created),
      nameof(Domain.Entities.ToDoItem.Name),
      nameof(Domain.Entities.ToDoItem.LastModified), 
      nameof(Domain.Entities.ToDoItem.PriorityLevel)
    };


    public GetToDoItemsQueryValidator()
    {

        RuleFor(r => r.PageNumber).GreaterThanOrEqualTo(1)
            .WithMessage(string.Format(ToDoItemsQueryValidator.PageNumberGreaterThanOrEqualTo, 1));

        RuleFor(r => r.PageSize).Custom((value, context) =>
        {
            if (!allowedPageSizes.Contains(value))
                context.AddFailure("PageSize", string.Format(ToDoItemsQueryValidator.PageSizeInValues,
                    $"[{string.Join(",", allowedPageSizes)}]"));
        });

        RuleFor(r => r.SortBy)
            .Must(value => string.IsNullOrEmpty(value) || allowedSortByColumnNames.Contains(value))
            .WithMessage(string.Format(ToDoItemsQueryValidator.SortByOptionalOrInValues,
            $"[{string.Join(",", allowedSortByColumnNames)}]"));

    }

}
