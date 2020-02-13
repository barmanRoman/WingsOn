using MediatR;
using WingsOn.Domain;

namespace WingsOn.WebApi.Requests
{
    public class UpdatePersonAddressRequest:IRequest<Person>
    {
        public int Id { get; }
        public string Address { get; }

        public UpdatePersonAddressRequest(int id, string address)
        {
            Id = id;
            Address = address;
        }
    }
}