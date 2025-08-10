using CleanArchEcommerce.Application.Common.Exceptions;
using CleanArchEcommerce.Application.Services.Orders.Commands.CreateOrder;
using CleanArchEcommerce.Application.Services.Orders.Queries.GetOrderHistory;
using CleanArchEcommerce.Application.Services.Orders.Queries.GetOrderItemHistory;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CleanArchEcommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ApiControllerBase
    {
        public OrderController(ISender mediator) : base(mediator)
        {
        }
        #region /Post /CreateOrder
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateOrderAsync([FromQuery] CreateOrderCommand orderCommand)
        {
            try
            {
                Log.Information($"Executing Create Order Controller..");
                var order = await Mediator.Send(orderCommand);
                if (order.IsFailure)
                {
                    Log.Error("Something went wronge...");
                    return order.ErrorType switch
                    {
                        ErrorType.None => Ok(),
                        ErrorType.NotFound => NotFound(order.Error),
                        ErrorType.Validation => BadRequest(order.Error),
                        ErrorType.Conflict => Conflict(order.Error),
                        ErrorType.Unauthorized => Unauthorized(),
                        ErrorType.Forbidden => Forbid(),
                        ErrorType.BadRequest => BadRequest(order.Error),
                        _ => throw new NotImplementedException(),
                    };
                }
                Log.Information("Returning order successfully..");
                return Ok(order);
            }
            catch (ValidationException ex)
            {
                Log.Error("Validation Error..");
                return BadRequest(new
                {
                    message = "Validation error",
                    errors = ex.Errors.Select(e => new { field = e.PropertyName, message = e.ErrorMessage })
                });
            }
            catch (Exception e)
            {
                Log.Error("Unhandled Error..");
                return StatusCode(500, $"Internal Server Error: {e.Message}\n\nStackTrace:\n{e.StackTrace}\n\nInner:\n{e.InnerException?.Message}");
            }
        }
        #endregion
        #region /Get /Orders history
        [Authorize]
        [HttpGet("OrderHistory")]
        public async Task<IActionResult> GetOrderHistoryAsync(GetOrderHistoryQuery getOrder)
        {
            try
            {
                Log.Information("Executing get order history controller..");
                var order = await Mediator.Send(getOrder);
                if (order.IsFailure)
                {
                    Log.Error("No order found..");
                    return order.ErrorType switch
                    {
                        ErrorType.None => Ok(),
                        ErrorType.NotFound => NotFound(order.Error),
                        ErrorType.Validation => BadRequest(order.Error),
                        ErrorType.Conflict => Conflict(order.Error),
                        ErrorType.Unauthorized => Unauthorized(),
                        ErrorType.Forbidden => Forbid(),
                        ErrorType.BadRequest => BadRequest(order.Error),
                        _ => throw new NotImplementedException(),
                    };
                }
                return Ok(order);
            }
            catch (ValidationException ex)
            {
                Log.Error("Validation Error..");
                return BadRequest(new
                {
                    message = "Validation error",
                    errors = ex.Errors.Select(e => new { field = e.PropertyName, message = e.ErrorMessage })
                });
            }
            catch (Exception e)
            {
                Log.Error("Unhandled Error..");
                return StatusCode(500, $"Internal Server Error: {e.Message}\n\nStackTrace:\n{e.StackTrace}\n\nInner:\n{e.InnerException?.Message}");
            }
        }
        #endregion
        #region /Get /Order item history
        [Authorize]
        [HttpGet("ItemHistory")]
        public async Task<IActionResult> GetOrderItemHistory(GetOrderItemHistoryQuery getOrder)
        {
            try
            {
                Log.Information("Executing get order item history controller..");
                var order = await Mediator.Send(getOrder);
                if (order.IsFailure)
                {
                    Log.Error("No item history found for this user..");
                    return order.ErrorType switch
                    {
                        ErrorType.None => Ok(),
                        ErrorType.NotFound => NotFound(order.Error),
                        ErrorType.Validation => BadRequest(order.Error),
                        ErrorType.Conflict => Conflict(order.Error),
                        ErrorType.Unauthorized => Unauthorized(),
                        ErrorType.Forbidden => Forbid(),
                        ErrorType.BadRequest => BadRequest(order.Error),
                        _ => throw new NotImplementedException(),
                    };
                }
                return Ok(order);
            }
            catch (ValidationException ex)
            {
                Log.Error("Validation Error..");
                return BadRequest(new
                {
                    message = "Validation error",
                    errors = ex.Errors.Select(e => new { field = e.PropertyName, message = e.ErrorMessage })
                });
            }
            catch (Exception e)
            {
                Log.Error("Unhandled Error..");
                return StatusCode(500, $"Internal Server Error: {e.Message}\n\nStackTrace:\n{e.StackTrace}\n\nInner:\n{e.InnerException?.Message}");
            }
        }
        #endregion
    }
}
