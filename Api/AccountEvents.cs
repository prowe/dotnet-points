
public abstract class AccountEvent
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid AccountId { get; set; }
    public DateTimeOffset Timestamp { get; set; } = DateTimeOffset.Now;
    public int PointChange { get; set; } = 0;
}

public class DepositPointsEvent : AccountEvent
{
}

public class RedeemPointsEvent : AccountEvent
{
    public Guid ProductId { get; set; }
}