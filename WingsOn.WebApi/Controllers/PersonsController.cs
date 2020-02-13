using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WingsOn.Domain;
using WingsOn.WebApi.Requests;

namespace WingsOn.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PersonsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> Get(int id)
        {
            return new ObjectResult(await _mediator.Send(new GetPersonRequest(id)));
        }

        [HttpGet("{gender:genderType}")]
        public async Task<ActionResult<IEnumerable<Person>>> GetMale(GenderType gender)
        {
            return new ObjectResult(await _mediator.Send(new GetPersonsByGenderTypeRequest(gender)));
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<Person>> Put(int id, [FromBody]string address)
        {
            return Ok(await _mediator.Send(new UpdatePersonAddressRequest(id, address)));
        }
    }
}