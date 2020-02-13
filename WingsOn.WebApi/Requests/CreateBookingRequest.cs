using MediatR;
using WingsOn.Domain;
using WingsOn.WebApi.Models;

namespace WingsOn.WebApi.Requests
{
    public class CreateBookingRequest:IRequest<Booking>
    {
        public CreateBookingModel Model { get; }

        public CreateBookingRequest(CreateBookingModel model)
        {
            Model = model;
        }
    }
}