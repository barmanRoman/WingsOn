using MediatR;
using WingsOn.Domain;

namespace WingsOn.WebApi.Requests
{
    public class GetPersonRequest : IRequest<Person>
    {
        public int Id { get; }

        public GetPersonRequest(int id)
        {
            Id = id;
        }
    }
}