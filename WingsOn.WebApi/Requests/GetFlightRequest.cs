using MediatR;
using WingsOn.Domain;

namespace WingsOn.WebApi.Requests
{
    public class GetFlightRequest:IRequest<Flight>
    {
        public int Id { get; }

        public GetFlightRequest(int id)
        {
            Id = id;
        }
    }
}