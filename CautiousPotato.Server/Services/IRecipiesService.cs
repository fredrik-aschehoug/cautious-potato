using CauriousPotato.Requests.Ingredients;
using CauriousPotato.Requests.Recipies;
using CautiousPotato.Core.Models;

namespace CautiousPotato.Server.Services;

public interface IRecipiesService
{
    Task<Recipe> CreateAsync(CreateRecipeRequest request);
    Task DeleteAsync(DeleteRecipeRequest request);
    Task<Recipe[]> GetAllAsync();
    Task<Recipe?> GetAsync(GetRecipeRequest request);
}