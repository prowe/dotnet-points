using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddGraphQLServer()
    .AddDocumentFromFile("schema.graphql")
    .BindRuntimeType<Query>()
    .BindRuntimeType<Mutation>()
    .AddErrorFilter(err => err);

builder.Services
    .AddSingleton<IInterceptor, CurrentAccountBalanceEventInterceptor>();

builder.Services.AddDbContext<ApiDbContext>((provider, options) => {
    options.UseSqlite(builder.Configuration.GetConnectionString("Database"));
    options.AddInterceptors(provider.GetServices<IInterceptor>());
});

var app = builder.Build();

app.MapGraphQL();
app.Run();
