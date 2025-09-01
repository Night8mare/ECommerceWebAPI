using CleanArchEcommerce.Application;
using CleanArchEcommerce.Application.Common.Exceptions;
using CleanArchEcommerce.Application.Common.Middlewares;
using CleanArchEcommerce.Infrastructure;
using CleanArchEcommerce.Infrastructure.Context;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

#region Dependency Injection
builder.Services.AddApplicationInfrastructure(builder.Configuration).AddApplicationServices();
#endregion
#region Serilog Settings
// Configure Serilog from appsettings.json
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

// Replace default logging
builder.Host.UseSerilog();
#endregion
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
#region Swagger Settings
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Ecommerce API", Version = "v1" });

    // JWT auth definition
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' followed by your token.",
    });

    // Global requirement for all endpoints
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});
#endregion


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        context.Database.Migrate(); // This will apply all pending migrations
    }
    catch (Exception ex)
    {
        Log.Error(ex, "An error occurred while applying migrations");
        throw;
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    #region Swagger Environment
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ecommerce API V1");
        c.ConfigObject.AdditionalItems["persistAuthorization"] = true;
        c.RoutePrefix = "";
    });
    #endregion
}
#region Global Exception Handler
app.UseExceptionHandler(appError =>
{
    appError.Run(async context =>
    {
        var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
        if (contextFeature != null)
        {
            var error = contextFeature.Error;
            context.Response.ContentType = "application/json";

            var errorResponse = new ErrorResponse
            {
                Title = "Unhandled Error",
                Message = "Something went wrong.",
                StatusCode = StatusCodes.Status500InternalServerError
            };

            if (error is ValidationException validationEx)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;

                errorResponse = new ErrorResponse
                {
                    Title = "Validation Failed",
                    Message = "One or more validation errors occurred.",
                    StatusCode = 400,
                    Errors = validationEx.Errors
                        .Select(e => $"{e.PropertyName}: {e.ErrorMessage}")
                        .ToList()
                };
            }
            else if (error is DbUpdateException dbUpdateEx)
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                errorResponse = new ErrorResponse
                {
                    Title = "Database Update Failed",
                    Message = "An error occurred while saving data.",
                    StatusCode = 500
                };
            }
            else if (error is SqlException sqlEx)
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                errorResponse = new ErrorResponse
                {
                    Title = "Database Connection Error",
                    Message = "Could not connect to the database.",
                    StatusCode = 500
                };
            }

            var result = JsonSerializer.Serialize(errorResponse);
            await context.Response.WriteAsync(result);
        }
    });
});
#endregion
app.UseMiddleware<RequestLoggingMiddleware>();
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
