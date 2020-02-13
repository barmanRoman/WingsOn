using System.Collections.Generic;
using MediatR;
using WingsOn.Domain;

namespace WingsOn.WebApi.Requests
{
    public class GetPassengersRequest : IRequest<IEnumerable<Person>>
    {
        public string Number { get; }

        public GetPassengersRequest(string number)
        {
            Number = number;
        }
    }
}