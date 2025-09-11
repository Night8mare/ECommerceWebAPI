using CleanArchEcommerce.Application.Common.Services.Tokens;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchEcommerce.Application.Common.Behaviours
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoggingBehavior(
            ILogger<LoggingBehavior<TRequest, TResponse>> logger,
            IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            var handlerName = typeof(TRequest).Name;

            // Safely resolve user identity
            var user = _httpContextAccessor.HttpContext?.User;
            string userId = user?.FindFirst("UserId")?.Value ?? "Anonymous";
            string username = user?.Identity?.IsAuthenticated == true
                ? user.Identity.Name ?? "Unknown"
                : "Anonymous";

            string ip = _httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? "Unknown";

            _logger.LogInformation(
                "Handling {Handler} by {User} (UserId: {UserId}, IP: {IP})",
                handlerName, username, userId, ip);

            var stopwatch = Stopwatch.StartNew();

            try
            {
                var response = await next();
                stopwatch.Stop();

                _logger.LogInformation(
                    "Handled {Handler} in {Elapsed} ms with response {@Response}",
                    handlerName, stopwatch.ElapsedMilliseconds, response);

                return response;
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                _logger.LogError(ex,
                    "Error handling {Handler} by {User} (UserId: {UserId}, IP: {IP}) in {Elapsed} ms with request {@Request}",
                    handlerName, username, userId, ip, stopwatch.ElapsedMilliseconds, request);
                throw;
            }
        }
    }

}
