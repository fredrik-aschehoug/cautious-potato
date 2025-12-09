using CautiousPotato.Database.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CautiousPotato.Database;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        services.AddDbContext<CautiousPotatoDbContext>(options =>
            options.UseSqlServer("Server=localhost\\SQLEXPRESS;Trusted_Connection=True;TrustServerCertificate=True;Integrated Security=True"));

        services.AddScoped<IIngredientRepository, IngredientRepository>();

        return services;
    }
}
