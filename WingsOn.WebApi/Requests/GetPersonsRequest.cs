using System.Collections.Generic;
using MediatR;
using WingsOn.Domain;

namespace WingsOn.WebApi.Requests
{
    public class GetPersonsRequest:IRequest<IEnumerable<Person>>
    {
        public IEnumerable<int> Ids { get; }

        public GetPersonsRequest(IEnumerable<int> ids)
        {
            Ids = ids;
        }
    }
}