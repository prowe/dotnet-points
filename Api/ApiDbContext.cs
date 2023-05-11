using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

public class ApiDbContext : DbContext
{
    public DbSet<AccountEvent> AccountEvents { get; set; }
    
    public ApiDbContext(DbContextOptions<ApiDbContext> options): base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AccountEvent>()
            .HasIndex(ae => ae.AccountId);

        modelBuilder.Entity<DepositPointsEvent>()
            .HasBaseType<AccountEvent>();

        modelBuilder.Entity<RedeemPointsEvent>()
            .HasBaseType<AccountEvent>();

        modelBuilder.Entity<BalanceAdjustmentEvent>()
            .HasBaseType<AccountEvent>();
    }
}