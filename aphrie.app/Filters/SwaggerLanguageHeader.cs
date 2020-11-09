using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aphrie.app.Filters
{
    public class SwaggerLanguageHeader : IOperationFilter
    {
        readonly IServiceProvider _serviceProvider;
        public SwaggerLanguageHeader(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
                operation.Parameters = new List<OpenApiParameter>();
            operation.Parameters.Add(new NonBodyParameter
            {
                Name = "Accept-Language",
                In = ParameterLocation.Header,
                Type = "string",
                Enum = (_serviceProvider.GetService(typeof(IOptions<RequestLocalizationOptions>)) as IOptions<RequestLocalizationOptions>)?
                        .Value?.SupportedCultures?.Select(c => c.TwoLetterISOLanguageName).ToList<string>(),
                Required = false,
            });
        }
    }
}
