using Microsoft.AspNetCore.Mvc;

namespace NetPower.API.Controllers;

/// <summary>
/// Base class for all API controllers.
/// Provides common configuration and behavior for API endpoints.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public abstract class ApiControllerBase : ControllerBase
{
}
