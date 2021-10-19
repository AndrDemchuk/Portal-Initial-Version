using BvAcademyPortal.Application.Common.Exceptions;
using BvAcademyPortal.WebUI.Models.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
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
            ApiErrorResponse errorResponse;
            var response = context.Response;
            response.ContentType = "application/json";

            switch (exception)
            {
                case ForbiddenAccessException e:
                    errorResponse = new ApiErrorResponse(
                        new ApiError
                        {
                            Message = e.Message,
                            Code = (int)HttpStatusCode.Forbidden,
                            Details = e.InnerException.Message
                        }
                        );                    
                    //response.StatusCode = (int)HttpStatusCode.Forbidden;
                    _logger.LogError($"A new ForbiddenAccessException has been thrown: {e}");
                    break;
                case NotFoundException e:
                    errorResponse = new ApiErrorResponse(
                        new ApiError
                        {
                            Message = e.Message,
                            Code = (int)HttpStatusCode.NotFound,
                            Details = e.InnerException.Message
                        }
                        );
                    //response.StatusCode = (int)HttpStatusCode.NotFound;
                    _logger.LogError($"A new NotFoundException has been thrown: {e}");
                    break;
                case ValidationException e:
                    errorResponse = new ApiErrorResponse(
                        new ApiError
                        {
                            Message = e.Message,
                            Code = (int)HttpStatusCode.BadRequest,
                            Details = e.InnerException.Message
                        }
                        );
                    //response.StatusCode = (int)HttpStatusCode.BadRequest;
                    _logger.LogError($"A new ValidationException has been thrown: {e}");
                    break;
                default:
                    errorResponse = new ApiErrorResponse(
                        new ApiError
                        {
                            Message = exception.Message,
                            Code = (int)HttpStatusCode.InternalServerError,
                            Details = exception.InnerException.Message
                        }
                        );
                    //response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    _logger.LogError($"A new InternalServerError occured: {exception}");
                    break;
            }

            //var result = JsonSerializer.Serialize(new { message = exception?.Message });
            await response.WriteAsync(JsonConvert.SerializeObject(errorResponse));
        }
    }
}
