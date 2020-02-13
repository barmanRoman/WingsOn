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
    public class FlightsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FlightsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{number}/passengers")]
        public async Task<ActionResult<IEnumerable<Person>>> Get(string number)
        {
            return new ObjectResult(await _mediator.Send(new GetPassengersRequest(number)));
        }
    }
}