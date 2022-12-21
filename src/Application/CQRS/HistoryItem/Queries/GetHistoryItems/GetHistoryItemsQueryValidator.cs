using FluentValidation;
using Jorda.Server.Application.Common.Resources.Validation.HistoryItem;


namespace Jorda.Server.Application.CQRS.HistoryItem.Queries.GetHistoryItems;

public class GetHistoryItemsQueryValidator : AbstractValidator<GetHistoryItemsQuery>
{
    private int[] allowedPageSizes = new[] { 6, 12, 18 };

    private string[] allowedSortByColumnNames = new[]
        { nameof(Domain.Entities.HistoryItem.Created),
          nameof(Domain.Entities.HistoryItem.Name)};


    public GetHistoryItemsQueryValidator()
    {

        RuleFor(r => r.PageNumber).GreaterThanOrEqualTo(1)
            .WithMessage(string.Format(HistoryItemsQueryValidator.PageNumberGreaterThanOrEqualTo, 1));

        RuleFor(r => r.PageSize).Custom((value, context) =>
        {
            if (!allowedPageSizes.Contains(value))
                context.AddFailure("PageSize", string.Format(HistoryItemsQueryValidator.PageSizeInValues,
                    $"[{string.Join(",", allowedPageSizes)}]"));
        });

        RuleFor(r => r.SortBy)
            .Must(value => string.IsNullOrEmpty(value) || allowedSortByColumnNames.Contains(value))
            .WithMessage(string.Format(HistoryItemsQueryValidator.SortByOptionalOrInValues,
            $"[{string.Join(",", allowedSortByColumnNames)}]"));

    }

}