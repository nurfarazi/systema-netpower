using NetPower.API.Middleware;
using NetPower.API.OpenApi;
using NetPower.Application;
using NetPower.Infrastructure;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Add OpenAPI services with enhanced configuration
builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer<OpenApiMetadataTransformer>();
    options.AddDocumentTransformer<BearerSecuritySchemeTransformer>();
});

// Register clean architecture layers
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration, useAdoNet: false);

// Configure logging
builder.Services.AddLogging(config =>
{
    config.AddConsole();
    config.AddDebug();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Map OpenAPI endpoint
    app.MapOpenApi();
    
    // Add Scalar UI for interactive API documentation
    app.MapScalarApiReference(options =>
    {
        options
            .WithTitle("NetPower API")
            .WithTheme(ScalarTheme.Purple)
            .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
    });
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
