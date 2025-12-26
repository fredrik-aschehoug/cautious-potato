using CauriousPotato.Requests.Ingredients;
using CautiousPotato.Core.Models;
using CautiousPotato.Database.Repositories;

namespace CautiousPotato.Services;

public class IngredientService(IIngredientRepository repository) : IIngredientService
{
    public async Task<Ingredient> CreateAsync(CreateIngredientRequest request)
    {
        var ingredient = new Ingredient(Guid.NewGuid(), request.Name);
        await repository.CreateAsync(ingredient);
        return ingredient;
    }

    public Task DeleteAsync(DeleteIngredientRequest request) => repository.DeleteAsync(request.Id);
    public Task<Ingredient[]> GetAllAsync() => repository.GetAllAsync();

    public Task<Ingredient?> GetAsync(GetIngredientRequest request) => repository.GetAsync(request.Id);
}
