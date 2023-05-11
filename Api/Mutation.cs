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
            AccountId = input.accountId,
            PointChange = input.amount,
        };
        dbContext.Add(depositEvent);
        await dbContext.SaveChangesAsync();
        return depositEvent;
    }

    public async Task<RedeemPointsEvent> RedeemPoints(RedeemPointsInput input, [Service] ApiDbContext dbContext)
    {
        _logger.LogInformation("Redeem Event input: {}", input);
        var price = Math.Abs(input.productId.GetHashCode()) % 20 + 1;
        var depositEvent = new RedeemPointsEvent
        {
            AccountId = input.accountId,
            PointChange = -1 * price,
            ProductId = input.productId
        };
        dbContext.Add(depositEvent);
        await dbContext.SaveChangesAsync();
        return depositEvent;
    }
}

public class DepositPointsInput
{
    public Guid accountId { get; set; }
    public int amount { get; set; }
}

public class RedeemPointsInput
{
    public Guid accountId { get; set; }
    public Guid productId { get; set; }
}