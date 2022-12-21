using MediatR;

namespace Jorda.Server.Domain.Common;
public abstract class BaseEvent:INotification
{
    public abstract string GetEventEntityDescription();

}
