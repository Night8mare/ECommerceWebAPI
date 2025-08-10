using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchEcommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class ApiControllerBase : ControllerBase
    {
        private ISender _mediator;
        protected ISender Mediator => _mediator;
        protected ApiControllerBase(ISender mediator)
        {
            _mediator = mediator;
        }
    }
}
