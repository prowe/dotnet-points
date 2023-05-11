using Microsoft.EntityFrameworkCore;

public class Query
{
    public async Task<IEnumerable<AccountEvent>> AccountEvents(
        Guid accountId, [Service] ApiDbContext dbContext)
    {
        var events = await dbContext.AccountEvents
            .Where(e => e.AccountId == accountId)
            .OrderByDescending(e => e.Timestamp)
            .ToListAsync();
        return events;
    }
}