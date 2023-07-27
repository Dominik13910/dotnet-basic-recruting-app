using FluentValidation;
using MatchDataManager.DataBase.Models.Paination;

namespace MatchDataManager.Application.Validators
{
    public class QueryValidator : AbstractValidator<Query>
    {
        private int[] allowdPageSize = new[] { 5, 10, 15 };
        public QueryValidator()
        {
            RuleFor(q => q.PageNumber).GreaterThanOrEqualTo(1);
            RuleFor(q => q.PageSize).Custom((value, context) =>
            {
                if (!allowdPageSize.Contains(value))
                {
                    context.AddFailure("PageSize", $"PageSize Must in [{string.Join(",", allowdPageSize)}]");
                }
            });
        }
    }
}