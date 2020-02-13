using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using WingsOn.Dal;
using WingsOn.Domain;
using WingsOn.WebApi.Requests;

namespace WingsOn.WebApi.Handlers
{
    public class UpdatePersonAddressRequestHandler:IRequestHandler<UpdatePersonAddressRequest,Person>
    {
        private readonly IMediator _mediator;
        private readonly IValidator<UpdatePersonAddressRequest> _validator;
        private readonly PersonRepository _personRepository;

        public UpdatePersonAddressRequestHandler(IMediator mediator, PersonRepository personRepository,
            IValidator<UpdatePersonAddressRequest> validator)
        {
            _mediator = mediator;
            _personRepository = personRepository;
            _validator = validator;
        }
        public async Task<Person> Handle(UpdatePersonAddressRequest request, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(request);
            var person = await _mediator.Send(new GetPersonRequest(request.Id));
            person.Address = request.Address;

            _personRepository.Save(person);

            return person;
        }
    }
}