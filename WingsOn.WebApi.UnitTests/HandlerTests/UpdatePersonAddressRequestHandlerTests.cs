using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Moq;
using WingsOn.Dal;
using WingsOn.Domain;
using WingsOn.WebApi.Handlers;
using WingsOn.WebApi.Requests;
using WingsOn.WebApi.Tests.Implementations;
using Xunit;

namespace WingsOn.WebApi.Tests.HandlerTests
{
    public class UpdatePersonAddressRequestHandlerTests
    {
        [Fact]
        public async Task Handle_ReturnPerson()
        {
            var person = new Person {Id = 1, Address = "Test"};
            var newAddress = "Test test";

            var personRepository = new PersonRepository();
            personRepository.Save(person);

            var mock = new Mock<IMediator>();
            mock.Setup(m =>
                    m.Send(It.Is<GetPersonRequest>(request => request.Id == person.Id), default(CancellationToken)))
                .ReturnsAsync(person);

            var result = await 
                new UpdatePersonAddressRequestHandler(mock.Object, personRepository,
                        new TestValidator<UpdatePersonAddressRequest>())
                    .Handle(new UpdatePersonAddressRequest(person.Id, newAddress), new CancellationToken());

            Assert.Equal(newAddress,result.Address,ignoreCase:false);
            
            mock.Verify(m=>m.Send(It.IsAny<GetPersonRequest>(),default(CancellationToken)),Times.Once);
        }
    }
}