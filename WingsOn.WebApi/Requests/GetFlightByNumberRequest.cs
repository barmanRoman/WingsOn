using MediatR;
using WingsOn.Domain;

namespace WingsOn.WebApi.Requests
{
    public class GetFlightByNumberRequest:IRequest<Flight>
    {
        public string Number { get; }

        public GetFlightByNumberRequest(string number)
        {
            Number = number;
        }
    }
}