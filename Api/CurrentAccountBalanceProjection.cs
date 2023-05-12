
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

public class CurrentAccountBalanceProjection
{
    public Guid Id { get; set; }
    public DateTime? LastAppliedEventTimestamp { get; set; }
    public int Balance { get; set; } = 0;

    public void Apply(AccountEvent accountEvent)
    {
        if (accountEvent.Timestamp <= LastAppliedEventTimestamp)
        {
            return;
        }
        LastAppliedEventTimestamp = accountEvent.Timestamp;
        Balance += accountEvent.PointChange;
    }
}

public class CurrentAccountBalanceEventInterceptor : SaveChangesInterceptor
{
    private readonly ILogger<CurrentAccountBalanceEventInterceptor> _logger;

    public CurrentAccountBalanceEventInterceptor(ILogger<CurrentAccountBalanceEventInterceptor> logger)
    {
        _logger = logger;
    }

    public void OnEntityTracked(object? sender, EntityTrackedEventArgs entityTrackedEvent)
    {
        if (entityTrackedEvent.FromQuery)
        {
            return;
        }
        var accountEvent = entityTrackedEvent.Entry.Entity as AccountEvent;
        if (accountEvent == null) {
            return;
        }
    }

    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("SavingChangesAsync Fired");
        await ApplyEvents(eventData.Context, cancellationToken);
        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private async Task ApplyEvents(DbContext? context, CancellationToken cancellationToken)
    {
        if (context == null)
        {
            return;
        }
        var events = context.ChangeTracker
            .Entries<AccountEvent>()
            .Where(entry => entry.State == EntityState.Added)
            .Select(entry => entry.Entity)
            .OrderBy(ae => ae.Timestamp);

        foreach (var accountEvent in events)
        {
            var accountId = accountEvent.AccountId;
            var proj = await context.FindAsync<CurrentAccountBalanceProjection>(accountId, cancellationToken);
            if (proj == null)
            {
                proj = new CurrentAccountBalanceProjection { Id = accountId };
                context.Add(proj);
            }
            proj.Apply(accountEvent);
        }
    }
}