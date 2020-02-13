using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using WingsOn.Dal;
using WingsOn.Domain;
using WingsOn.WebApi.Exceptions;
using WingsOn.WebApi.Requests;

namespace WingsOn.WebApi.Handlers
{
    public class GetPersonRequestHandler:IRequestHandler<GetPersonRequest,Person>
    {
        private readonly PersonRepository _personRepository;
        private readonly IValidator<GetPersonRequest> _validator;

        public GetPersonRequestHandler(PersonRepository personRepository, IValidator<GetPersonRequest> validator)
        {
            _personRepository = personRepository;
            _validator = validator;
        }

        public Task<Person> Handle(GetPersonRequest request, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(request);
            var person = _personRepository.Get(request.Id);
            if (person == null)
            {
                throw new DomainObjectNotFoundException($"Person with id '{request.Id}' not found");
            }
            return Task.FromResult(person);
        }
    }
}