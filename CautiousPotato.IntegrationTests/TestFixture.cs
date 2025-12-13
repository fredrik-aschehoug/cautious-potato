using AutoFixture;
using CauriousPotato.Clients;
using CautiousPotato.Database;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CautiousPotato.IntegrationTests;

public class TestFixture : WebApplicationFactory<Program>, IAsyncLifetime
{
    public IIngredientsClient IngredientsClient => new IngredientsClient(CreateClient());
    public Fixture AutoFixture = new();

    public async ValueTask InitializeAsync()
    {
        using var scope = Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<CautiousPotatoDbContext>();

        await dbContext.Database.MigrateAsync();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            services.AddDbContext<CautiousPotatoDbContext>(options =>
            options.UseSqlServer("Server=localhost\\SQLEXPRESS;Initial Catalog=CautiousPotato-test;Trusted_Connection=True;TrustServerCertificate=True;Integrated Security=True"));

        });
        
    }
}
