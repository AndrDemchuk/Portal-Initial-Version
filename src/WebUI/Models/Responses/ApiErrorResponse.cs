using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BvAcademyPortal.WebUI.Models.Responses
{
    public class ApiErrorResponse
    {
        /// <summary>
        /// Gets or sets the errors.
        /// </summary>
        /// <value>
        /// The errors.
        /// </value>
        public IReadOnlyCollection<ApiError> Errors { get; set; }

        public ApiErrorResponse(ApiError error)
        {
            Errors = new List<ApiError>(1) { error };
        }

        public ApiErrorResponse(IReadOnlyCollection<ApiError> errors)
        {
            Errors = errors;
        }

        public ApiErrorResponse(int code, string message = null, string field = null)
            : this(new ApiError { Code = code, Message = message, Field = field })
        {

        }
    }
}
