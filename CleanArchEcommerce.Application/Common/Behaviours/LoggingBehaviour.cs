using CleanArchEcommerce.Application.Common.Services.Tokens;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchEcommerce.Application.Common.Behaviours
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;
        private readonly ITokenService _currentUser;

        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger, ITokenService currentUser)
        {
            _logger = logger;
            _currentUser = currentUser;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            _logger.LogInformation("User {userId} is handling {requestType}", _currentUser.UserId, typeof(TRequest).Name);
            var response = await next();
            _logger.LogInformation("Handled {requestType}", typeof(TRequest).Name);
            return response;
        }
    }

}
