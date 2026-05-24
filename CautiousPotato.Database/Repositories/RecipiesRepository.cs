using CautiousPotato.Core.Models;
using CautiousPotato.Database.Mapping;
using Microsoft.EntityFrameworkCore;

namespace CautiousPotato.Database.Repositories;

internal class RecipiesRepository(CautiousPotatoDbContext dbContext) : IRecipiesRepository
{
    public async Task<Recipe> CreateAsync(string name, Guid[] ingredientIds)
    {
        var ingredients = await dbContext.Ingredients
            .Where(ingredient => ingredientIds.Contains(ingredient.ExternalId))
            .ToListAsync();

        var entity = new Entities.Recipe
        {
            ExternalId = Guid.NewGuid(),
            Name = name,
            Ingredients = ingredients
        };

        dbContext.Recipes.Add(entity);
        await dbContext.SaveChangesAsync();

        return entity.ToCoreModel();
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

    public async Task<Recipe> GetRequiredAsync(Guid id)
    {
        var entity = await dbContext.Recipes
            .Include(r => r.Ingredients)
            .SingleAsync(recipe => recipe.ExternalId == id);

        return entity.ToCoreModel();
    }

    public async Task AddIngredientAsync(Guid id, Guid ingredientId)
    {
        var recipe = await dbContext.Recipes
            .Include(r => r.Ingredients)
            .SingleAsync(recipe => recipe.ExternalId == id);
        
        var ingredient = await dbContext.Ingredients
            .SingleAsync(ingredient => ingredient.ExternalId == ingredientId);

        recipe.Ingredients.Add(ingredient);

        await dbContext.SaveChangesAsync();
    }

    public async Task RemoveIngredientAsync(Guid id, Guid ingredientId)
    {
        var recipe = await dbContext.Recipes
            .Include(r => r.Ingredients)
            .SingleAsync(recipe => recipe.ExternalId == id);

        recipe.Ingredients = recipe.Ingredients.Where(ingredient => ingredient.ExternalId != ingredientId).ToList();

        await dbContext.SaveChangesAsync();
    }
}
