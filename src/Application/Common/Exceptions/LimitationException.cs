namespace Jorda.Server.Application.Common.Exceptions;

public class LimitationException:Exception
{
    /// <summary>
    /// Initialize error object
    /// </summary>
    /// <param name="message">Short error message</param>
    /// <param name="details">Localized error explanation</param>
    public LimitationException(string message, string details)
    : base(message)
    {
        Details = details;
    }
    public string Details { get; }
}
