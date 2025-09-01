using CleanArchEcommerce.API.Controllers.Base;
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
        public ItemController(ISender mediator, IHttpContextAccessor token) : base(mediator,token)
        {
        }
        #region Command
        #region /Put /UpdateItem
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateItemAsync([FromQuery] UpdateItemCommand command)
        {
            try
            {
                Log.Information("Executing update item controller..");
                var item = await Mediator.Send(command);
                if (item.IsFailure)
                {
                    Log.Error("Item wasn`t updated..");
                    return ValidationResultHandler.Handle(item);
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
        public async Task<IActionResult> DeleteItemAsync([FromQuery] DeleteItemCommand command)
        {
            try
            {
                Log.Information("Executing delete item controller..");
                var item = await Mediator.Send(command);
                if (item.IsFailure)
                {
                    Log.Error($"Item ID: {command.ItemId} wasn`t deleted..");
                    return ValidationResultHandler.Handle(item);
                }
                Log.Information($"Item ID: {command.ItemId} deleted successfully..");
                return Ok($"Item ID: {command.ItemId} Deleted.");
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
        #endregion

        #region Query
        #region /Get /GetAllItems
        [Authorize(Roles ="Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllItemAsync([FromQuery] GetAllItemQuery query)
        {
            try
            {
                Log.Information("Executing get all item controller..");
                var item = await Mediator.Send(query);
                if (item.IsFailure)
                {
                    Log.Error("No item found in the database..");
                    return ValidationResultHandler.Handle(item);
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
        public async Task<IActionResult> GetAllCartItemAsync([FromQuery] GetAllCartItemQuery query)
        {
            try
            {
                Log.Information("Executing get all cart item controller..");
                var item = await Mediator.Send(query);
                if (item.IsFailure)
                {
                    Log.Error("There is no items available in the cart..");
                    return ValidationResultHandler.Handle(item);
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
        public async Task<IActionResult> CreateItemAsync([FromQuery] CreateItemCommand query)
        {
            try
            {
                Log.Information("Executing create item controller..");
                var item = await Mediator.Send(query);
                if (item.IsFailure)
                {
                    Log.Error("Item wasn`t created..");
                    return ValidationResultHandler.Handle(item);
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
        
        #endregion
    }
}
