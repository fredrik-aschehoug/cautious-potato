using CauriousPotato.Requests.Ingredients;
using CautiousPotato.Core.Models;

namespace CautiousPotato.Services;

public interface IIngredientService
{
    Task<Ingredient> CreateAsync(CreateIngredientRequest request);
    Task DeleteAsync(DeleteIngredientRequest request);
    Task<Ingredient[]> GetAllAsync();
    Task<Ingredient?> GetAsync(GetIngredientRequest request);
}