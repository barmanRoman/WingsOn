using System.Collections.Generic;
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
    public class GetPersonsRequestHandler:IRequestHandler<GetPersonsRequest,IEnumerable<Person>>
    {
        private readonly PersonRepository _personRepository;

        public GetPersonsRequestHandler(PersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public Task<IEnumerable<Person>> Handle(GetPersonsRequest request, CancellationToken cancellationToken)
        {
            var persons = _personRepository.GetAll().Where(p => request.Ids.Contains(p.Id));
            if (persons.Count() == request.Ids.Count())
            {
                return Task.FromResult(persons);
            }

            var missingPersons = GetMissingPersonsId(persons, request.Ids);
            if (missingPersons.Count() == 1)
            {
                throw new DomainObjectNotFoundException($"Person with id '{missingPersons.Single()}' not found");
            }
            else
            {
                throw new DomainObjectNotFoundException($"Persons with id '{string.Join(',',missingPersons)}' not found");
            }
        }

        private IEnumerable<int> GetMissingPersonsId(IEnumerable<Person> persons, IEnumerable<int> personsId)
        {
            return personsId.Where(id => persons.All(p => p.Id != id)).ToList();
        }
    }
}