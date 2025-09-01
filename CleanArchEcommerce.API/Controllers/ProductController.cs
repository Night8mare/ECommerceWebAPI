using CleanArchEcommerce.API.Controllers.Base;
using CleanArchEcommerce.Application.Common.Exceptions;
using CleanArchEcommerce.Application.Services.Products.Commands.CreateProduct;
using CleanArchEcommerce.Application.Services.Products.Commands.DeleteProduct;
using CleanArchEcommerce.Application.Services.Products.Commands.UpdateProduct;
using CleanArchEcommerce.Application.Services.Products.Queries.GetProduct;
using CleanArchEcommerce.Application.Services.Products.Queries.GetProductById;
using CleanArchEcommerce.Application.Services.Products.Queries.GetProductFilter;
using CleanArchEcommerce.Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CleanArchEcommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ApiControllerBase
    {
        public ProductController(ISender mediator, IHttpContextAccessor token) : base(mediator, token)
        {
        }
        #region Command
        #region /Post /CreateProduct
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateProductAsync([FromBody] CreateProductCommand command)
        {
            try
            {
                Log.Information("Executing create product Handler..");
                var product = await Mediator.Send(command);
                if (product.IsFailure)
                {
                    return ValidationResultHandler.Handle(product);
                }
                Log.Information("Returning product created..");
                return Ok(product);
            }
            catch (ValidationException ex)
            {
                return BadRequest(new
                {
                    message = "Validation error",
                    errors = ex.Errors.Select(e => new { field = e.PropertyName, message = e.ErrorMessage })
                });
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Internal Server Error: {e.Message}\n\nStackTrace:\n{e.StackTrace}\n\nInner:\n{e.InnerException?.Message}");
            }
        }
        #endregion

        #region /Put /UpdateProduct
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProductAsync([FromBody] UpdateProductCommand command)
        {
            try
            {
                var product = await Mediator.Send(command);
                if (product.IsFailure)
                {
                    return ValidationResultHandler.Handle(product);
                }
                return Ok("Updated");
            }
            catch (ValidationException ex)
            {
                return BadRequest(new
                {
                    message = "Validation error",
                    errors = ex.Errors.Select(e => new { field = e.PropertyName, message = e.ErrorMessage })
                });
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Internal Server Error: {e.Message}\n\nStackTrace:\n{e.StackTrace}\n\nInner:\n{e.InnerException?.Message}");
            }
        }
        #endregion

        #region /Delete /DeteleProduct
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductAsync([FromQuery] DeleteProductCommand command)
        {
            try
            {
                Log.Information("Executing delete product controller..");
                var product = await Mediator.Send(command);
                if (product.IsFailure)
                {
                    return ValidationResultHandler.Handle(product);
                }
                Log.Information("Returning product deleted successfully");
                return Ok("Deleted");
            }
            catch (ValidationException ex)
            {
                return BadRequest(new
                {
                    message = "Validation error",
                    errors = ex.Errors.Select(e => new { field = e.PropertyName, message = e.ErrorMessage })
                });
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Internal Server Error: {e.Message}\n\nStackTrace:\n{e.StackTrace}\n\nInner:\n{e.InnerException?.Message}");
            }
        }
        #endregion

        #endregion

        #region Query
        #region /Get /GetAllProduct
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllProductAsync([FromQuery] GetProductQuery query)
        {
            try
            {
                Log.Information("Executing get all products Controller..");
                var product = await Mediator.Send(query);
                Log.Information($"{product.Value.Count()} products returned");
                if (product.IsFailure)
                {
                    Log.Error("product list came with no items..");
                    return ValidationResultHandler.Handle(product);
                }
                Log.Information("returning product list..");
                return Ok(product.Value);
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

        #region /Get /Filter
        [Authorize]
        [HttpGet("Filter")]
        public async Task<IActionResult> GetFilterProductAsync([FromQuery] GetProductFilterQuery query)
        {
            try
            {
                Log.Information("Executing get product filter controller..");
                var product = await Mediator.Send(query);
                if (product.IsFailure)
                {
                    Log.Error("No product found");
                    return ValidationResultHandler.Handle(product);
                }
                return Ok(product);
            }
            catch (ValidationException ex)
            {
                return BadRequest(new
                {
                    message = "Validation error",
                    errors = ex.Errors.Select(e => new { field = e.PropertyName, message = e.ErrorMessage })
                });
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Internal Server Error: {e.Message}\n\nStackTrace:\n{e.StackTrace}\n\nInner:\n{e.InnerException?.Message}");
            }
        }
        #endregion

        #region /Get /GetProductById
        [Authorize(Roles = "Admin")]
        [HttpGet("{ProductId}")]
        public async Task<IActionResult> GetByIdProductAsync([FromQuery] GetProductByIdQuery query)
        {
            try
            {
                Log.Information("Executing Get product by ID Controller..");
                var product = await Mediator.Send(query);
                if (product.IsFailure)
                {
                    Log.Error("No product found");
                    return ValidationResultHandler.Handle(product);
                }
                return Ok(product);
            }
            catch (ValidationException ex)
            {
                return BadRequest(new
                {
                    message = "Validation error",
                    errors = ex.Errors.Select(e => new { field = e.PropertyName, message = e.ErrorMessage })
                });
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Internal Server Error: {e.Message}\n\nStackTrace:\n{e.StackTrace}\n\nInner:\n{e.InnerException?.Message}");
            }
        }
        #endregion
        #endregion
    }
}
