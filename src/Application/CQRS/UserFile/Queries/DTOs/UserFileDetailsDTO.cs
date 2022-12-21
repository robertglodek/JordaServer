namespace Jorda.Server.Application.CQRS.UserFile.Queries.DTOs;

public class UserFileDetailsDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string ContentType { get; set; } = null!;
    public string Data { get; set; } = null!;
}
