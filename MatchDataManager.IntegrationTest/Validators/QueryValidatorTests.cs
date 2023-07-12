using MatchDataManager.Api.Models.Paination;
using MatchDataManager.Api.Validators;

namespace MatchDataManager.IntegrationTest.Validators
{
    public class QueryValidatorTests
    {
        [Fact]
        public void Validate_ForCorrectModel_ReturnSuccess()
        {
            var validator = new QueryValidator();
            var model = new Query()
            {
                PageNumber = 1,
                PageSize = 10,
            };

        var result = validator.Validate(model);
        }
    }
}
