using Jorda.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Jorda.Server.Application.Common.Jobs;

public class DetermineUsersTodosCompletionJob: IJob
{
    private readonly IApplicationDbContext _dbContext;

    public DetermineUsersTodosCompletionJob(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Execute()
    {
        var dateTimeNow = DateTime.UtcNow;

        var items = await _dbContext.TodoItems.Where(n => n.Active && n.DueDate != null && dateTimeNow > n.DueDate
            && dateTimeNow.DayOfWeek != n.DueDate.Value.DayOfWeek).Include(n=>n.Section).ThenInclude(n=>n.Goal).ToListAsync();

        if(items.Count>0)
        {
            items.ForEach(n => n.Failed());
            await _dbContext.SaveChangesAsync();
        }  
    }

}
