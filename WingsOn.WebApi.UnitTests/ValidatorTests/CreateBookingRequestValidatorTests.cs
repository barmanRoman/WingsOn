using System.Collections.Generic;
using WingsOn.WebApi.Models;
using WingsOn.WebApi.Requests;
using WingsOn.WebApi.Validators;
using Xunit;

namespace WingsOn.WebApi.Tests.ValidatorTests
{
    public class CreateBookingRequestValidatorTests
    {
        [Theory]
        [InlineData(1,1,"AZ-256",new []{1,2})]
        public void ValidResult(int customer, int flight, string number, IEnumerable<int> passengers)
        {
            var result = new CreateBookingRequestValidator().Validate(new CreateBookingRequest(new CreateBookingModel
                {Customer = customer, Flight = flight, Number = number, Passengers = passengers}));
            Assert.True(result.IsValid);
        }

        [Theory]
        [InlineData(0, 1, "AZ-256", new []{1,2})]
        [InlineData(-1, 1, "AZ-256", new[] { 1, 2 })]
        [InlineData(1, 0, "AZ-256", new[] { 1, 2 })]
        [InlineData(1, -1, "AZ-256", new[] { 1, 2 })]
        [InlineData(1, 1, "", new[] { 1, 2 })]
        [InlineData(1, 1, null, new[] { 1, 2 })]
        [InlineData(1, 1, "AZ-256", new[] { 0, 2 })]
        [InlineData(1, 1, "AZ-256", new[] { -1, 2 })]
        [InlineData(1, 1, "AZ-256", new[] { 1, 0 })]
        [InlineData(1, 1, "AZ-256", new[] { 1, -1 })]
        [InlineData(1, 1, "AZ-256", new int[0])]
        public void InvalidResult(int customer, int flight, string number, IEnumerable<int> passengers)
        {
            var result = new CreateBookingRequestValidator().Validate(new CreateBookingRequest(new CreateBookingModel
                { Customer = customer, Flight = flight, Number = number, Passengers = passengers }));
            Assert.False(result.IsValid);
        }
    }
}