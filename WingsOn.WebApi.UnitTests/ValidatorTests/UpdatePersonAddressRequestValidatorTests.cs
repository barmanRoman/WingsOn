using WingsOn.WebApi.Requests;
using WingsOn.WebApi.Validators;
using Xunit;

namespace WingsOn.WebApi.Tests.ValidatorTests
{
    public class UpdatePersonAddressRequestValidatorTests
    {
        [Theory]
        [InlineData(1, "test@mail.com")]
        public void ValidResult(int id,  string address)
        {
            var result = new UpdatePersonAddressRequestValidator().Validate(new UpdatePersonAddressRequest(id, address));
            Assert.True(result.IsValid);
        }

        [Theory]
        [InlineData(0, "test@mail.com")]
        [InlineData(-1, "test@mail.com")]
        [InlineData(1, "")]
        [InlineData(1, null)]
        [InlineData(1, "test")]
        public void InvalidResult(int id, string address)
        {
            var result = new UpdatePersonAddressRequestValidator().Validate(new UpdatePersonAddressRequest(id, address));
            Assert.False(result.IsValid);
        }
    }
}