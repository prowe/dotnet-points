public class Mutation
{
    private readonly ILogger<Mutation> _logger;

    public Mutation(ILogger<Mutation> logger)
    {
        _logger = logger;
    }

    public async Task<DepositPointsEvent> DepositPoints(DepositPointsInput input)
    {
        _logger.LogInformation("Deposit Event input: {}", input);
        var depositEvent = new DepositPointsEvent
        {
            Id = Guid.NewGuid(),
            AccountId = input.accountId,
            PointChange = input.amount,
        };
        return depositEvent;
    }
}

public class DepositPointsInput
{
    public Guid accountId { get; set; }
    public int amount { get; set; }
}