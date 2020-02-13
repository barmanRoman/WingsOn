using WingsOn.WebApi.Requests;
using WingsOn.WebApi.Validators;
using Xunit;

namespace WingsOn.WebApi.Tests.ValidatorTests
{
    public class GetPassengersRequestValidatorTests
    {
        [Theory]
        [InlineData("AZ256")]
        [InlineData("az256")]
        public void ValidResult(string number)
        {
            var result = new GetPassengersRequestValidator().Validate(new GetPassengersRequest(number));
            Assert.True(result.IsValid);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void InvalidResult(string number)
        {
            var result = new GetPassengersRequestValidator().Validate(new GetPassengersRequest(number));
            Assert.False(result.IsValid);
        }
    }
}