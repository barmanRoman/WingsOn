using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WingsOn.Dal;
using WingsOn.Domain;
using WingsOn.WebApi.Requests;

namespace WingsOn.WebApi.Handlers
{
    public class GetPersonsByGenderTypeRequestHandler:IRequestHandler<GetPersonsByGenderTypeRequest, IEnumerable<Person>>
    {
        private readonly PersonRepository _personRepository;

        public GetPersonsByGenderTypeRequestHandler(PersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public Task<IEnumerable<Person>> Handle(GetPersonsByGenderTypeRequest request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_personRepository.GetAll()
                .Where(p => p.Gender == request.Gender));
        }
    }
}