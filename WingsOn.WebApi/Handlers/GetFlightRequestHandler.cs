using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WingsOn.Dal;
using WingsOn.Domain;
using WingsOn.WebApi.Exceptions;
using WingsOn.WebApi.Requests;

namespace WingsOn.WebApi.Handlers
{
    public class GetFlightRequestHandler:IRequestHandler<GetFlightRequest,Flight>
    {
        private readonly FlightRepository _flightRepository;

        public GetFlightRequestHandler(FlightRepository flightRepository)
        {
            _flightRepository = flightRepository;
        }

        public Task<Flight> Handle(GetFlightRequest request, CancellationToken cancellationToken)
        {
            var flight = _flightRepository.Get(request.Id);
            if (flight == null)
            {
                throw new DomainObjectNotFoundException($"Flight with id '{request.Id}' not found");
            }
            return Task.FromResult(flight);
        }
    }
}