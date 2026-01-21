using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;

namespace NetPower.API.OpenApi;

/// <summary>
/// Adds metadata and contact information to the OpenAPI document.
/// </summary>
public sealed class OpenApiMetadataTransformer : IOpenApiDocumentTransformer
{
    public Task TransformAsync(OpenApiDocument document, OpenApiDocumentTransformerContext context, CancellationToken cancellationToken)
    {
        document.Info = new OpenApiInfo
        {
            Title = "NetPower API",
            Version = "v1",
            Description = "A modern REST API built with Clean Architecture and ASP.NET Core 10",
            Contact = new OpenApiContact
            {
                Name = "NetPower Team",
                Email = "support@netpower.com",
                Url = new Uri("https://github.com/netpower/netpower")
            },
            License = new OpenApiLicense
            {
                Name = "MIT License",
                Url = new Uri("https://opensource.org/licenses/MIT")
            }
        };

        // Add tags with descriptions
        var usersTag = new OpenApiTag
        {
            Name = "Users",
            Description = "Operations related to user management"
        };
        
        var weatherTag = new OpenApiTag
        {
            Name = "Weather",
            Description = "Weather forecast operations"
        };

        document.Tags.Add(usersTag);
        document.Tags.Add(weatherTag);

        return Task.CompletedTask;
    }
}
