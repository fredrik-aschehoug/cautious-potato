using CauriousPotato.Requests.Recipies;
using CautiousPotato.Core.Models;

namespace CautiousPotato.Server.Services;

public interface IRecipiesService
{
    Task<Recipe> CreateAsync(CreateRecipeRequest request);
    Task DeleteAsync(DeleteRecipeRequest request);
    Task<ICollection<Recipe>> GetAllAsync();
    Task<Recipe?> GetAsync(GetRecipeRequest request);
    Task<Recipe> AddIngredientAsync(AddIngredientToRecipeRequest request);
    Task<Recipe> RemoveIngredientAsync(RemoveIngredientFromRecipeRequest request);
}