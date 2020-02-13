using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WingsOn.Dal;
using WingsOn.Domain;
using WingsOn.WebApi.Handlers;
using WingsOn.WebApi.Requests;
using Xunit;

namespace WingsOn.WebApi.Tests.HandlerTests
{
    public class GetPersonsByGenderTypeRequestHandlerTests
    {
        [Theory]
        [InlineData(GenderType.Male)]
        [InlineData(GenderType.Female)]
        public async Task Handle_ReturnMale(GenderType gender)
        {
            var personRepository = new PersonRepository();
            foreach (var id in new[] { 1, 2, 3 })
            {
                personRepository.Save(new Person{Id=id, Gender = gender});
            }

            var persons = personRepository.GetAll().Where(p => p.Gender == gender);

            var result = await
                new GetPersonsByGenderTypeRequestHandler(personRepository).Handle(new GetPersonsByGenderTypeRequest(gender),
                    new CancellationToken());

            Assert.Equal(persons, result);
        }
    }
}