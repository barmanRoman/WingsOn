using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Moq;
using WingsOn.Dal;
using WingsOn.Domain;
using WingsOn.WebApi.Handlers;
using WingsOn.WebApi.Requests;
using WingsOn.WebApi.Tests.Implementations;
using Xunit;

namespace WingsOn.WebApi.Tests.HandlerTests
{
    public class GetPassengersRequestHandlerTests
    {
        [Fact]
        public async Task Handle_ReturnPassengers()
        {
            var number = "AZ587";
            var booking = new Booking
            {
                Id = 1,
                Flight = new Flight
                {
                    Id = 100
                },
                Passengers = new List<Person>
                {
                    new Person {Id = 5}
                }
            };

            var bookingRepository = new BookingRepository();
            bookingRepository.Save(booking);

            var mock = new Mock<IMediator>();
            mock.Setup(m => m.Send(It.Is<GetFlightByNumberRequest>(request => request.Number.Equals(number)),
                    default(CancellationToken)))
                .ReturnsAsync(() => booking.Flight);

            var result =
                await new GetPassengersRequestHandler(bookingRepository, mock.Object,
                    new TestValidator<GetPassengersRequest>()).Handle(new GetPassengersRequest(number),
                    new CancellationToken());

            Assert.Equal(booking.Passengers, result);

            mock.Verify(m => m.Send(It.IsAny<GetFlightByNumberRequest>(), default(CancellationToken)), Times.Once);
        }

        [Fact]
        public async Task Handle_ReturnNothing()
        {
            var number = "AZ587";
            var mock = new Mock<IMediator>();
            mock.Setup(m => m.Send(It.Is<GetFlightByNumberRequest>(request => request.Number.Equals(number)),
                    default(CancellationToken)))
                .ReturnsAsync(() => new Flight {Id = 1});

            var result =
                await new GetPassengersRequestHandler(new BookingRepository(), mock.Object,
                    new TestValidator<GetPassengersRequest>()).Handle(new GetPassengersRequest(number),
                    new CancellationToken());

            Assert.False(result.Any());

            mock.Verify(m => m.Send(It.IsAny<GetFlightByNumberRequest>(), default(CancellationToken)), Times.Once);
        }
    }
}