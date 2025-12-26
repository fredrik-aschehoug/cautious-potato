using CautiousPotato.Core.Models;
using CautiousPotato.Database.Mapping;
using Microsoft.EntityFrameworkCore;

namespace CautiousPotato.Database.Repositories;

public class IngredientRepository(CautiousPotatoDbContext dbContext) : IIngredientRepository
{
    public async Task CreateAsync(Ingredient ingredient)
    {
        var entity = ingredient.ToEntity();

        dbContext.Ingredients.Add(entity);
        await dbContext.SaveChangesAsync();
    }

    public Task DeleteAsync(Guid id) => dbContext.Ingredients
        .Where(ingredient => ingredient.ExternalId == id)
        .ExecuteDeleteAsync();

    public async Task<Ingredient[]> GetAllAsync()
    {
        var entities = await dbContext.Ingredients.ToArrayAsync();
        return entities.ToCoreModel();
    }

    public async Task<Ingredient?> GetAsync(Guid id)
    {
        var entity = await dbContext.Ingredients
            .SingleOrDefaultAsync(ingredient => ingredient.ExternalId == id);

        return entity?.ToCoreModel();
    }
}
