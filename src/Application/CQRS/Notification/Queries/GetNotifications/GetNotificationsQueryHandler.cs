using AutoMapper;
using Jorda.Application.Common.Interfaces;
using Jorda.Server.Application.CQRS.Notification.Queries.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Jorda.Server.Application.CQRS.Notification.Queries.GetNotifications;

public class GetNotificationsQueryHandler : IRequestHandler<GetNotificationsQuery, IEnumerable<NotificationDTO>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetNotificationsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<NotificationDTO>> Handle(GetNotificationsQuery request, CancellationToken cancellationToken)
    {
        IQueryable<Domain.Entities.Notification> items = _context.Notifications;
        if(request.ActiveOnly!=null)
        {
            items = items.Where(n => n.Active == request.ActiveOnly);
        }
        return _mapper.Map<List<NotificationDTO>>(await items.ToListAsync());
    }
}

