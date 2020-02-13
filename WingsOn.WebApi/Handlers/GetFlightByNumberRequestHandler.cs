using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WingsOn.Dal;
using WingsOn.Domain;
using WingsOn.WebApi.Exceptions;
using WingsOn.WebApi.Requests;

namespace WingsOn.WebApi.Handlers
{
    public class GetFlightByNumberRequestHandler : IRequestHandler<GetFlightByNumberRequest, Flight>
    {
        private readonly FlightRepository _flightRepository;

        public GetFlightByNumberRequestHandler(FlightRepository flightRepository)
        {
            _flightRepository = flightRepository;
        }

        public Task<Flight> Handle(GetFlightByNumberRequest request, CancellationToken cancellationToken)
        {
            var flight = _flightRepository.GetAll()
                .SingleOrDefault(f => f.Number.Equals(request.Number, StringComparison.OrdinalIgnoreCase));
            if (flight==null)
            {
                throw new DomainObjectNotFoundException($"Flight with number '{request.Number}' not found");
            }

            return Task.FromResult(flight);
        }
    }
}