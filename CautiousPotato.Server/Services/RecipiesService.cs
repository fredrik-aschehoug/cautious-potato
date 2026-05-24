using CauriousPotato.Requests.Recipies;
using CautiousPotato.Core.Models;
using CautiousPotato.Database.Repositories;

namespace CautiousPotato.Server.Services;

public class RecipiesService(IRecipiesRepository repository, IIngredientRepository ingredientRepository) : IRecipiesService
{
    public async Task<Recipe> CreateAsync(CreateRecipeRequest request)
    {
        var allIngredients = await ingredientRepository.GetAllAsync();
        var ingredients = allIngredients.IntersectBy(request.Ingredients, i => i.Id).ToArray();
        var recipe = new Recipe(Guid.NewGuid(), request.Name, ingredients);
        
        await repository.CreateAsync(recipe);

        return recipe;
    }

    public Task DeleteAsync(DeleteRecipeRequest request) => repository.DeleteAsync(request.Id);
    public Task<ICollection<Recipe>> GetAllAsync() => repository.GetAllAsync();
    public Task<Recipe?> GetAsync(GetRecipeRequest request) => repository.GetAsync(request.Id);

    public async Task<Recipe> RemoveIngredientAsync(RemoveIngredientFromRecipeRequest request)
    {
        await repository.RemoveIngredientAsync(request.RecipeId, request.IngredientId);
        return await repository.GetRequiredAsync(request.RecipeId);
    }

    public async Task<Recipe> AddIngredientAsync(AddIngredientToRecipeRequest request)
    {
        await repository.AddIngredientAsync(request.RecipeId, request.IngredientId);
        return await repository.GetRequiredAsync(request.RecipeId);
    }
}
