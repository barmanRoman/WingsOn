using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Moq;
using WingsOn.Dal;
using WingsOn.Domain;
using WingsOn.WebApi.Handlers;
using WingsOn.WebApi.Models;
using WingsOn.WebApi.Requests;
using WingsOn.WebApi.Tests.Implementations;
using Xunit;

namespace WingsOn.WebApi.Tests.HandlerTests
{
    public class CreateBookingRequestHandlerTests
    {
        [Fact]
        public async Task Handle_ReturnBooking()
        {
            var bookingModel = new CreateBookingModel
                {Customer = 1, Number = "WO-291471", Flight = 1, Passengers = new[] {1}};

            var mock = new Mock<IMediator>();
            mock.Setup(m => m.Send(It.Is<GetFlightRequest>(request => request.Id == bookingModel.Flight),
                    default(CancellationToken)))
                .ReturnsAsync(new Flight {Id = bookingModel.Flight});
            mock.Setup(m => m.Send(It.Is<GetPersonRequest>(request => request.Id == bookingModel.Customer),
                    default(CancellationToken)))
                .ReturnsAsync(new Person {Id = bookingModel.Customer});
            mock.Setup(m =>
                    m.Send(
                        It.Is<GetPersonsRequest>(request =>
                            request.Ids.Count() == bookingModel.Passengers.Count() &&
                            request.Ids.All(id => bookingModel.Passengers.Contains(id))), default(CancellationToken)))
                .ReturnsAsync(bookingModel.Passengers.Select(p => new Person {Id = p}));


            var result = await new CreateBookingRequestHandler(new BookingRepository(), mock.Object,
                    new TestValidator<CreateBookingRequest>())
                .Handle(new CreateBookingRequest(bookingModel), new CancellationToken());

            Assert.Equal(bookingModel.Customer,result.Customer.Id);
            Assert.Equal(bookingModel.Number,result.Number, ignoreCase: true);
            Assert.Equal(bookingModel.Flight,result.Flight.Id);
            Assert.Equal(bookingModel.Passengers,result.Passengers.Select(p=>p.Id));

            mock.Verify(m => m.Send(It.IsAny<GetFlightRequest>(), default(CancellationToken)),Times.Once);
            mock.Verify(m => m.Send(It.IsAny<GetPersonRequest>(), default(CancellationToken)), Times.Once);
            mock.Verify(m => m.Send(It.IsAny<GetPersonsRequest>(), default(CancellationToken)), Times.Once);
        }
    }
}