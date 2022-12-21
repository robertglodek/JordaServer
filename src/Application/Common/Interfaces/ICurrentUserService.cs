namespace Jorda.Application.Common.Interfaces;

public interface ICurrentUserService
{
    public string? UserId { get; }
    public string? UserRole { get; }
}
