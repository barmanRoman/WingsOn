using System.Threading;
using System.Threading.Tasks;
using WingsOn.Dal;
using WingsOn.Domain;
using WingsOn.WebApi.Exceptions;
using WingsOn.WebApi.Handlers;
using WingsOn.WebApi.Requests;
using Xunit;

namespace WingsOn.WebApi.Tests.HandlerTests
{
    public class GetFlightByNumberRequestHandlerTests
    {
        [Theory]
        [InlineData("AZ654")]
        [InlineData("az654")]
        public async Task Handle_ReturnFlight(string number)
        {
            var flight = new Flight {Number = number.ToUpper()};

            var flightRepository = new FlightRepository();
            flightRepository.Save(flight);

            var result =
                await new GetFlightByNumberRequestHandler(flightRepository).Handle(new GetFlightByNumberRequest(number),
                    new CancellationToken());

            Assert.Equal(flight, result);
        }

        [Fact]
        public async Task Handle_ThrowDomainObjectNotFoundException()
        {
            await Assert.ThrowsAsync<DomainObjectNotFoundException>(() =>
                new GetFlightByNumberRequestHandler(new FlightRepository()).Handle(new GetFlightByNumberRequest("TT"),
                    new CancellationToken()));
        }
    }
}