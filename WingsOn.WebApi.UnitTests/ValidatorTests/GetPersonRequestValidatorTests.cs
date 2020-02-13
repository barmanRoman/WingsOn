using WingsOn.WebApi.Requests;
using WingsOn.WebApi.Validators;
using Xunit;

namespace WingsOn.WebApi.Tests.ValidatorTests
{
    public class GetPersonRequestValidatorTests
    {
        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(100)]
        public void ValidResult(int id)
        {
            var result = new GetPersonRequestValidator().Validate(new GetPersonRequest(id));
            Assert.True(result.IsValid);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void InvalidResult(int id)
        {
            var result = new GetPersonRequestValidator().Validate(new GetPersonRequest(id));
            Assert.False(result.IsValid);
        }
    }
}