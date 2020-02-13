using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using WingsOn.Dal;
using WingsOn.Domain;
using WingsOn.WebApi.Requests;

namespace WingsOn.WebApi.Handlers
{
    public class CreateBookingRequestHandler:IRequestHandler<CreateBookingRequest,Booking>
    {
        private readonly BookingRepository _bookingRepository;
        private readonly IMediator _mediator;
        private readonly IValidator<CreateBookingRequest> _validator;

        public CreateBookingRequestHandler(BookingRepository bookingRepository, IMediator mediator,
            IValidator<CreateBookingRequest> validator)
        {
            _bookingRepository = bookingRepository;
            _mediator = mediator;
            _validator = validator;
        }

        public async Task<Booking> Handle(CreateBookingRequest request, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(request);
            var booking=new Booking
            {
                Id = _bookingRepository.GetAll().Max(b=>b.Id)+1,
                Number = request.Model.Number.ToUpper(),
                DateBooking = DateTime.Now,
                Flight = await _mediator.Send(new GetFlightRequest(request.Model.Flight)),
                Customer = await _mediator.Send(new GetPersonRequest(request.Model.Customer)),
                Passengers = await _mediator.Send(new GetPersonsRequest(request.Model.Passengers))
            };

            _bookingRepository.Save(booking);

            return booking;
        }
    }
}