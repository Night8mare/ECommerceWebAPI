using CleanArchEcommerce.Application.Common.Exceptions;
using CleanArchEcommerce.Application.Services.Items.Command.CreateItem;
using CleanArchEcommerce.Application.Services.Items.Command.DeleteItem;
using CleanArchEcommerce.Application.Services.Items.Command.UpdateItem;
using CleanArchEcommerce.Application.Services.Items.Query.GetAllCartItem;
using CleanArchEcommerce.Application.Services.Items.Query.GetAllItem;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CleanArchEcommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ApiControllerBase
    {
        public ItemController(ISender mediator) : base(mediator)
        {
        }
        #region /Get /GetAllItems
        [Authorize(Roles ="Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllItemAsync([FromQuery] GetAllItemQuery getAll)
        {
            try
            {
                Log.Information("Executing get all item controller..");
                var item = await Mediator.Send(getAll);
                if (item.IsFailure)
                {
                    Log.Error("No item found in the database..");
                    return item.ErrorType switch
                    {
                        ErrorType.None => Ok(),
                        ErrorType.NotFound => NotFound(item.Error),
                        ErrorType.Validation => BadRequest(item.Error),
                        ErrorType.Conflict => Conflict(item.Error),
                        ErrorType.Unauthorized => Unauthorized(),
                        ErrorType.Forbidden => Forbid(),
                        ErrorType.BadRequest => BadRequest(item.Error),
                        _ => throw new NotImplementedException(),
                    };
                }
                Log.Information("Returning item list");
                return Ok(item);
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

        #region /Get /GetCartItem
        [Authorize]
        [HttpGet("GetCartItem")]
        public async Task<IActionResult> GetAllCartItemAsync([FromQuery] GetAllCartItemQuery getAllCart)
        {
            try
            {
                Log.Information("Executing get all cart item controller..");
                var item = await Mediator.Send(getAllCart);
                if (item.IsFailure)
                {
                    Log.Error("There is no items available in the cart..");
                    return item.ErrorType switch
                    {
                        ErrorType.None => Ok(),
                        ErrorType.NotFound => NotFound(item.Error),
                        ErrorType.Validation => BadRequest(item.Error),
                        ErrorType.Conflict => Conflict(item.Error),
                        ErrorType.Unauthorized => Unauthorized(),
                        ErrorType.Forbidden => Forbid(),
                        ErrorType.BadRequest => BadRequest(item.Error),
                        _ => throw new NotImplementedException(),
                    };
                }
                Log.Information("Returning item list for the cart");
                return Ok(item);
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

        #region /Get /CreateItem
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateItemAsync([FromQuery] CreateItemCommand itemCommand)
        {
            try
            {
                Log.Information("Executing create item controller..");
                var item = await Mediator.Send(itemCommand);
                if (item.IsFailure)
                {
                    Log.Error("Item wasn`t created..");
                    return item.ErrorType switch
                    {
                        ErrorType.None => Ok(),
                        ErrorType.NotFound => NotFound(item.Error),
                        ErrorType.Validation => BadRequest(item.Error),
                        ErrorType.Conflict => Conflict(item.Error),
                        ErrorType.Unauthorized => Unauthorized(),
                        ErrorType.Forbidden => Forbid(),
                        ErrorType.BadRequest => BadRequest(item.Error),
                        _ => throw new NotImplementedException(),
                    };
                }
                Log.Information("Item created successfully..");
                return Ok("Item created");
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

        #region /Put /UpdateItem
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateItemAsync([FromQuery] UpdateItemCommand updateItem)
        {
            try
            {
                Log.Information("Executing update item controller..");
                var item = await Mediator.Send(updateItem);
                if (item.IsFailure)
                {
                    Log.Error("Item wasn`t updated..");
                    return item.ErrorType switch
                    {
                        ErrorType.None => Ok(),
                        ErrorType.NotFound => NotFound(item.Error),
                        ErrorType.Validation => BadRequest(item.Error),
                        ErrorType.Conflict => Conflict(item.Error),
                        ErrorType.Unauthorized => Unauthorized(),
                        ErrorType.Forbidden => Forbid(),
                        ErrorType.BadRequest => BadRequest(item.Error),
                        _ => throw new NotImplementedException(),
                    };
                }
                Log.Information("Item updated successfully..");
                return Ok("Item updated");
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

        #region /Delete /DeleteItem
        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> DeleteItemAsync([FromQuery] DeleteItemCommand deleteItem)
        {
            try
            {
                Log.Information("Executing delete item controller..");
                var item = await Mediator.Send(deleteItem);
                if (item.IsFailure)
                {
                    Log.Error($"Item ID: {deleteItem.ItemId} wasn`t deleted..");
                    return item.ErrorType switch
                    {
                        ErrorType.None => Ok(),
                        ErrorType.NotFound => NotFound(item.Error),
                        ErrorType.Validation => BadRequest(item.Error),
                        ErrorType.Conflict => Conflict(item.Error),
                        ErrorType.Unauthorized => Unauthorized(),
                        ErrorType.Forbidden => Forbid(),
                        ErrorType.BadRequest => BadRequest(item.Error),
                        _ => throw new NotImplementedException(),
                    };
                }
                Log.Information($"Item ID: {deleteItem.ItemId} deleted successfully..");
                return Ok($"Item ID: {deleteItem.ItemId} Deleted.");
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
