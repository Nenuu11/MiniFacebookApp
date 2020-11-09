using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aphrie.app.Models
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public object Data { get; set; }
    }
}
