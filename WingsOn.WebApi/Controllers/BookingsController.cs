using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WingsOn.Domain;
using WingsOn.WebApi.Models;
using WingsOn.WebApi.Requests;

namespace WingsOn.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BookingsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Booking>> Post(CreateBookingModel model)
        {
            return Ok(await _mediator.Send(new CreateBookingRequest(model)));
        }
    }
}