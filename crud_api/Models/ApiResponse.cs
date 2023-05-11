using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crud_api.Models
{
    public class ApiResponse
    {
        public bool Ok { get; set; }
        public string Message { get; set; }

        public Object Data { get; set; }
        public string Token { get; set; }
    }
}
