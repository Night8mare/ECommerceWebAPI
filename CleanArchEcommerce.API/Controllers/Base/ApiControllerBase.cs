using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchEcommerce.API.Controllers.Base
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class ApiControllerBase : ControllerBase
    {
        private ISender _mediator;
        private readonly IHttpContextAccessor _token;
        protected ISender Mediator => _mediator;
        protected IHttpContextAccessor Token => _token;
        protected ApiControllerBase(ISender mediator, IHttpContextAccessor token)
        {
            _mediator = mediator;
            _token = token;
        }
    }
}
