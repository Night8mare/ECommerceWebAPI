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
        public UserController(ISender mediator) : base(mediator)
        {
        }
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
                    return user.ErrorType switch
                    {
                        ErrorType.None => Ok(),
                        ErrorType.NotFound => NotFound(user.Error),
                        ErrorType.Validation => BadRequest(user.Error),
                        ErrorType.Conflict => Conflict(user.Error),
                        ErrorType.Unauthorized => Unauthorized(),
                        ErrorType.Forbidden => Forbid(),
                        ErrorType.BadRequest => BadRequest(user.Error),
                        _ => throw new NotImplementedException(),
                    };
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
                    return user.ErrorType switch
                    {
                        ErrorType.None => Ok(),
                        ErrorType.NotFound => NotFound(user.Error),
                        ErrorType.Validation => BadRequest(user.Error),
                        ErrorType.Conflict => Conflict(user.Error),
                        ErrorType.Unauthorized => Unauthorized(),
                        ErrorType.Forbidden => Forbid(),
                        ErrorType.BadRequest => BadRequest(user.Error),
                        _ => throw new NotImplementedException(),
                    };
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

        #region /Post /RegisterUser
        [HttpPost]
        public async Task<IActionResult> RegisterUserAsync([FromBody] RegistryUserCommand register)
        {
            try
            {
                Log.Information("Executed register user Controller..");
                var createUser = await Mediator.Send(register);
                if (createUser.IsFailure)
                {
                    Log.Error("Something went wrong while saving..");
                    return createUser.ErrorType switch
                    {
                        ErrorType.None => Ok(),
                        ErrorType.NotFound => NotFound(createUser.Error),
                        ErrorType.Validation => BadRequest(createUser.Error),
                        ErrorType.Conflict => Conflict(createUser.Error),
                        ErrorType.Unauthorized => Unauthorized(),
                        ErrorType.Forbidden => Forbid(),
                        ErrorType.BadRequest => BadRequest(createUser.Error),
                        _ => throw new NotImplementedException(),
                    };
                }
                return Ok(createUser);
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
        public async Task<IActionResult> AdminCreateAsync([FromBody] AdminCreateCommand adminCreate)
        {
            try
            {
                Log.Information("Executing admin create controller..");
                var createAdmin = await Mediator.Send(adminCreate);
                if (createAdmin.IsFailure)
                {
                    Log.Error("Something went wrong while creating the admin account..");
                    return createAdmin.ErrorType switch
                    {
                        ErrorType.None => Ok(),
                        ErrorType.NotFound => NotFound(createAdmin.Error),
                        ErrorType.Validation => BadRequest(createAdmin.Error),
                        ErrorType.Conflict => Conflict(createAdmin.Error),
                        ErrorType.Unauthorized => Unauthorized(),
                        ErrorType.Forbidden => Forbid(),
                        ErrorType.BadRequest => BadRequest(createAdmin.Error),
                        _ => throw new NotImplementedException(),
                    };
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

        #region /Post /LoginUser
        [HttpPost("Login")]
        public async Task<IActionResult> LoginUserAsync([FromBody] LoginUserQuery login)
        {
            try
            {
                Log.Information("Started Excuting Login controller...");
                var userLogin = await Mediator.Send(login);
                if (userLogin.IsFailure)
                {
                    Log.Error("Something went wrong while logging in..");
                    return userLogin.ErrorType switch
                    {
                        ErrorType.None => Ok(),
                        ErrorType.NotFound => NotFound(userLogin.Error),
                        ErrorType.Validation => BadRequest(userLogin.Error),
                        ErrorType.Conflict => Conflict(userLogin.Error),
                        ErrorType.Unauthorized => Unauthorized(),
                        ErrorType.Forbidden => Forbid(),
                        ErrorType.BadRequest => BadRequest(userLogin.Error),
                        _ => throw new NotImplementedException(),
                    };
                }
                return Ok(userLogin);
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
        public async Task<IActionResult> DeleteAsync(DeleteUserCommand deleteUser)
        {
            try
            {
                Log.Information("Executing delete user controller..");
                var user = await Mediator.Send(deleteUser);
                if (user.IsFailure)
                {
                    Log.Error("Something went wrong while deleting user..");
                    return user.ErrorType switch
                    {
                        ErrorType.None => Ok(),
                        ErrorType.NotFound => NotFound(user.Error),
                        ErrorType.Validation => BadRequest(user.Error),
                        ErrorType.Conflict => Conflict(user.Error),
                        ErrorType.Unauthorized => Unauthorized(),
                        ErrorType.Forbidden => Forbid(),
                        ErrorType.BadRequest => BadRequest(user.Error),
                        _ => throw new NotImplementedException(),
                    };
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
        public async Task<IActionResult> AdminDeleteAsync([FromQuery] DeleteAdminCommand deleteAdmin)
        {
            try
            {
                Log.Information("Executing admin delete controller..");
                var user = await Mediator.Send(deleteAdmin);
                if (user.IsFailure)
                {
                    Log.Error("Something went wrong while deleting..");
                    return user.ErrorType switch
                    {
                        ErrorType.None => Ok(),
                        ErrorType.NotFound => NotFound(user.Error),
                        ErrorType.Validation => BadRequest(user.Error),
                        ErrorType.Conflict => Conflict(user.Error),
                        ErrorType.Unauthorized => Unauthorized(),
                        ErrorType.Forbidden => Forbid(),
                        ErrorType.BadRequest => BadRequest(user.Error),
                        _ => throw new NotImplementedException(),
                    };
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
        public async Task<IActionResult> UpdateUserAsync([FromBody] UpdateUserCommand updateUser)
        {
            try
            {
                Log.Information("Executed update user controller..");
                var user = await Mediator.Send(updateUser);
                if (user.IsFailure)
                {
                    Log.Error("Something went wrong while updating user..");
                    return user.ErrorType switch
                    {
                        ErrorType.None => Ok(),
                        ErrorType.NotFound => NotFound(user.Error),
                        ErrorType.Validation => BadRequest(user.Error),
                        ErrorType.Conflict => Conflict(user.Error),
                        ErrorType.Unauthorized => Unauthorized(),
                        ErrorType.Forbidden => Forbid(),
                        ErrorType.BadRequest => BadRequest(user.Error),
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

        #region /Put /UpdateAdmin
        [Authorize(Roles ="Admin")]
        [HttpPut("AdminUpdate")]
        public async Task<IActionResult> UpdateAdminAsync([FromBody] UpdateAdminCommand updateAdmin)
        {
            try
            {
                Log.Information("Execute update admin controller..");
                var user = await Mediator.Send(updateAdmin);
                if (user.IsFailure)
                {
                    Log.Error("Something went wrong while updating admin account..");
                    return user.ErrorType switch
                    {
                        ErrorType.None => Ok(),
                        ErrorType.NotFound => NotFound(user.Error),
                        ErrorType.Validation => BadRequest(user.Error),
                        ErrorType.Conflict => Conflict(user.Error),
                        ErrorType.Unauthorized => Unauthorized(),
                        ErrorType.Forbidden => Forbid(),
                        ErrorType.BadRequest => BadRequest(user.Error),
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
    }
}
