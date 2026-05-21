using CautiousPotato.Core.Models;
using CautiousPotato.Database.Mapping;
using Microsoft.EntityFrameworkCore;

namespace CautiousPotato.Database.Repositories;

internal class RecipiesRepository(CautiousPotatoDbContext dbContext) : IRecipiesRepository
{
    public async Task CreateAsync(Recipe recipe)
    {
        var entity = recipe.ToEntity();

        dbContext.Recipes.Add(entity);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        await dbContext.Recipes
            .Include(r => r.Ingredients)
            .Where(recipe => recipe.ExternalId == id)
            .ExecuteDeleteAsync();
    }

    public async Task<ICollection<Recipe>> GetAllAsync()
    {
        var entities = await dbContext.Recipes.Include(r => r.Ingredients).ToArrayAsync();
        return entities.ToCoreModel();
    }

    public async Task<Recipe?> GetAsync(Guid id)
    {
        var entity = await dbContext.Recipes
            .Include(r => r.Ingredients)
            .SingleOrDefaultAsync(recipe => recipe.ExternalId == id);

        return entity?.ToCoreModel();
    }
}
