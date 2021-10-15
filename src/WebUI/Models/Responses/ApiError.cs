using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BvAcademyPortal.WebUI.Models.Responses
{
    public class ApiError
    {
        public string Message { get; set; }
        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        public int Code { get; set; }
        public string Field { get; set; }
        public string Details { get; set; }
        //public Dictionary<string, object> Data { get; set; }
    }
}
