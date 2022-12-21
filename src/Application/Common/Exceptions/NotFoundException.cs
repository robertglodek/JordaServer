using Jorda.Server.Application.Common.Resources;

namespace Jorda.Application.Common.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string name, object key)
        : base($"Entity \"{name}\" ({key}) was not found.")
    {
        Details = string.Format(General.NotFound, name, key);
    }

    public string Details { get; set; }
}
