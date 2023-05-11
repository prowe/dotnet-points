public class Mutation
{
    private readonly ILogger<Mutation> _logger;

    public Mutation(ILogger<Mutation> logger)
    {
        _logger = logger;
    }

    public async Task<DepositPointsEvent> DepositPoints(DepositPointsInput input, [Service] ApiDbContext dbContext)
    {
        _logger.LogInformation("Deposit Event input: {}", input);
        var depositEvent = new DepositPointsEvent
        {
            AccountId = input.AccountId,
            PointChange = input.Amount,
        };
        dbContext.Add(depositEvent);
        await dbContext.SaveChangesAsync();
        return depositEvent;
    }

    public async Task<RedeemPointsEvent> RedeemPoints(RedeemPointsInput input, [Service] ApiDbContext dbContext)
    {
        _logger.LogInformation("Redeem Event input: {}", input);
        var price = Math.Abs(input.ProductId.GetHashCode()) % 20 + 1;
        var redeemEvent = new RedeemPointsEvent
        {
            AccountId = input.AccountId,
            PointChange = -1 * price,
            ProductId = input.ProductId
        };
        dbContext.Add(redeemEvent);
        await dbContext.SaveChangesAsync();
        return redeemEvent;
    }

    public async Task<BalanceAdjustmentEvent> AdjustBalance(BalanceAdjustmentInput input, [Service] ApiDbContext dbContext)
    {
        _logger.LogInformation("Adjust balance: {}", input);
        var adjustEvent = new BalanceAdjustmentEvent
        {
            AccountId = input.AccountId,
            PointChange = input.Amount,
            Reason = input.Reason
        };
        dbContext.Add(adjustEvent);
        await dbContext.SaveChangesAsync();
        return adjustEvent;
    }
}

public class DepositPointsInput
{
    public Guid AccountId { get; set; }
    public int Amount { get; set; }
}

public class RedeemPointsInput
{
    public Guid AccountId { get; set; }
    public Guid ProductId { get; set; }
}

public class BalanceAdjustmentInput
{
    public Guid AccountId { get; set; }
    public int Amount { get; set; }
    public string Reason { get; set; }
}