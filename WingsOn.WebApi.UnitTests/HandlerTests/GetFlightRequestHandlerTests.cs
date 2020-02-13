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
    public class GetFlightRequestHandlerTests
    {
        [Fact]
        public async Task Handle_ReturnFlight()
        {
            var flight = new Flight {Id = 1};

            var flightRepository = new FlightRepository();
            flightRepository.Save(flight);

            var result =
                await new GetFlightRequestHandler(flightRepository).Handle(new GetFlightRequest(flight.Id),
                    new CancellationToken());

            Assert.Equal(flight, result);
        }

        [Fact]
        public async Task Handle_ThrowDomainObjectNotFoundException()
        {
            await Assert.ThrowsAsync<DomainObjectNotFoundException>(() =>
                new GetFlightRequestHandler(new FlightRepository()).Handle(new GetFlightRequest(1),
                    new CancellationToken()));
        }
    }
}