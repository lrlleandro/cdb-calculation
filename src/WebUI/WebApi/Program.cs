using Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddWebApiServices();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();
    await initialiser.SeedAsync();
}

app.UseWebApiConfigurations()
    .Run();