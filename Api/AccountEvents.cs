
using Microsoft.EntityFrameworkCore;

public abstract class AccountEvent
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid AccountId { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public int PointChange { get; set; } = 0;

    public async Task<int> RemainingBalance([Service] ApiDbContext dbContext)
    {
        var balance = await dbContext.AccountEvents
            .Where(ae => ae.AccountId == this.AccountId && ae.Timestamp < this.Timestamp)
            .SumAsync(ae => ae.PointChange);
        return balance + PointChange;
    }
}

public class DepositPointsEvent : AccountEvent
{
}

public class RedeemPointsEvent : AccountEvent
{
    public Guid ProductId { get; set; }
}

public class BalanceAdjustmentEvent: AccountEvent
{
    public string Reason { get; set; }
}