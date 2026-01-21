using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;

namespace NetPower.API.OpenApi;

/// <summary>
/// Adds Bearer token authentication scheme to OpenAPI document.
/// </summary>
public sealed class BearerSecuritySchemeTransformer : IOpenApiDocumentTransformer
{
    public Task TransformAsync(OpenApiDocument document, OpenApiDocumentTransformerContext context, CancellationToken cancellationToken)
    {
        var securityScheme = new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT",
            Description = "Enter your JWT token in the format: Bearer {your token}",
            In = ParameterLocation.Header
        };

        document.Components ??= new OpenApiComponents();
        document.Components.SecuritySchemes ??= new Dictionary<string, IOpenApiSecurityScheme>();
        document.Components.SecuritySchemes["Bearer"] = securityScheme;

        // Apply security requirement globally
        var securityRequirement = new OpenApiSecurityRequirement();
        var schemeReference = new OpenApiSecuritySchemeReference("Bearer", document);
        
        securityRequirement.Add(schemeReference, new List<string>());
        
        if (document.Security == null)
        {
            document.Security = new List<OpenApiSecurityRequirement>();
        }
        document.Security.Add(securityRequirement);

        return Task.CompletedTask;
    }
}
