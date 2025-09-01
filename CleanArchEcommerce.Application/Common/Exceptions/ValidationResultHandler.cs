using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchEcommerce.Application.Common.Exceptions
{
    public static class ValidationResultHandler
    {
        public static IActionResult Handle<T>(MetaResult<T> result)
        {
            return result.ErrorType switch
            {
                ErrorType.None => new OkObjectResult(result.Value),
                ErrorType.NotFound => new NotFoundObjectResult(result.Error),
                ErrorType.Validation => new BadRequestObjectResult(result.Error),
                ErrorType.Conflict => new ConflictObjectResult(result.Error),
                ErrorType.Unauthorized => new UnauthorizedResult(),
                ErrorType.Forbidden => new ForbidResult(),
                ErrorType.BadRequest => new BadRequestObjectResult(result.Error),
                _ => throw new NotImplementedException($"ErrorType {result.ErrorType} not handled."),
            };
        }
        public static IActionResult Handle<T>(Result<T> result)
        {
            return result.ErrorType switch
            {
                ErrorType.None => new OkObjectResult(result.Value),
                ErrorType.NotFound => new NotFoundObjectResult(result.Error),
                ErrorType.Validation => new BadRequestObjectResult(result.Error),
                ErrorType.Conflict => new ConflictObjectResult(result.Error),
                ErrorType.Unauthorized => new UnauthorizedResult(),
                ErrorType.Forbidden => new ForbidResult(),
                ErrorType.BadRequest => new BadRequestObjectResult(result.Error),
                _ => throw new NotImplementedException($"ErrorType {result.ErrorType} not handled."),
            };
        }
    }
}
