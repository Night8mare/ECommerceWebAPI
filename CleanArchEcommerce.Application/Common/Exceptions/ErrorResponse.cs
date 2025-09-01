using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchEcommerce.Application.Common.Exceptions
{
    public class ErrorResponse
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }
        public List<string>? Errors { get; set; }
    }
}
