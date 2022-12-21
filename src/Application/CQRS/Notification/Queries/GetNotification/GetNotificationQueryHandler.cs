using AutoMapper;
using Jorda.Application.Common.Exceptions;
using Jorda.Application.Common.Interfaces;
using Jorda.Application.CQRS.Goal.Queries.DTOs;
using Jorda.Application.CQRS.Goal.Queries.GetGoal;
using Jorda.Server.Application.CQRS.Notification.Queries.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jorda.Server.Application.CQRS.Notification.Queries.GetNotification;

public class GetNotificationQueryHAndler : IRequestHandler<GetNotificationQuery, NotificationDTO>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetNotificationQueryHAndler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<NotificationDTO> Handle(GetNotificationQuery request, CancellationToken cancellationToken)
    {
        var item = await _context.Notifications.FirstOrDefaultAsync(n => n.Id == request.Id);
        if (item == null)
        {
            throw new NotFoundException(nameof(Server.Domain.Entities.Notification), request.Id);
        }
        return _mapper.Map<NotificationDTO>(item);
    }
}

