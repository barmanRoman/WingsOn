using System.Collections.Generic;
using MediatR;
using WingsOn.Domain;

namespace WingsOn.WebApi.Requests
{
    public class GetPersonsByGenderTypeRequest : IRequest<IEnumerable<Person>>
    {
        public GenderType Gender;

        public GetPersonsByGenderTypeRequest(GenderType gender)
        {
            Gender = gender;
        }
    }
}