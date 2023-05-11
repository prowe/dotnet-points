public class Query
{
    public async Task<IEnumerable<AccountEvent>> AccountEvents(Guid accountId)
    {
        var events = Enumerable.Empty<AccountEvent>();
        return events;
    }
}