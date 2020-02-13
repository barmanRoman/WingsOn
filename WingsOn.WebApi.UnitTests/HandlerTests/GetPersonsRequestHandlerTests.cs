using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WingsOn.Dal;
using WingsOn.Domain;
using WingsOn.WebApi.Exceptions;
using WingsOn.WebApi.Handlers;
using WingsOn.WebApi.Requests;
using Xunit;

namespace WingsOn.WebApi.Tests.HandlerTests
{
    public class GetPersonsRequestHandlerTests
    {
        [Fact]
        public async Task Handle_ReturnPersons()
        {
            var persons = new[] {new Person {Id = 1}, new Person {Id = 2}};

            var personRepository = new PersonRepository();
            foreach (var person in persons)
            {
                personRepository.Save(person);
            }

            var result =
                await new GetPersonsRequestHandler(personRepository).Handle(
                    new GetPersonsRequest(persons.Select(p => p.Id)), new CancellationToken());

            Assert.Equal(persons,result);
        }

        [Fact]
        public async Task Handle_ThrowDomainObjectNotFoundException()
        {
            await Assert.ThrowsAsync<DomainObjectNotFoundException>(() =>
                new GetPersonsRequestHandler(new PersonRepository()).Handle(
                    new GetPersonsRequest(new []{1}), new CancellationToken()));
        }
    }
}