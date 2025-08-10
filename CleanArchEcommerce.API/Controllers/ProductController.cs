using CleanArchEcommerce.Application.Common.Exceptions;
using CleanArchEcommerce.Application.Services.Products.Commands.CreateProduct;
using CleanArchEcommerce.Application.Services.Products.Commands.DeleteProduct;
using CleanArchEcommerce.Application.Services.Products.Commands.UpdateProduct;
using CleanArchEcommerce.Application.Services.Products.Queries.GetProduct;
using CleanArchEcommerce.Application.Services.Products.Queries.GetProductById;
using CleanArchEcommerce.Application.Services.Products.Queries.GetProductFilter;
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
        public ProductController(ISender mediator) : base(mediator)
        {
        }

        #region /Get /GetAllProduct
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllProductAsync([FromQuery] GetProductQuery query)
        {
            try
            {
                Log.Information("Executing get all products Controller..");
                var products = await Mediator.Send(query);
                Log.Information($"{products.Value.Count()} products returned");
                if (products.IsFailure)
                {
                    Log.Error("product list came with no items..");
                    return products.ErrorType switch
                    {
                        ErrorType.None => Ok(),
                        ErrorType.NotFound => NotFound(products.Error),
                        ErrorType.Validation => BadRequest(products.Error),
                        ErrorType.Conflict => Conflict(products.Error),
                        ErrorType.Unauthorized => Unauthorized(),
                        ErrorType.Forbidden => Forbid(),
                        ErrorType.BadRequest => BadRequest(products.Error),
                        _ => throw new NotImplementedException(),
                    };
                }
                Log.Information("returning product list..");
                return Ok(products.Value);
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
        public async Task<IActionResult> GetFilterProductAsync([FromQuery] GetProductFilterQuery filter)
        {
            try
            {
                Log.Information("Executing get product filter controller..");
                var productFilter = await Mediator.Send(filter);
                if (productFilter.IsFailure)
                {
                    Log.Error("No product found");
                    return productFilter.ErrorType switch
                    {
                        ErrorType.None => Ok(),
                        ErrorType.NotFound => NotFound(productFilter.Error),
                        ErrorType.Validation => BadRequest(productFilter.Error),
                        ErrorType.Conflict => Conflict(productFilter.Error),
                        ErrorType.Unauthorized => Unauthorized(),
                        ErrorType.Forbidden => Forbid(),
                        ErrorType.BadRequest => BadRequest(productFilter.Error),
                        _ => throw new NotImplementedException(),
                    };
                }
                return Ok(productFilter);
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
        public async Task<IActionResult> GetByIdProductAsync([FromQuery] GetProductByIdQuery getProductByIdQuery)
        {
            try
            {
                Log.Information("Executing Get product by ID Controller..");
                var product = await Mediator.Send(getProductByIdQuery);
                if (product.IsFailure)
                {
                    Log.Error("No product found");
                    return product.ErrorType switch
                    {
                        ErrorType.None => Ok(),
                        ErrorType.NotFound => NotFound(product.Error),
                        ErrorType.Validation => BadRequest(product.Error),
                        ErrorType.Conflict => Conflict(product.Error),
                        ErrorType.Unauthorized => Unauthorized(),
                        ErrorType.Forbidden => Forbid(),
                        ErrorType.BadRequest => BadRequest(product.Error),
                        _ => throw new NotImplementedException(),
                    };
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

        #region /Post /CreateProduct
        [Authorize(Roles ="Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateProductAsync([FromBody]CreateProductCommand createProductCommand)
        {
            try
            {
                Log.Information("Executing create product Handler..");
                var createProduct = await Mediator.Send(createProductCommand);
                if (createProduct.IsFailure)
                {
                    return createProduct.ErrorType switch
                    {
                        ErrorType.None => Ok(),
                        ErrorType.NotFound => NotFound(createProduct.Error),
                        ErrorType.Validation => BadRequest(createProduct.Error),
                        ErrorType.Conflict => Conflict(createProduct.Error),
                        ErrorType.Unauthorized => Unauthorized(),
                        ErrorType.Forbidden => Forbid(),
                        ErrorType.BadRequest => BadRequest(createProduct.Error),
                        _ => throw new NotImplementedException(),
                    };
                }
                Log.Information("Returning product created..");
                return Ok(createProduct);
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
        public async Task<IActionResult> UpdateProductAsync([FromBody] UpdateProductCommand updateProductCommand)
        {
            try
            {
                var updateProduct = await Mediator.Send(updateProductCommand);
                if (updateProduct.IsFailure)
                {
                    return updateProduct.ErrorType switch
                    {
                        ErrorType.None => Ok(),
                        ErrorType.NotFound => NotFound(updateProduct.Error),
                        ErrorType.Validation => BadRequest(updateProduct.Error),
                        ErrorType.Conflict => Conflict(updateProduct.Error),
                        ErrorType.Unauthorized => Unauthorized(),
                        ErrorType.Forbidden => Forbid(),
                        ErrorType.BadRequest => BadRequest(updateProduct.Error),
                        _ => throw new NotImplementedException(),
                    };
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
        public async Task<IActionResult> DeleteProductAsync([FromQuery] DeleteProductCommand deleteProductCommand)
        {
            try
            {
                Log.Information("Executing delete product controller..");
                var deleteProduct = await Mediator.Send(deleteProductCommand);
                if (deleteProduct.IsFailure)
                {
                    return deleteProduct.ErrorType switch
                    {
                        ErrorType.None => Ok(),
                        ErrorType.NotFound => NotFound(deleteProduct.Error),
                        ErrorType.Validation => BadRequest(deleteProduct.Error),
                        ErrorType.Conflict => Conflict(deleteProduct.Error),
                        ErrorType.Unauthorized => Unauthorized(),
                        ErrorType.Forbidden => Forbid(),
                        ErrorType.BadRequest => BadRequest(deleteProduct.Error),
                        _ => throw new NotImplementedException(),
                    };
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
    }
}
