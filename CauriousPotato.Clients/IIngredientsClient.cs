using CauriousPotato.Requests.Ingredients;
using CautiousPotato.Core.Models;

namespace CauriousPotato.Clients;

public interface IIngredientsClient
{
    Task<Ingredient> CreateAsync(CreateIngredientRequest request);
    Task DeleteAsync(DeleteIngredientRequest request);
    Task<Ingredient[]> GetAllAsync();
    Task<Ingredient?> GetAsync(GetIngredientRequest request);
}