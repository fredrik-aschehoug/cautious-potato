using CautiousPotato.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace CautiousPotato.Database;

public class CautiousPotatoDbContext(DbContextOptions<CautiousPotatoDbContext> options) : DbContext(options)
{
    public DbSet<Ingredient> Ingredients { get; set; }
    public DbSet<Recipe> Recipes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Recipe>()
            .HasMany(e => e.Ingredients)
            .WithMany();
    }
}
