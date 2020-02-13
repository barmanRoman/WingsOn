using System.Collections.Generic;
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
    public class GetPassengersRequestHandler : IRequestHandler<GetPassengersRequest, IEnumerable<Person>>
    {
        private readonly BookingRepository _bookingRepository;
        private readonly IMediator _mediator;
        private readonly IValidator<GetPassengersRequest> _validator;

        public GetPassengersRequestHandler(BookingRepository bookingRepository, IMediator mediator,
            IValidator<GetPassengersRequest> validator)
        {
            _bookingRepository = bookingRepository;
            _mediator = mediator;
            _validator = validator;
        }

        public async Task<IEnumerable<Person>> Handle(GetPassengersRequest request, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(request);
            var flight =await _mediator.Send(new GetFlightByNumberRequest(request.Number));
            return _bookingRepository.GetAll()
                .Where(b => b.Flight.Id==flight.Id)
                .SelectMany(b => b.Passengers);
        }
    }
}