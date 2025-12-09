using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CautiousPotato.Database;

public class CautiousPotatoDbContextFactory : IDesignTimeDbContextFactory<CautiousPotatoDbContext>
{
    public CautiousPotatoDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<CautiousPotatoDbContext>();
        optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Trusted_Connection=True;TrustServerCertificate=True;Integrated Security=True");

        return new CautiousPotatoDbContext(optionsBuilder.Options);
    }
}