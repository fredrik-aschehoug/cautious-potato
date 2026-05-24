using CauriousPotato.Requests.Recipies;
using CautiousPotato.Core.Models;
using CautiousPotato.Database.Repositories;

namespace CautiousPotato.Server.Services;

public class RecipiesService(IRecipiesRepository repository) : IRecipiesService
{
    public Task<Recipe> CreateAsync(CreateRecipeRequest request) =>
        repository.CreateAsync(request.Name, request.Ingredients);

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
