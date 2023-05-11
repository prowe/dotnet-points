
public abstract class AccountEvent
{
    public Guid Id { get; set; }
    public Guid AccountId { get; set; }
    public int PointChange { get; set; } = 0;
}

public class DepositPointsEvent : AccountEvent
{
}