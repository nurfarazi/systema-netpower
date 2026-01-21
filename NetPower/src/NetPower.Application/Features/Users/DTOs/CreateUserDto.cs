namespace NetPower.Application.Features.Users.DTOs;

/// <summary>
/// Data transfer object for creating a user.
/// </summary>
public class CreateUserDto
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
}
