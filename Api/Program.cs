using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddGraphQLServer()
    .AddDocumentFromFile("schema.graphql")
    .BindRuntimeType<Query>()
    .BindRuntimeType<Mutation>()
    .AddErrorFilter(err => err);

builder.Services.AddDbContext<ApiDbContext>(options => {
    options.UseSqlite(builder.Configuration.GetConnectionString("Database"));
});

var app = builder.Build();

app.MapGraphQL();
app.Run();
