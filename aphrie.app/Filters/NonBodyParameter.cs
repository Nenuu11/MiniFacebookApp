using Microsoft.OpenApi.Models;
using System.Collections.Generic;

namespace aphrie.app.Filters
{
    internal class NonBodyParameter : OpenApiParameter
    {
        public string Type { get; set; }
        public List<string> Enum { get; set; }
    }
}