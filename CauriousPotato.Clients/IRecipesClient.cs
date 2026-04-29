using CauriousPotato.Requests.Recipies;
using CautiousPotato.Core.Models;

namespace CauriousPotato.Clients;

public interface IRecipesClient
{
    Task<Recipe> CreateAsync(CreateRecipeRequest request);
    Task DeleteAsync(DeleteRecipeRequest request);
    Task<Recipe[]> GetAllAsync();
    Task<Recipe?> GetAsync(GetRecipeRequest request);
}