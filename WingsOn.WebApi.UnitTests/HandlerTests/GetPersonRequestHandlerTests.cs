using System.Threading;
using System.Threading.Tasks;
using WingsOn.Dal;
using WingsOn.Domain;
using WingsOn.WebApi.Exceptions;
using WingsOn.WebApi.Handlers;
using WingsOn.WebApi.Requests;
using WingsOn.WebApi.Tests.Implementations;
using Xunit;

namespace WingsOn.WebApi.Tests.HandlerTests
{
    public class GetPersonRequestHandlerTests
    {
        [Fact]
        public async Task Handle_ReturnPerson()
        {
            var person = new Person { Id = 1 };

            var personRepository = new PersonRepository();
            personRepository.Save(person);

            var result =
                await new GetPersonRequestHandler(personRepository, new TestValidator<GetPersonRequest>()).Handle(
                    new GetPersonRequest(person.Id), new CancellationToken());

            Assert.Equal(person, result);
        }

        [Fact]
        public async Task Handle_ThrowDomainObjectNotFoundException()
        {
            await Assert.ThrowsAsync<DomainObjectNotFoundException>(() =>
                new GetPersonRequestHandler(new PersonRepository(), new TestValidator<GetPersonRequest>()).Handle(
                    new GetPersonRequest(1), new CancellationToken()));
        }
    }
}