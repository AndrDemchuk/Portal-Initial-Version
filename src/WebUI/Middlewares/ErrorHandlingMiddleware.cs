using BvAcademyPortal.Application.Common.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace BvAcademyPortal.WebUI.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            switch (exception)
            {
                case ForbiddenAccessException e:
                    response.StatusCode = (int)HttpStatusCode.Forbidden;
                    _logger.LogError($"A new ForbiddenAccessException has been thrown: {e}");
                    break;
                case NotFoundException e:
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    _logger.LogError($"A new NotFoundException has been thrown: {e}");
                    break;
                case ValidationException e:
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    _logger.LogError($"A new ValidationException has been thrown: {e}");
                    break;
                default:
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    _logger.LogError($"A new InternalServerError occured: {exception}");
                    break;
            }

            var result = JsonSerializer.Serialize(new { message = exception?.Message });
            await response.WriteAsync(result);
        }
    }
}
