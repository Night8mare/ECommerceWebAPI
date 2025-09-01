using CleanArchEcommerce.API.Controllers.Base;
using CleanArchEcommerce.Application.Common.Exceptions;
using CleanArchEcommerce.Application.Services.Users.Commands.CreateUser.AdminCreate;
using CleanArchEcommerce.Application.Services.Users.Commands.CreateUser.Registery;
using CleanArchEcommerce.Application.Services.Users.Commands.DeleteUser.AdminDelete;
using CleanArchEcommerce.Application.Services.Users.Commands.DeleteUser.UserDelete;
using CleanArchEcommerce.Application.Services.Users.Commands.UpdateUser.AdminUpdate;
using CleanArchEcommerce.Application.Services.Users.Commands.UpdateUser.UserUpdate;
using CleanArchEcommerce.Application.Services.Users.Queries.GetUser;
using CleanArchEcommerce.Application.Services.Users.Queries.GetUserById;
using CleanArchEcommerce.Application.Services.Users.Queries.LoginUser;
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
    public class UserController : ApiControllerBase
    {
        public UserController(ISender mediator, IHttpContextAccessor token) : base(mediator, token)
        {
        }
        #region Command

        #region /Post /RegisterUser
        [HttpPost]
        public async Task<IActionResult> RegisterUserAsync([FromBody] RegistryUserCommand command)
        {
            try
            {
                Log.Information("Executed register user Controller..");
                var user = await Mediator.Send(command);
                if (user.IsFailure)
                {
                    Log.Error("Something went wrong while saving..");
                    return ValidationResultHandler.Handle(user);
                }
                return Ok(user);
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

        #region /Post /AdminCreate
        [Authorize(Roles = "Admin")]
        [HttpPost("AdminCreate")]
        public async Task<IActionResult> AdminCreateAsync([FromBody] AdminCreateCommand command)
        {
            try
            {
                Log.Information("Executing admin create controller..");
                var user = await Mediator.Send(command);
                if (user.IsFailure)
                {
                    Log.Error("Something went wrong while creating the admin account..");
                    return ValidationResultHandler.Handle(user);
                }
                return Ok("Registered");
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

        #region /Delete /DeleteUser
        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(DeleteUserCommand command)
        {
            try
            {
                Log.Information("Executing delete user controller..");
                var user = await Mediator.Send(command);
                if (user.IsFailure)
                {
                    Log.Error("Something went wrong while deleting user..");
                    return ValidationResultHandler.Handle(user);
                }
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

        #region /Delete /AdminDelete
        [Authorize(Roles = "Admin,Support")]
        [HttpDelete("AdminDelete")]
        public async Task<IActionResult> AdminDeleteAsync([FromQuery] DeleteAdminCommand command)
        {
            try
            {
                Log.Information("Executing admin delete controller..");
                var user = await Mediator.Send(command);
                if (user.IsFailure)
                {
                    Log.Error("Something went wrong while deleting..");
                    return ValidationResultHandler.Handle(user);
                }
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

        #region /Put /UpdateUser
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateUserAsync([FromBody] UpdateUserCommand command)
        {
            try
            {
                Log.Information("Executed update user controller..");
                var user = await Mediator.Send(command);
                if (user.IsFailure)
                {
                    Log.Error("Something went wrong while updating user..");
                    return ValidationResultHandler.Handle(user);
                }
                return Ok(user);
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

        #region /Put /UpdateAdmin
        [Authorize(Roles = "Admin")]
        [HttpPut("AdminUpdate")]
        public async Task<IActionResult> UpdateAdminAsync([FromBody] UpdateAdminCommand command)
        {
            try
            {
                Log.Information("Execute update admin controller..");
                var user = await Mediator.Send(command);
                if (user.IsFailure)
                {
                    Log.Error("Something went wrong while updating admin account..");
                    return ValidationResultHandler.Handle(user);
                }
                return Ok(user);
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

        #region /Get /GetAllUsers
        [Authorize(Roles = "Admin")]
        [HttpGet("GetUsers")]
        public async Task<IActionResult> GetAllUsersAsync([FromQuery] GetUserQuery query)
        {
            try
            {
                var user = await Mediator.Send(query);
                if (user.IsFailure)
                {
                    Log.Error("Item wasn`t created..");
                    return ValidationResultHandler.Handle(user);
                }
                return Ok(user);
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

        #region /Get /GetUserById
        [Authorize(Roles = "Admin")]
        [HttpGet("GetUser")]
        public async Task<IActionResult> GetUserByIdAsync([FromQuery] GetUserByIdQuery query)
        {
            try
            {
                Log.Information("Executing get user by ID controller..");
                var user = await Mediator.Send(query);
                if (user.IsFailure)
                {
                    Log.Error("User not found in the database..");
                    return ValidationResultHandler.Handle(user);
                }
                return Ok(user);
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

        #region /Post /LoginUser
        [HttpPost("Login")]
        public async Task<IActionResult> LoginUserAsync([FromBody] LoginUserQuery query)
        {
            try
            {
                Log.Information("Started Excuting Login controller...");
                var user = await Mediator.Send(query);
                if (user.IsFailure)
                {
                    Log.Error("Something went wrong while logging in..");
                    return ValidationResultHandler.Handle(user);
                }
                return Ok(user);
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
